using Cave;
using Cave.Collections.Generic;
using Cave.IO;
using Cave.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace InternetX
{
    public class Autodns : ILogSource
    {
        const string XMLHEADER = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

        const string AUTODNSURI = "https://gateway.autodns.com/";

        InternetXConfig m_Config;
        CommandCodeLookup m_CommandCodeLookup = new CommandCodeLookup();

        public string LogSourceName
        {
            get
            {
                return "Autodns " + m_Config.UserName;
            }
        }

        XElement CreateRequest(IEnumerable<XElement> tasks)
        {
            XElement requestnode = new XElement("request",
                new XElement("auth",
                    new XElement("user", m_Config.UserName),
                    new XElement("password", m_Config.Password),
                    new XElement("context", m_Config.Context)
                    ),
                tasks
            );
            return requestnode;
        }

        XElement CreateRequest(params XElement[] tasks)
        {
            return CreateRequest((IEnumerable<XElement>)tasks);
        }

        XElement CreateSearch(string key, string op, string value)
        {
            return new XElement("where",
                new XElement("key", key),
                new XElement("operator", op),
                new XElement("value", value)
            );
        }

        XElement CreateTask(CommandCode command, params object[] parameters)
        {
            return new XElement("task", new XElement("code", m_CommandCodeLookup[command]), parameters);
        }

        string CreateZoneInquire(IEnumerable<string> zones)
        {
            List<XElement> tasks = new List<XElement>();                
            foreach(string zone in zones)
            {
                tasks.Add(CreateTask(CommandCode.ZoneInquire, new XElement("zone", new XElement("name", zone))));
            }            
            return XMLHEADER + Environment.NewLine + CreateRequest(tasks).ToString();
        }

        string CreateZoneSearch(string pattern)
        {
            return XMLHEADER + Environment.NewLine + CreateRequest(CreateTask(CommandCode.ZoneInquire, CreateSearch("name", "like", pattern))).ToString();
        }

        string Post(string request)
        {
            WebRequest webrequest = WebRequest.Create(AUTODNSURI);
            webrequest.Method = "POST";
            webrequest.ContentType = "text/xml";
            byte[] postData = Encoding.UTF8.GetBytes(request);
            webrequest.ContentLength = postData.Length;
            using (Stream reqStream = webrequest.GetRequestStream())
            {
                reqStream.Write(postData, 0, postData.Length);
            }
            WebResponse response = webrequest.GetResponse();
            using (Stream respStream = response.GetResponseStream())
            {
                return Encoding.UTF8.GetString(respStream.ReadAllBytes());
            }
        }

        XElement[] ParseRecords(IEnumerable<DnsResourceRecord> records)
        {
            List<XElement> list = new List<XElement>();
            foreach (DnsResourceRecord record in records)
            {
                switch (record.Type)
                {
                    case "MX":
                    case "SRV":
                        list.Add(new XElement("rr",
                            new XElement("name", record.Name),
                            new XElement("pref", record.Pref),
                            new XElement("type", record.Type),
                            new XElement("value", record.Value),
                            new XElement("ttl", record.TTL.ToString())
                        ));
                        break;
                    default:
                            list.Add(new XElement("rr",
                            new XElement("name", record.Name),
                            new XElement("type", record.Type),
                            new XElement("value", record.Value),
                            new XElement("ttl", record.TTL.ToString())
                        ));
                        break;
                }
            }
            return list.ToArray();
        }

        XElement[] CreateDefaultNameServerRecords()
        {
            return new XElement[4]
            {
                new XElement("nserver", new XElement("name", "a.ns14.net")),
                new XElement("nserver", new XElement("name", "b.ns14.net")),
                new XElement("nserver", new XElement("name", "c.ns14.net")),
                new XElement("nserver", new XElement("name", "d.ns14.net")),
            };
        }

        string CreateZoneUpdate(string zone, IPAddress mainIP, IEnumerable<DnsResourceRecord> records)
        {
            XElement zonenode = new XElement("zone",
                new XElement("name", zone),
                CreateDefaultNameServerRecords(),
                new XElement("main",
                    new XElement("value", mainIP)
                ),
                new XElement("www_include", "0"),
                new XElement("soa",
                    new XElement("level", "3"),
                    new XElement("email", "postmaster@" + zone)
                ),
                ParseRecords(records)
            );

            return XMLHEADER + CreateRequest(CreateTask(CommandCode.ZoneUpdate, zonenode)).ToString();
        }

        public Autodns(InternetXConfig config)
        {
            m_Config = config;
        }

        public string[] ZoneSearch(string pattern)
        {
            this.LogDebug("ZoneSearch <magenta>{0}", pattern);
            string reqString = CreateZoneSearch(pattern);
            string result = Post(reqString);

            XElement root = XElement.Parse(result);
            if (root.Name != "response") throw new Exception("Unknown response!");
            if (root.XPathSelectElement("./result/status/type").Value != "success")
            {
                var e = new Exception("InternetX AutoDNS Error: " + root.XPathSelectElement("./result/msg/text").Value);
                e.Data.Add("Request", reqString);
                e.Data.Add("Answer", result);
                throw e;
            }

            Set<string> domains = new Set<string>();
            foreach(var zone in root.XPathSelectElements("./result/data/zone"))
            {
                string name = zone.XPathSelectElement("./name").Value;
                domains.Add(name);
            }
            return domains.ToArray();
        }

        public AutodnsRecordList ZoneInquire(IEnumerable<string> zones)
        {
            this.LogDebug("ZoneInquire <cyan>{0}", string.Join("<default>, <cyan>", zones));
            AutodnsRecordList result = new AutodnsRecordList();
            string reqString = CreateZoneInquire(zones);
            string postResult = Post(reqString);

            XElement root = XElement.Parse(postResult);
            if (root.Name != "response") throw new Exception("Unknown response!");
            if (root.XPathSelectElement("./result/status/type").Value != "success")
            {
                var e = new Exception("InternetX AutoDNS Error: " + root.XPathSelectElement("./result/msg/text").Value);
                e.Data.Add("Request", reqString);
                e.Data.Add("Answer", postResult);
                throw e;
            }

            foreach (var zone in root.XPathSelectElements("./result/data/zone"))
            {
                string zoneName = zone.XPathSelectElement("./name").Value;
                List<DnsResourceRecord> list = new List<DnsResourceRecord>();
                foreach (var rr in zone.XPathSelectElements("./rr"))
                {
                    XElement pref = rr.XPathSelectElement("./pref");
                    XElement ttl = rr.XPathSelectElement("./ttl");
                    XElement type = rr.XPathSelectElement("./type");
                    XElement name = rr.XPathSelectElement("./name");
                    XElement value = rr.XPathSelectElement("./value");

                    DnsResourceRecord record = new DnsResourceRecord();
                    if (pref == null) record.Pref = 0; else uint.TryParse(pref.Value, out record.Pref);
                    if (ttl == null) record.TTL = 360; else uint.TryParse(ttl.Value, out record.TTL);
                    record.Type = type.Value;
                    if (name == null || string.IsNullOrEmpty(name.Value))
                    {
                        record.Name = null;
                    }
                    else
                    {
                        record.Name = name.Value;
                    }

                    record.Value = value == null ? "" : value.Value;
                    list.Add(record);
                }
                result[zoneName] = new AutodnsRecordItem(this, zoneName, list.ToArray());
            }

            return result;
        }

        public AutodnsRecordList ZoneInquire(params string[] zones)
        {
            return ZoneInquire((IEnumerable<string>) zones);
        }

        public void ZoneUpdate(string zone, IPAddress address, IEnumerable<DnsResourceRecord> records)
        {
            this.LogDebug("ZoneUpdate <cyan>{0} <magenta>{1}", zone, address);
            string reqString = CreateZoneUpdate(zone, address, records);
            string postResult = Post(reqString);
            XElement root = XElement.Parse(postResult);
            if (root.Name != "response") throw new Exception("Unknown response!");
            if (root.XPathSelectElement("./result/status/type").Value != "success")
            {
                var e = new Exception("InternetX AutoDNS Error: " + root.XPathSelectElement("./result/msg/text").Value);
                e.Data.Add("Request", reqString);
                e.Data.Add("Answer", postResult);
                throw e;
            }
        }

        public override string ToString()
        {
            return LogSourceName;
        }
    }
}
