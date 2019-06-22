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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Cave.DynDns.Client
{
    static class Program
    {
        static bool verbose;
        static IPAddress ServerIPv4 { get; } = IPAddress.Parse("148.251.252.83");
        static IPAddress ServerIPv6 { get; } = IPAddress.Parse("2a01:4f8:190:4349::2000");

        static void Usage()
        {
            Console.WriteLine("Usage: cave-dyndns-client <@commandfile|command> [--verbose]");
            Console.WriteLine();
            Console.WriteLine("Valid commands:");
            Console.WriteLine("  update domainname password");
            Console.WriteLine("  update domainname password ipaddress");
            Console.WriteLine();
            Console.WriteLine("A commandfile contains one command per line and is executed sequentially.");
            Console.WriteLine();
            Console.WriteLine("ReturnCodes:");
            Console.WriteLine("  0: No error");
            Console.WriteLine("  1: Error");
        }

        static void Update(string domain, string password, string ip)
        {
            string mainDomain = domain.Substring(domain.IndexOf('.')).Trim('.');
            try
            {
                Update(ServerIPv4, mainDomain, domain, password, ip);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error: Cannot update ipv4 address");
                if (verbose) Console.WriteLine(ex.Message);
            }
            try
            {
                Update(ServerIPv6, mainDomain, domain, password, ip);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error: Cannot update ipv6 address");
                if (verbose) Console.WriteLine(ex.Message);
            }
        }

        private static void Update(IPAddress serverIP, string mainDomain, string domain, string password, string targetIP)
        {
            if (verbose) Console.WriteLine($"Connecting to {mainDomain}");
            TcpClient client = new TcpClient(serverIP.AddressFamily);
            client.Connect(serverIP, 8246);
#if !DEBUG
            client.ReceiveTimeout = 5000;
            client.SendTimeout = 5000;
#endif
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream, Encoding.ASCII);
            StreamWriter writer = new StreamWriter(stream, Encoding.ASCII);
            string greeting = reader.ReadLine();
            if (verbose) Console.WriteLine($"Received {greeting}");
            if (!greeting.StartsWith("*"))
            {
                client.Close();
                throw new Exception("Invalid server greeting!");
            }
            if (verbose) Console.WriteLine($"Login {domain}");
            writer.WriteLine("LOGIN " + domain);
            writer.Flush();
            string answerUserSalt = reader.ReadLine();
            if (!answerUserSalt.StartsWith("USERSALT"))
            {
                client.Close();
                throw new Exception(answerUserSalt);
            }
            if (verbose) Console.WriteLine($"Received {answerUserSalt}");
            string answerServerSalt = reader.ReadLine();
            if (!answerServerSalt.StartsWith("SERVERSALT"))
            {
                client.Close();
                throw new Exception(answerServerSalt);
            }
            if (verbose) Console.WriteLine($"Received {answerServerSalt}");

            {
                string userSalt = answerUserSalt.Split(' ')[1];
                string serverSalt = answerServerSalt.Split(' ')[1];

                SHA256Managed SHA256 = new SHA256Managed();
                SHA256.Initialize();
                byte[] data1 = SHA256.ComputeHash(Encoding.UTF8.GetBytes(password + userSalt));
                string string1 = Convert.ToBase64String(data1).TrimEnd('=');

                SHA256 = new SHA256Managed();
                SHA256.Initialize();
                byte[] data2 = SHA256.ComputeHash(Encoding.UTF8.GetBytes(string1 + serverSalt));
                string string2 = Convert.ToBase64String(data2).TrimEnd('=');
                writer.WriteLine("PASSWORD " + string2);
                writer.Flush();
                if (verbose) Console.WriteLine($"Sent password...");
            }

            {
                string answerLogin = reader.ReadLine();
                if (!answerLogin.StartsWith("OK LOGIN"))
                {
                    client.Close();
                    throw new Exception(answerLogin);
                }
                if (verbose) Console.WriteLine($"Received {answerLogin}");
            }

            if (targetIP != null)
            {
                if (verbose) Console.WriteLine($"Sending update request for {targetIP}");
                writer.WriteLine("UPDATE " + targetIP);
                writer.Flush();
            }
            else
            {
                if (verbose) Console.WriteLine($"Sending update request.");
                writer.WriteLine("UPDATE");
                writer.Flush();
            }

            {
                string answerUpdate = reader.ReadLine();
                if (!answerUpdate.StartsWith("OK UPDATE"))
                {
                    client.Close();
                    throw new Exception(answerUpdate);
                }
                Console.WriteLine($"{answerUpdate}");
            }
            client.Close();
        }

        static void Command(string[] parameters)
        {
            switch (parameters[0].ToLower())
            {
                case "update":
                    switch (parameters.Length)
                    {
                        case 3: Update(parameters[1], parameters[2], null); return;
                        case 4: Update(parameters[1], parameters[2], parameters[3]); return;
                        default: throw new Exception("Invalid parameter count!");
                    }
                default: throw new Exception("Unknown command");
            }
        }

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            try
            {
                List<string> parameters = new List<string>();

                verbose = Debugger.IsAttached;
                foreach (string arg in args)
                {
                    if (arg.StartsWith("--"))
                    {
                        if (arg == "--verbose") { verbose = true; continue; }
                        if (arg == "--form")
                        {
                            new CaveDynDnsMain();
                            Application.Run();
                            return 0;
                        }
                        else { Usage(); return 1; }
                    }
                    parameters.Add(arg);
                }

                if (parameters.Count < 1)
                {
                    Usage();
                    return 1;
                }
                if (parameters[0].StartsWith("@"))
                {
                    int i = 0;
                    string file = Path.GetFullPath(parameters[0].Substring(1));
                    foreach (string line in File.ReadAllLines(file))
                    {
                        ++i;
                        try { Command(line.Split(' ')); }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format("Error: Invalid command at line #{0}", i), ex);
                        }
                    }
                    return 0;
                }
                Command(parameters.ToArray());
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return 4;
            }
        }
    }
}