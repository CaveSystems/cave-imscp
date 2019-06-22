#region Authors & Contributors
/*
   Author:
     Andreas Rohleder <a.rohleder@cavesystems.de>

   Contributors:

   Copyright (c) 2003-2014 CaveSystems UG (http://www.cavesystems.de)
 */
#endregion
#region LICENSE
/*
    This program/library/sourcecode is free software; you can redistribute it
    and/or modify it under the terms of the GNU General Public License
    version 3 as published by the Free Software Foundation subsequent called
    the License.

    You may not use this program/library/sourcecode except in compliance
    with the License. The License is included in the LICENSE.GPL30 file
    found at the installation directory or the distribution package.

    Permission is hereby granted, free of charge, to any person obtaining
    a copy of this software and associated documentation files (the
    "Software"), to deal in the Software without restriction, including
    without limitation the rights to use, copy, modify, merge, publish,
    distribute, sublicense, and/or sell copies of the Software, and to
    permit persons to whom the Software is furnished to do so, subject to
    the following conditions:

    The above copyright notice and this permission notice shall be included
    in all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
    EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
    NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
    LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
    OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion LICENSE

using Cave.Console;
using Cave.Data;
using Cave.Data.Mysql;
using Cave.IO;
using Cave.Logging;
using Imscp;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Cave.LetsEncrypt
{
    class Program : ILogSource
    {
        private string mailSender;
        private SmtpClient smtpClient;
        private IStorage storage;
        private IDatabase db;
        private ITable<Domain> domains;
        private ITable<SslCerts> ssl_certs;

        public string LogSourceName => "LetsEncrypt";

        public string LetsEncryptPath { get; private set; }

        static void Main(string[] args)
        {
            new Program().Run(Arguments.FromArray(args));
        }

        void Run(Arguments args)
        {
            ILogReceiver console = null;
            if (args.IsOptionPresent("debug")) { if (console == null) console = LogConsole.Create(); console.Level = LogLevel.Debug; }
            if (args.IsOptionPresent("verbose")) { if (console == null) console = LogConsole.Create(); console.Level = LogLevel.Verbose; }

            LoadConfig();

            List<XT> results = new List<XT>();
            foreach (string dir in Directory.GetDirectories(Path.Combine(LetsEncryptPath, "live")))
            {
                string domainName = Path.GetFileName(dir);

                var conf = Path.Combine(LetsEncryptPath, "renewal", domainName + ".conf");
                if (!File.Exists(conf)) continue;
                    
                if (domainName.Split('.').Length != 2) continue;
                if (!domains.TryGetStruct(nameof(Domain.Name), domainName, out Domain domain))
                {
                    var x = XT.Format("<red>Error: <default>Domain <red>{0}<default> removed from imscp!", domainName);
                    results.Add(x);
                    SystemConsole.WriteLine(x);
                    File.Delete(conf);
                    continue;
                }

                string certText = File.ReadAllText(Path.Combine(dir, "cert.pem")).GetValidChars(ASCII.Strings.Printable + '\n') + "\n";
                string keyText = File.ReadAllText(Path.Combine(dir, "privkey.pem")).GetValidChars(ASCII.Strings.Printable + '\n') + "\n";
                string chainText = File.ReadAllText(Path.Combine(dir, "chain.pem")).GetValidChars(ASCII.Strings.Printable + '\n') + "\n";

                var sslCerts = ssl_certs.GetStructs(nameof(SslCerts.DomainID), domain.ID);
                if (sslCerts.Count > 1)
                {
                    var x = XT.Format("<red>Error: <default>Multiple ssl certs for domain {0}!", domain);
                    results.Add(x);
                    SystemConsole.WriteLine(x);
                    continue;
                }
                var newCert = PEM.ReadCert(certText.SplitNewLine());
                if (sslCerts.Count == 1)
                {
                    //already got one, check for update
                    var sslCert = sslCerts[0];
                    var oldCert = PEM.ReadCert(sslCert.Certificate.SplitNewLine());
                    if (newCert.Equals(oldCert))
                    {
                        this.LogInfo("Domain <green>{0}<default> valid till <green>{1}", domainName, oldCert.GetExpirationDateString());
                        //no change
                        continue;
                    }
                    if (!oldCert.Issuer.Contains("O=Let's Encrypt"))
                    {
                        //do not override users own certs
                        continue;
                    }
                    sslCert.Certificate = certText;
                    sslCert.PrivateKey = keyText;
                    sslCert.CaBundle = chainText;
                    sslCert.Status = "tochange";
                    ssl_certs.Update(sslCert);
                }
                else
                {
                    var sslCert = new SslCerts()
                    {
                        AllowHsts = "off",
                        CaBundle = chainText,
                        Certificate = certText,
                        DomainID = (int)domain.ID,
                        DomainType = "dmn",
                        HstsIncludeSubdomains = "off",
                        HstsMaxAge = 31536000,
                        PrivateKey = keyText,
                        Status = "tochange",
                    };
                    ssl_certs.Insert(sslCert);
                }
                domain.Status = "tochange";
                domains.Update(domain);
                {
                    var x = XT.Format("<green>Certificate: <default>Domain {0} new certificate {1} valid till {2}!", domain, newCert.Subject, newCert.GetExpirationDateString());
                    results.Add(x);
                    SystemConsole.WriteLine(x);
                }
            }
            this.LogInfo("Completed.");
            if (results.Count > 0)
            {
                MailMessage msg = new MailMessage(mailSender, "admin-team@cave.cloud")
                {
                    Subject = "CaveSystems LetsEncrypt",
                    IsBodyHtml = true,
                    Body = results.ToHtml()
                };
                smtpClient.Send(msg);
            }
            Logger.Flush();
            Logger.CloseAll();
            console?.Dispose();
        }

        private void LoadConfig()
        {
            var config = IniReader.FromLocation(RootLocation.AllUserConfig);
            {
                var username = config.ReadSetting("imscp", "UserName");
                var passwort = config.ReadSetting("imscp", "Password");
                var hostname = config.ReadSetting("imscp", "hostname");

                ConnectionString con = $"mysql://{username}:{passwort}@{hostname}";
                this.LogDebug("Connecting to {0}", con);
                storage = new MySqlStorage(con, DbConnectionOptions.AllowUnsafeConnections);
                db = storage.GetDatabase("imscp");
                domains = db.GetTable<Domain>();
                ssl_certs = db.GetTable<SslCerts>();
            }

            LetsEncryptPath = config.ReadString("letsencrypt", "path", "/etc/letsencrypt");

            #region MAIL
            {
                string server = config.ReadSetting("MAIL", "SERVER");
                mailSender = config.ReadSetting("MAIL", "SENDER");
                if (string.IsNullOrEmpty(server)) server = "localhost";
                smtpClient = new SmtpClient(server);
                string username = config.ReadSetting("MAIL", "USERNAME");
                string password = config.ReadSetting("MAIL", "PASSWORD");
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    smtpClient.Credentials = new NetworkCredential(username, password);
                }
            }
            #endregion
        }
    }
}
