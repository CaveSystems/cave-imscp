using Cave;
using Cave.Collections.Generic;
using Cave.Data;
using Cave.Data.Mysql;
using Cave.IO;
using Cave.Logging;
using Cave.Service;
using Imscp;
using InternetX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace Cave.AutoDns
{
    class Program : ServiceProgram
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        ImscpConfig imscpConfig;
        IDatabase imscp;
        ReadCachedTable<Domain> imscpDomain;
        ReadCachedTable<DomainDns> imscpDomainDns;
        ReadCachedTable<Subdomain> imscpSubdomain;
        ReadCachedTable<Admin> imscpAdmins;
        List<Autodns> autodnsAccounts = new List<Autodns>();
        Dictionary<string, AutodnsRecordItem> autodnsCache = new Dictionary<string, AutodnsRecordItem>();
        DateTime autodnsRecheckTime = new DateTime();
        Set<string> warningsSent = new Set<string>();
        string m_MailSender;
        SmtpClient m_SmtpClient;


        protected override void OnKeyPressed(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.Escape) ServiceParameters.CommitShutdown();
        }

        protected override void Worker()
        {
            LoadConfig();

            SyncFromInternetX();

            int errorCounter = 0;
            DateTime nextCheck = new DateTime();
            while (!ServiceParameters.Shutdown)
            {
                Thread.Sleep(100);

                if (DateTime.Now > nextCheck)
                {
                    nextCheck = DateTime.Now.AddSeconds(10);
                    try
                    {
                        SyncToInternextX();
                        errorCounter = 0;
                    }
                    catch (Exception ex)
                    {
                        nextCheck = DateTime.Now.AddSeconds(60 + errorCounter++ * 10);
                        this.LogWarning(ex, ex.Message);
                        string msg = string.Format("Error number {0}, Retry at {1}", errorCounter, nextCheck);
                        this.LogInfo(msg);
                        m_SmtpClient.Send(m_MailSender, "admin-team@cave.cloud", "CaveSystems AutoDNS", ex.ToText(true) + "\r\n" + msg);
                    }
                }
            }
        }

        private bool SyncToInternextX()
        {
            bool result = false;
            imscpDomain.UpdateCache();
            imscpDomainDns.UpdateCache();
            imscpSubdomain.UpdateCache();
            imscpAdmins.UpdateCache();

            foreach (Domain imscpDomain in imscpDomain.GetStructs())
            {
                if (imscpDomain.Name.Split('.').Length > 2)
                {
                    this.LogDebug("Ignore sub domain {0}", imscpDomain);
                    continue;
                }

                this.LogDebug("Check domain <cyan>{0}", imscpDomain);
                bool changeInProgress = imscpDomain.Status != "ok";

                //check if the imscp domain is present at internet x
                if (!autodnsCache.TryGetValue(imscpDomain.Name, out AutodnsRecordItem item))
                {
                    //already sent email ?
                    if (warningsSent.Contains(imscpDomain.Name)) continue;
                    //no, maybe someone repaired it ?
                    bool found = false;
                    if (DateTime.Now > autodnsRecheckTime)
                    {
                        SyncFromInternetX();
                        found = autodnsCache.TryGetValue(imscpDomain.Name, out item);
                    }
                    if (!found)
                    {
                        //no its really missing
                        this.LogWarning("Zone <red>{0}<default> is not present at <red>InternetX<default>, skipping...", imscpDomain);
                        warningsSent.Include(imscpDomain.Name);
                        //send email
                        try
                        {
                            var email = imscpAdmins[imscpDomain.AdminID].Email;
                            m_SmtpClient.Send(m_MailSender, email, "CaveSystems AutoDNS", string.Format("Zone {0} is registered at imscp but is not present at InternetX.\nPlease correct this error...", imscpDomain));
                        }
                        catch { }
                        continue;
                    }
                }

                Autodns autodns = item.Autodns;
                Set<DnsResourceRecord> records = new Set<DnsResourceRecord>(item.Records);
                List<DnsResourceRecord> newRecords = new List<DnsResourceRecord>();

                var imscpDomainDnsEntries = imscpDomainDns.GetStructs(nameof(DomainDns.DomainID), (int)imscpDomain.ID);
                foreach (DomainDns imscpDomainDnsEntry in imscpDomainDnsEntries)
                {
                    if (imscpDomainDnsEntry.Status == "todelete") continue;
                    if (imscpDomainDnsEntry.Status.StartsWith("to"))
                    {
                        changeInProgress = true;
                        this.LogDebug("Domain <cyan>{0}<default> change in progress <yellow>{1}", imscpDomain, imscpDomainDnsEntry.Status);
                        break;
                    }
                    if (imscpDomainDnsEntry.Status != "ok")
                    {
                        changeInProgress = true;
                        this.LogWarning("Domain <cyan>{0}<default> error <red>{1}", imscpDomain, imscpDomainDnsEntry.Status);
                        break;
                    }

                    DnsResourceRecord newRec = new DnsResourceRecord
                    {
                        Name = imscpDomainDnsEntry.Name.Replace("wildcard", "*")
                    };
                    if (imscpDomainDnsEntry.Name.StartsWith(".") || (imscpDomainDnsEntry.Name != imscpDomain.Name + "." && !imscpDomainDnsEntry.Name.EndsWith("." + imscpDomain.Name + ".")))
                    {
                        this.LogWarning("Invalid entry <yellow>{0}", imscpDomainDnsEntry);
                        ImscpDeleteDomainDns(imscpDomainDnsEntry);
                        continue;
                    }
                    newRec.Name = newRec.Name.Substring(0, newRec.Name.Length - imscpDomain.Name.Length - 1).TrimEnd('.');
                    if (newRec.Name.Length == 0) newRec.Name = null;
                    newRec.TTL = imscpDomainDnsEntry.TTL;
                    newRec.Type = imscpDomainDnsEntry.DomainType;
                    if (newRec.Name == "www")
                    {
                        this.LogWarning("Invalid entry <yellow>{0}", imscpDomain);
                        ImscpDeleteDomainDns(imscpDomainDnsEntry);
                        continue;
                    }
                    try
                    {
                        switch (imscpDomainDnsEntry.DomainType)
                        {
                            default:
                                newRec.Value = imscpDomainDnsEntry.DomainText;
                                break;
                            case "MX":
                            case "SRV":
                                int i = imscpDomainDnsEntry.DomainText.IndexOf(' ');
                                newRec.Pref = uint.Parse(imscpDomainDnsEntry.DomainText.Substring(0, i));
                                newRec.Value = imscpDomainDnsEntry.DomainText.Substring(i + 1);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        this.LogWarning(ex, "Invalid entry <yellow>{0}", imscpDomainDnsEntry);
                        ImscpDeleteDomainDns(imscpDomainDnsEntry);
                        continue;
                    }
                    newRecords.Add(newRec);
                }
                if (changeInProgress) { result = true; continue; }

                //add autoconfig if not present
                if (!newRecords.Any(r => r.Name == "autoconfig"))
                {
                    newRecords.Insert(0, new DnsResourceRecord()
                    {
                        Name = "autoconfig",
                        Type = "CNAME",
                        Value = imscpConfig.Hostname + ".",
                        TTL = 360,
                    });
                }

                //add www if not present
                if (!newRecords.Any(r => r.Name == "www"))
                {
                    newRecords.Insert(0, new DnsResourceRecord()
                    {
                        Name = "www",
                        Type = "A",
                        Value = imscpConfig.DomainIP.ToString(),
                        TTL = 360,
                    });
                }

                //add mx if not present
                if (!newRecords.Any(r => r.Type == "MX"))
                {
                    newRecords.Insert(0, new DnsResourceRecord()
                    {
                        Name = "",
                        Type = "MX",
                        Value = imscpConfig.Hostname + ".",
                        TTL = 360,
                        Pref = 10,
                    });
                }

                //add TXT spf if not present
                if (!newRecords.Any(r => r.Type == "TXT" && r.Value.StartsWith("v=spf1")))
                {
                    newRecords.Add(new DnsResourceRecord()
                    {
                        Name = "",
                        Type = "TXT",
                        Value = $"v=spf1 a mx ip4:{imscpConfig.DomainIP}/32",
                        TTL = 360,
                    });
                }

                //check subdomains
                foreach (var sub in imscpSubdomain.GetStructs(nameof(Subdomain.DomainID), imscpDomain.ID))
                {
                    var items = newRecords.FindAll(r => r.Name == sub.Name);
                    if (items.Count != 1 || items[0].Type != "A")
                    {
                        foreach (var i in items) newRecords.Remove(i);
                        newRecords.Add(new DnsResourceRecord()
                        {
                            Name = sub.Name,
                            Type = "A",
                            Value = imscpConfig.DomainIP.ToString(),
                            TTL = 360,
                        });
                    }
                }

                bool update = false;
                if (newRecords.Count == records.Count)
                {
                    foreach (DnsResourceRecord rec in newRecords)
                    {
                        if (records.Contains(rec)) records.Remove(rec);
                        else break;
                    }
                    update = records.Count > 0;
                }
                else update = true;

                if (update)
                {
                    result = true;
                    this.LogInfo("Update <magenta>{0}<default> with <cyan>{1}<default> resource records.", imscpDomain, newRecords.Count);
                    autodns.ZoneUpdate(imscpDomain.Name, imscpConfig.DomainIP, newRecords);
                    //update at internet x
                    SyncFromInternetX(autodns.ZoneInquire(imscpDomain.Name));
                }
            }
            return result;
        }

        private void SyncFromInternetX()
        {
            //load from internet x
            this.LogInfo("Running full sync from InternetX to Imscp...");
            autodnsRecheckTime = DateTime.Now.AddHours(1);
            autodnsCache.Clear();
            warningsSent.Clear();
            foreach (Autodns autodns in autodnsAccounts)
            {
                this.LogInfo("Sync {0}", autodns);
                string[] zones = autodns.ZoneSearch("*");
                AutodnsRecordList autodnsRecords = autodns.ZoneInquire(zones);
                SyncFromInternetX(autodnsRecords);
            }
        }

        private void SyncFromInternetX(AutodnsRecordList zones)
        {
            foreach (AutodnsRecordItem item in zones.Items)
            {
                this.LogDebug("Sync {0}", item);
                autodnsCache[item.Zone] = item;

                string domainName = item.Zone;
                if (!imscpDomain.TryGetStruct(nameof(Domain.Name), domainName, out Domain domain))
                {
                    this.LogInfo("InternetX Domain <red>{0}<default> is not present in <red>imscp<default>!", domainName);
                    continue;
                }

                var oldDoms = imscpDomainDns.GetStructs(
                    Search.FieldEquals(nameof(DomainDns.DomainID), domain.ID) &
                    Search.FieldEquals(nameof(DomainDns.DomainClass), "IN"));

                bool updated = false;
                foreach (DnsResourceRecord record in item.Records)
                {
                    string name = record.Name;
                    switch (record.Name)
                    {
                        case "www": continue;
                    }

                    if (name == null) name = ""; else name = name + ".";
                    if (name.Contains("*")) name = name.Replace("*", "wildcard");

                    string nameAndTTL = name + domainName + ".\t" + record.TTL;
                    string domainText;
                    switch (record.Type)
                    {
                        case "A":
                        case "AAAA":
                        case "CNAME":
                        case "SPF":
                        case "TXT":
                        case "NS":
                            domainText = record.Value;
                            break;
                        case "SRV":
                        case "MX":
                            domainText = Math.Max(0, record.Pref) + " " + record.Value;
                            break;
                        default: throw new NotImplementedException();
                    }

                    DomainDns domainDns = oldDoms.SingleOrDefault(d => d.NameAndTTL == nameAndTTL && d.DomainType == record.Type && d.DomainText == domainText);
                    if (domainDns.ID == 0)
                    {
                        //create new
                        domainDns = new DomainDns()
                        {
                            //dont care
                            ID = 0,
                            AliasID = 0,
                            OwnedBy = "custom_dns_feature",
                            //fixed
                            DomainClass = "IN",
                            DomainID = (int)domain.ID,
                            //data
                            NameAndTTL = nameAndTTL,
                            DomainType = record.Type,
                            DomainText = domainText,
                            Status = "tochange",
                        };
                        this.LogInfo("New <green>{0}<default>.", domainDns);
                        imscpDomainDns.Insert(domainDns);
                        updated = true;
                        continue;
                    }
                    else
                    {
                        oldDoms.Remove(domainDns);
                    }
                }
                foreach (var dom in oldDoms)
                {
                    var copy = dom;
                    copy.Status = "todelete";
                    updated = true;
                    this.LogInfo("Deleted <red>{0}<default>.", copy);
                    imscpDomainDns.Replace(copy);
                }
                if (updated)
                {
                    domain.Status = "tochange";
                    imscpDomain.Update(domain);
                }
            }
        }

        private void ImscpDeleteDomainDns(DomainDns todelete)
        {
            this.LogInfo("<red>Deleted<default> domain {0}", todelete);
            todelete.Status = "todelete";
            imscpDomainDns.Update(todelete);
        }

        private void LoadConfig()
        {
            IniReader config = IniReader.FromLocation(RootLocation.AllUserConfig);
            this.LogInfo("Loading {0}", config);
            imscpConfig = config.ReadStruct<ImscpConfig>("Imscp", true);

            if (imscpConfig.Hostname == null || imscpConfig.Hostname.EndsWith(".")) throw new Exception(string.Format("Invalid hostname {0}", imscpConfig.Hostname));
            foreach (string section in config.ReadSection("InternetX", true))
            {
                this.LogDebug("Loading InternetX account <cyan>{0}", section);
                InternetXConfig internetXConfig = config.ReadStruct<InternetXConfig>(section, true);
                autodnsAccounts.Add(new Autodns(internetXConfig));
                this.LogInfo("Loaded InternetX account <green>{0}", section);
            }

            var cs = new ConnectionString("mysql", imscpConfig.UserName, imscpConfig.Password, imscpConfig.Database);
            var storage = new MySqlStorage(cs, DbConnectionOptions.AllowUnsafeConnections);
            imscp = storage.GetDatabase("imscp", false);

            imscpDomain = new ReadCachedTable<Domain>(imscp.GetTable<Domain>());
            this.LogDebug("Loaded {0}", imscpDomain);
            imscpSubdomain = new ReadCachedTable<Subdomain>(imscp.GetTable<Subdomain>());
            this.LogDebug("Loaded {0}", imscpSubdomain);
            imscpDomainDns = new ReadCachedTable<DomainDns>(imscp.GetTable<DomainDns>());
            this.LogDebug("Loaded {0}", imscpDomainDns);
            imscpAdmins = new ReadCachedTable<Admin>(imscp.GetTable<Admin>());
            this.LogDebug("Loaded {0}", imscpAdmins);

            #region MAIL
            string server = config.ReadSetting("MAIL", "SERVER");
            m_MailSender = config.ReadSetting("MAIL", "SENDER");
            if (string.IsNullOrEmpty(server)) server = "localhost";
            m_SmtpClient = new SmtpClient(server);
            string username = config.ReadSetting("MAIL", "USERNAME");
            string password = config.ReadSetting("MAIL", "PASSWORD");
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                m_SmtpClient.Credentials = new NetworkCredential(username, password);
            }
            m_SmtpClient.Send(m_MailSender, "admin-team@cave.cloud", "CaveSystems AutoDNS", "AutoDNS restarted.");
            #endregion
        }
    }
}
