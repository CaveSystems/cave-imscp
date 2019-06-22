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
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace Cave.DynDns.Client
{
    static class CaveDynDnsClient
    {
        public static string Update(string p_Username, string p_Password, string p_IPAddress)
        {
            TcpClient l_Client = new TcpClient("hosting.caveserver.de", 8246);
            NetworkStream l_Stream = l_Client.GetStream();
            StreamReader l_Reader = new StreamReader(l_Stream);
            StreamWriter l_Writer = new StreamWriter(l_Stream);
            string l_Greeting = l_Reader.ReadLine();
            if (!l_Greeting.StartsWith("*"))
            {
                l_Client.Close();
                throw new Exception("Invalid server greeting!");
            }
            #region LOGIN
            {
                l_Writer.WriteLine("LOGIN " + p_Username);
                l_Writer.Flush();
                string l_Answer1 = l_Reader.ReadLine();
                if (!l_Answer1.StartsWith("USERSALT"))
                {
                    l_Client.Close();
                    throw new Exception("Error while logging in (Stage1):\n" + l_Answer1);
                }
                string l_Answer2 = l_Reader.ReadLine();
                if (!l_Answer2.StartsWith("SERVERSALT"))
                {
                    l_Client.Close();
                    throw new Exception("Error while logging in (Stage2):\n" + l_Answer2);
                }
                try
                {
                    string l_UserSalt = l_Answer1.Split(' ')[1];
                    string l_ServerSalt = l_Answer2.Split(' ')[1];

                    SHA256Managed SHA256 = new SHA256Managed();
                    SHA256.Initialize();
                    byte[] l_Data1 = SHA256.ComputeHash(Encoding.UTF8.GetBytes(p_Password + l_UserSalt));
                    string l_String1 = Convert.ToBase64String(l_Data1).TrimEnd('=');

                    SHA256 = new SHA256Managed();
                    SHA256.Initialize();
                    byte[] l_Data2 = SHA256.ComputeHash(Encoding.UTF8.GetBytes(l_String1 + l_ServerSalt));
                    string l_String2 = Convert.ToBase64String(l_Data2).TrimEnd('=');
                    l_Writer.WriteLine("PASSWORD " + l_String2);
                    l_Writer.Flush();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while logging in (Stage3)", ex);
                }
                string l_Answer = l_Reader.ReadLine();
                if (!l_Answer.StartsWith("OK LOGIN"))
                {
                    l_Client.Close();
                    throw new Exception("Error while logging in (Stage4):\n" + l_Answer);
                }
            }
            #endregion
            #region UPDATE
            {
                if (p_IPAddress != null)
                {
                    l_Writer.WriteLine("UPDATE " + p_IPAddress);
                    l_Writer.Flush();
                }
                else
                {
                    l_Writer.WriteLine("UPDATE");
                    l_Writer.Flush();
                }
                string l_Answer = l_Reader.ReadLine();
                l_Client.Close();
                if (!l_Answer.StartsWith("OK UPDATE"))
                {
                    throw new Exception("Error while updating ip address:\n" + l_Answer);
                }
                return l_Answer;
            }
            #endregion
        }

        public static string Command(string[] p_Parts)
        {
            try
            {
                switch (p_Parts[0].ToLower())
                {
                    case "update":
                        switch (p_Parts.Length)
                        {
                            case 3: return Update(p_Parts[1], p_Parts[2], null);
                            case 4: return Update(p_Parts[1], p_Parts[2], p_Parts[3]);
                            default: throw new Exception("Invalid number of arguments to update command!");
                        }
                    default: throw new Exception("Unknown command");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in command syntax!", ex);
            }
        }
    }
}
