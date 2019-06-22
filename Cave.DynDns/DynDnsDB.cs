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

using Cave.Data;
using Cave.Data.Mysql;
using Cave.Logging;
using Imscp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Cave.DynDns
{
    /// <summary>
    /// Provides access to the dyn dns database
    /// </summary>
    /// <seealso cref="Cave.Logging.ILogSource" />
    public class DynDnsDB : ILogSource
    {
        readonly IStorage Storage;
        readonly IDatabase DynDnsDatabase;
        readonly IDatabase ImscpDatabase;

        readonly ITable<DynDnsDomain> DynDnsDomains;
        readonly ITable<Admin> Admins;
        readonly ITable<Domain> Domains;
        readonly ITable<DomainDns> DomainEntries;

        /// <summary>Gets the name of the log source.</summary>
        /// <value>The name of the log source.</value>
        public string LogSourceName => "DynDnsDB";

        /// <summary>Initializes a new instance of the <see cref="DynDnsDB"/> class.</summary>
        /// <param name="connection">The connection.</param>
        public DynDnsDB(ConnectionString connection)
        {
            Storage = new MySqlStorage(connection, DbConnectionOptions.AllowUnsafeConnections);
            
            DynDnsDatabase = Storage.GetDatabase("cave", true);
            DynDnsDomains = DynDnsDatabase.GetTable<DynDnsDomain>(TableFlags.AllowCreate);

            ImscpDatabase = Storage.GetDatabase("imscp", false);
            Admins = ImscpDatabase.GetTable<Admin>();
            Domains = ImscpDatabase.GetTable<Domain>();
            DomainEntries = ImscpDatabase.GetTable<DomainDns>();
        }

        /// <summary>Closes this instance.</summary>
        public void Close()
        {
            ImscpDatabase.Close();
            DynDnsDatabase.Close();
            Storage.Close();
        }

        /// <summary>Gets the user.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">userName</exception>
        public DynDnsDomain GetUser(string userName)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException(nameof(userName));
            userName = userName.Trim('.') + ".";
            var domains = DynDnsDomains.GetStructs(Search.FieldEquals(nameof(DynDnsDomain.Username), userName));
            if (domains.Count == 1) return domains[0];
            this.LogDebug("Domain <red>{0}<default> is not present!", userName);
            return default(DynDnsDomain);
        }

        /// <summary>Updates the domain entry address.</summary>
        /// <param name="dom">The domain.</param>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// </exception>
        public string UpdateDomainEntryAddress(DynDnsDomain dom, IPAddress address)
        {
            List<string> errors = new List<string>();
            int updates = 0;
            var entries = DomainEntries.GetStructs(Search.FieldLike(nameof(DomainDns.NameAndTTL), dom.Username + "\t%"));
            foreach (var entry in entries)
            {
                bool update = IsSameDomainType(entry, address) && (entry.DomainText != address.ToString());
                if (update)
                {
                    DomainDns domainEntry = entry;
                    if (domainEntry.DomainText != address.ToString())
                    {
                        if (domainEntry.Status != "ok") throw new Exception(string.Format("Cannot update domain {0} status {1}", domainEntry.Name, domainEntry.Status));

                        Domain domain = Domains[domainEntry.DomainID];
                        if (domain.Status != "ok") throw new Exception(string.Format("Cannot update domain {0} status {1}", domain.Name, domain.Status));

                        domainEntry.DomainText = address.ToString();
                        domainEntry.Status = "tochange";
                        try { DomainEntries.Update(domainEntry); }
                        catch (Exception ex)
                        {
                            string msg = string.Format("Cannot update domain {0}", domainEntry);
                            this.LogWarning(ex, msg);
                            errors.Add(msg);
                            continue;
                        }
                        domain.Status = "tochange";
                        Domains.Update(domain);
                        this.LogInfo("Update {0}", domainEntry);
                    }
                    updates++;
                }
            }
            if (errors.Count == 0)
            {
                return $"{updates} datasets updated.";
            }
            return $"{updates} datasets updated - {errors.Count} Errors.\n" + errors.JoinNewLine();
        }

        /// <summary>
        /// Determines whether [is same domain type] [the specified domain DNS].
        /// </summary>
        /// <param name="domainDns">The domain DNS.</param>
        /// <param name="address">The address.</param>
        /// <returns><c>true</c> if [is same domain type] [the specified domain DNS]; otherwise, <c>false</c>.</returns>
        public bool IsSameDomainType(DomainDns domainDns, IPAddress address)
        {
            if (domainDns.DomainClass != "IN") return false;
            if ((domainDns.DomainType == "A") && (address.AddressFamily == AddressFamily.InterNetwork)) return true;
            if ((domainDns.DomainType == "AAAA") && (address.AddressFamily == AddressFamily.InterNetworkV6)) return true;
            return false;
        }

        /// <summary>Checks the domain entries.</summary>
        /// <returns></returns>
        public List<Credentials> CheckDomainEntries()
        {
            foreach (DynDnsDomain dynDnsDomain in DynDnsDomains.GetStructs())
            {
                var domainDns = DomainEntries.GetStructs(Search.FieldLike(nameof(DomainDns.NameAndTTL), dynDnsDomain.Username + "\t%"));
                bool ok = domainDns.Any(d => d.DomainClass == "IN" && (d.DomainType == "A" || d.DomainType == "AAAA"));
                if (!ok)
                {
                    DynDnsDomains.Delete(dynDnsDomain.ID);
                    this.LogWarning("Deleted dyndns account for domain <yellow>{0}", dynDnsDomain.Username);
                }
            }

            List<Credentials> newUsers = new List<Credentials>();
            foreach (DomainDns domainDns in DomainEntries.GetStructs())
            {
                if (domainDns.DomainClass != "IN") continue;
                if ((domainDns.DomainType != "A") && (domainDns.DomainType != "AAAA")) continue;

                string[] parts = domainDns.Name.Split(new char[] { '.' } , StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 3) continue;
                switch (parts[0])
                {
                    case "wildcard":
                    case "autoconfig": continue;
                }
                long id = CaveSystemData.CalculateID(domainDns.Name);
                if (!DynDnsDomains.Exist(id))
                {
                    Domain domain = Domains[domainDns.DomainID];
                    Admin admin = Admins[domain.AdminID];
                    Credentials credentials = new Credentials(id, admin.Email, domainDns);
                    DynDnsDomains.Replace(credentials.DynDnsDomain);
                    newUsers.Add(credentials);
                    this.LogInfo("Created new credentials for domain <cyan>{0}", domainDns.Name);
                }
            }
            return newUsers;
        }
    }
}
