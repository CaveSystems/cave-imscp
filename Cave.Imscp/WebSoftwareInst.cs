#region CopyRight 2017
/*
    Copyright (c) 2017 Andreas Rohleder (andreas@rohleder.cc)
    All rights reserved
*/
#endregion
#region License AGPL
/*
    This program/library/sourcecode is free software; you can redistribute it
    and/or modify it under the terms of the GNU Affero General Public License
    version 3 as published by the Free Software Foundation subsequent called
    the License.

    You may not use this program/library/sourcecode except in compliance
    with the License. The License is included in the LICENSE.AGPL30 file
    found at the installation directory or the distribution package.

    Permission is hereby granted, free of charge, to any person obtaining
    a copy of this software and associated documentation files (the
    'Software'), to deal in the Software without restriction, including
    without limitation the rights to use, copy, modify, merge, publish,
    distribute, sublicense, and/or sell copies of the Software, and to
    permit persons to whom the Software is furnished to do so, subject to
    the following conditions:

    The above copyright notice and this permission notice shall be included
    in all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND,
    EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
    NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
    LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
    OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion
#region Authors & Contributors
/*
   Author:
     Andreas Rohleder <andreas@rohleder.cc>

   Contributors:

 */
#endregion

using System;
using Cave;
using Cave.Data;

namespace Imscp
{
    /// <summary>
    /// Table structure imscp.web_software_inst
    /// </summary>
    [Table("web_software_inst")]
    public struct WebSoftwareInst
    {
        /// <summary>
        /// uint web_software_inst.domain_id [10]
        /// </summary>
        [Field(Name = "domain_id")]
        public uint DomainID;

        /// <summary>
        /// uint web_software_inst.alias_id [10]
        /// </summary>
        [Field(Name = "alias_id")]
        public uint AliasID;

        /// <summary>
        /// uint web_software_inst.subdomain_id [10]
        /// </summary>
        [Field(Name = "subdomain_id")]
        public uint SubdomainID;

        /// <summary>
        /// uint web_software_inst.subdomain_alias_id [10]
        /// </summary>
        [Field(Name = "subdomain_alias_id")]
        public uint SubdomainAliasID;

        /// <summary>
        /// int web_software_inst.software_id [10]
        /// </summary>
        [Field(Name = "software_id")]
        public int SoftwareID;

        /// <summary>
        /// uint web_software_inst.software_master_id [10]
        /// </summary>
        [Field(Name = "software_master_id")]
        public uint SoftwareMasterID;

        /// <summary>
        /// int web_software_inst.software_res_del [1]
        /// </summary>
        [Field(Name = "software_res_del")]
        public int SoftwareResDel;

        /// <summary>
        /// string web_software_inst.software_name [100]
        /// </summary>
        [Field(Name = "software_name", Length = 100)]
        public string SoftwareName;

        /// <summary>
        /// string web_software_inst.software_version [20]
        /// </summary>
        [Field(Name = "software_version", Length = 20)]
        public string SoftwareVersion;

        /// <summary>
        /// string web_software_inst.software_language [15]
        /// </summary>
        [Field(Name = "software_language", Length = 15)]
        public string SoftwareLanguage;

        /// <summary>
        /// string web_software_inst.path [255]
        /// </summary>
        [Field(Name = "path", Length = 255)]
        public string Path;

        /// <summary>
        /// string web_software_inst.software_prefix [50]
        /// </summary>
        [Field(Name = "software_prefix", Length = 50)]
        public string SoftwarePrefix;

#pragma warning disable CS0649  
        /// <summary>
        /// string web_software_inst.db [100]
        /// </summary>
        [Field(Name = "db", Length = 100)]
        string db;
#pragma warning restore CS0649  

        /// <summary>
        /// string web_software_inst.database_user [100]
        /// </summary>
        [Field(Name = "database_user", Length = 100)]
        public string DatabaseUser;

        /// <summary>
        /// string web_software_inst.database_tmp_pwd [100]
        /// </summary>
        [Field(Name = "database_tmp_pwd", Length = 100)]
        public string DatabaseTmpPwd;

        /// <summary>
        /// string web_software_inst.install_username [100]
        /// </summary>
        [Field(Name = "install_username", Length = 100)]
        public string InstallUsername;

        /// <summary>
        /// string web_software_inst.install_password [100]
        /// </summary>
        [Field(Name = "install_password", Length = 100)]
        public string InstallPassword;

        /// <summary>
        /// string web_software_inst.install_email [100]
        /// </summary>
        [Field(Name = "install_email", Length = 100)]
        public string InstallEmail;

        /// <summary>
        /// string web_software_inst.software_status [15]
        /// </summary>
        [Field(Name = "software_status", Length = 15)]
        public string SoftwareStatus;

        /// <summary>
        /// string web_software_inst.software_depot [15]
        /// </summary>
        [Field(Name = "software_depot", Length = 15)]
        public string SoftwareDepot;


        /// <summary>Returns a <see cref="string" /> that represents this instance.</summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"{SoftwareName}";
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. </returns>
        public override int GetHashCode()
        {
            return SoftwareName.GetHashCode();
        }

        /// <summary>Determines whether the specified <see cref="object" />, is equal to this instance.</summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is WebSoftwareInst)
            {
                WebSoftwareInst other = (WebSoftwareInst)obj;
                return DomainID == other.DomainID
                    && AliasID == other.AliasID
                    && SubdomainID == other.SubdomainID
                    && SubdomainAliasID == other.SubdomainAliasID
                    && SoftwareID == other.SoftwareID
                    && SoftwareMasterID == other.SoftwareMasterID
                    && SoftwareResDel == other.SoftwareResDel
                    && SoftwareName == other.SoftwareName
                    && SoftwareVersion == other.SoftwareVersion
                    && SoftwareLanguage == other.SoftwareLanguage
                    && Path == other.Path
                    && SoftwarePrefix == other.SoftwarePrefix
                    && db == other.db
                    && DatabaseUser == other.DatabaseUser
                    && DatabaseTmpPwd == other.DatabaseTmpPwd
                    && InstallUsername == other.InstallUsername
                    && InstallPassword == other.InstallPassword
                    && InstallEmail == other.InstallEmail
                    && SoftwareStatus == other.SoftwareStatus
                    && SoftwareDepot == other.SoftwareDepot;
            }
            return false;
        }
    }
}
