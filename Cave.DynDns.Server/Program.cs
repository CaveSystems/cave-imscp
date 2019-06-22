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

using Cave.IO;
using Cave.Logging;
using Cave.Net;
using Cave.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cave.DynDns.Server
{
    class Program : ServiceProgram
    {
#if DEBUG
        const int Timeout = 60 * 1000;
#else
        const int Timeout = 5 * 1000;
#endif

        TcpServer m_Server = new TcpServer();
        DynDnsDB m_DnsDB;
        Dictionary<long, long> m_DomainSerials = new Dictionary<long, long>();
        string m_MyHostName;
        string m_MailSender;
        SmtpClient m_SmtpClient;
        string m_Greeting;

        void LoadConfig()
        {
            IniReader config = IniReader.FromLocation(RootLocation.AllUserConfig);

            #region HOST NAME
            m_MyHostName = config.ReadSetting("HOST", "NAME");
            if (string.IsNullOrEmpty(m_MyHostName))
            {
                m_MyHostName = NetTools.HostName;
                this.LogInfo("[HOST] NAME setting not set, using " + m_MyHostName);
            }
            #endregion

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
            m_Greeting = string.Join("\r\n", config.ReadSection("GREETING", false));
            #endregion

            #region DB CONNECTION
            string connectionString = config.ReadSetting("IMSCP", "CONNECTION");
            m_DnsDB = new DynDnsDB(connectionString);
            this.LogInfo("Connected to database");
            #endregion

            #region PORT
            string portString = config.ReadSetting("HOST", "PORT");
            if (!int.TryParse(portString, out int port) || (port < 0)) port = 8246;
            m_Server.ClientAccepted += ClientAccepted; ;
            m_Server.Listen(port);
            this.LogInfo("Listening at " + m_Server.LocalEndPoint + " for dynamic updates...");
            #endregion
        }

        protected override void OnKeyPressed(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.Escape: ServiceParameters.CommitShutdown(); break;
            }
        }

        protected override void Worker()
        {
            LoadConfig();

            DateTime nextDomainCheck = new DateTime();
            while (!ServiceParameters.Shutdown)
            {
                Thread.Sleep(1000);

                if (DateTime.Now > nextDomainCheck)
                {
                    nextDomainCheck = DateTime.Now.AddMinutes(1);
                    var users = m_DnsDB.CheckDomainEntries();
                    foreach (Credentials user in users)
                    {
                        try
                        {
                            this.LogInfo("New user <green>{0}<default> created sending password to <yellow>{1}<default>.", user.DynDnsDomain.Username, user.Email);
                            string body = m_Greeting.
                                Replace("%USERNAME%", user.DynDnsDomain.Username).
                                Replace("%PASSWORD%", user.PlainPassword).
                                Replace("%DOMAIN%", user.DomainDns.ToString());
                            m_SmtpClient.Send(m_MailSender, user.Email, "Your CaveSystems DnyDns Account", body);
                        }
                        catch (Exception ex)
                        {
                            this.LogError(ex, "Error while sending mail");
                        }
                    }
                }
            }
        }

        void ClientAccepted(object sender, TcpServerClientEventArgs<TcpAsyncClient> e)
        {
            Task.Factory.StartNew(ReadClient, e);
        }

        void ReadClient(object state)
        {
            var e = (TcpServerClientEventArgs<TcpAsyncClient>)state;
            e.Client.Stream.ReadTimeout = Timeout;
            e.Client.Stream.WriteTimeout = Timeout;
            StreamReader reader = new StreamReader(e.Client.Stream, Encoding.ASCII);
            StreamWriter writer = new StreamWriter(e.Client.Stream, Encoding.ASCII);
            string serverSalt = Base64.NoPadding.Encode(DefaultRNG.Get(128));
            writer.WriteLine("* Cave.DnsDns.Server ready");
            writer.Flush();

            DynDnsDomain dynDnsDomain = default(DynDnsDomain);
            bool loggedIn = false;

            while (e.Client.IsConnected)
            {
                string[] parts;
                try { parts = reader.ReadLine().Split(' '); }
                catch { break; }
                switch (parts[0])
                {
                    default:
                    {
                        this.LogWarning(e.Client + " Invalid command received!");
                        writer.WriteLine("ERR Invalid or unknown command!");
                        writer.Flush();
                    }
                    break;
                    case "LOGIN":
                    {
                        dynDnsDomain = m_DnsDB.GetUser(parts[1]);
                        if (dynDnsDomain.ID <= 0)
                        {
                            writer.WriteLine("ERR LOGIN Invalid or unknown user!");
                            this.LogWarning(e.Client + " Invalid login received!");
                        }
                        else
                        {
                            this.LogDebug(e.Client + " login user " + dynDnsDomain);
                            writer.WriteLine("USERSALT " + dynDnsDomain.Salt);
                            writer.WriteLine("SERVERSALT " + serverSalt);
                        }
                        writer.Flush();
                    }
                    break;

                    case "PASSWORD":
                    {
                        loggedIn = dynDnsDomain.CheckPassword(serverSalt, parts[1]);
                        if (loggedIn)
                        {
                            this.LogInfo("<green>{0}<default> Login {1}", e.Client, dynDnsDomain.Username);
                            writer.WriteLine("OK LOGIN");
                            writer.Flush();
                        }
                        else
                        {
                            writer.WriteLine("ERR LOGIN Invalid password!");
                            writer.Flush();
                            this.LogInfo("<red>{0}<default> Invalid password received!", e.Client);
                            break;
                        }
                    }
                    break;

                    #region UPDATE [<IPADDRESS>]
                    case "UPDATE":
                    {
                        if (!loggedIn)
                        {
                            writer.WriteLine("NO UPDATE Please log in first!");
                            writer.Flush();
                            break;
                        }

                        IPAddress address;
                        if (parts.Length > 1)
                        {
                            if (!IPAddress.TryParse(parts[1], out address))
                            {
                                writer.WriteLine("NO UPDATE ipaddress '" + parts[2] + "' is not valid!");
                                writer.Flush();
                                break;
                            }
                        }
                        else
                        {
                            string s = e.Client.RemoteEndPoint.Address.ToString();
                            if (s.StartsWith("::ffff:"))
                            {
                                address = IPAddress.Parse(s.Substring(s.LastIndexOf(':') + 1));
                            }
                            else
                            {
                                address = e.Client.RemoteEndPoint.Address;
                            }
                        }
                        string result = m_DnsDB.UpdateDomainEntryAddress(dynDnsDomain, address).ToString();
                        // check for same Type
                        if (result == null)
                        {
                            writer.WriteLine($"NO UPDATE no domains found matching {dynDnsDomain.Username}.");
                            writer.Flush();
                            break;
                        }
                        writer.WriteLine("OK UPDATE " + result);
                        writer.Flush();
                        this.LogNotice(e.Client + " Update " + dynDnsDomain.Username + " " + result);
                    }
                    break;
                    #endregion
                }
            }
        }

        static void Main(string[] args)
        {
            new Program().Run();
        }
    }
}
