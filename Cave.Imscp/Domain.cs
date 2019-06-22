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
    /// Table structure imscp.domain
    /// </summary>
    [Table("domain")]
    public struct Domain
    {
        /// <summary>
        /// [ID, AutoIncrement] uint domain.domain_id [10]
        /// </summary>
        [Field(Flags = FieldFlags.ID | FieldFlags.AutoIncrement, Name = "domain_id")]
        public uint ID;

        /// <summary>
        /// string domain.domain_name [200]
        /// </summary>
        [Field(Name = "domain_name", Length = 200)]
        public string Name;

        /// <summary>
        /// uint domain.domain_admin_id [10]
        /// </summary>
        [Field(Name = "domain_admin_id")]
        public uint AdminID;

        /// <summary>
        /// uint domain.domain_created [10]
        /// </summary>
        [Field(Name = "domain_created")]
        public uint Created;

        /// <summary>
        /// uint domain.domain_expires [10]
        /// </summary>
        [Field(Name = "domain_expires")]
        public uint Expires;

        /// <summary>
        /// uint domain.domain_last_modified [10]
        /// </summary>
        [Field(Name = "domain_last_modified")]
        public uint LastModified;

        /// <summary>
        /// int domain.domain_mailacc_limit [11]
        /// </summary>
        [Field(Name = "domain_mailacc_limit")]
        public int MailaccLimit;

        /// <summary>
        /// int domain.domain_ftpacc_limit [11]
        /// </summary>
        [Field(Name = "domain_ftpacc_limit")]
        public int FtpaccLimit;

        /// <summary>
        /// long domain.domain_traffic_limit [20]
        /// </summary>
        [Field(Name = "domain_traffic_limit")]
        public long TrafficLimit;

        /// <summary>
        /// int domain.domain_sqld_limit [11]
        /// </summary>
        [Field(Name = "domain_sqld_limit")]
        public int SqldLimit;

        /// <summary>
        /// int domain.domain_sqlu_limit [11]
        /// </summary>
        [Field(Name = "domain_sqlu_limit")]
        public int SqluLimit;

        /// <summary>
        /// string domain.domain_status [255]
        /// </summary>
        [Field(Name = "domain_status", Length = 255)]
        public string Status;

        /// <summary>
        /// int domain.domain_alias_limit [11]
        /// </summary>
        [Field(Name = "domain_alias_limit")]
        public int AliasLimit;

        /// <summary>
        /// int domain.domain_subd_limit [11]
        /// </summary>
        [Field(Name = "domain_subd_limit")]
        public int SubdLimit;

        /// <summary>
        /// uint domain.domain_ip_id [10]
        /// </summary>
        [Field(Name = "domain_ip_id")]
        public uint IpID;

        /// <summary>
        /// ulong domain.domain_disk_limit [20]
        /// </summary>
        [Field(Name = "domain_disk_limit")]
        public ulong DiskLimit;

        /// <summary>
        /// ulong domain.domain_disk_usage [20]
        /// </summary>
        [Field(Name = "domain_disk_usage")]
        public ulong DiskUsage;

        /// <summary>
        /// ulong domain.domain_disk_file [20]
        /// </summary>
        [Field(Name = "domain_disk_file")]
        public ulong DiskFile;

        /// <summary>
        /// ulong domain.domain_disk_mail [20]
        /// </summary>
        [Field(Name = "domain_disk_mail")]
        public ulong DiskMail;

        /// <summary>
        /// ulong domain.domain_disk_sql [20]
        /// </summary>
        [Field(Name = "domain_disk_sql")]
        public ulong DiskSql;

        /// <summary>
        /// string domain.domain_php [15]
        /// </summary>
        [Field(Name = "domain_php", Length = 15)]
        public string Php;

        /// <summary>
        /// string domain.domain_cgi [15]
        /// </summary>
        [Field(Name = "domain_cgi", Length = 15)]
        public string Cgi;

        /// <summary>
        /// string domain.allowbackup [12]
        /// </summary>
        [Field(Name = "allowbackup", Length = 12)]
        public string Allowbackup;

        /// <summary>
        /// string domain.domain_dns [15]
        /// </summary>
        [Field(Name = "domain_dns", Length = 15)]
        public string Dns;

        /// <summary>
        /// string domain.domain_software_allowed [15]
        /// </summary>
        [Field(Name = "domain_software_allowed", Length = 15)]
        public string SoftwareAllowed;

        /// <summary>
        /// string domain.phpini_perm_system [20]
        /// </summary>
        [Field(Name = "phpini_perm_system", Length = 20)]
        public string PhpiniPermSystem;

        /// <summary>
        /// string domain.phpini_perm_allow_url_fopen [20]
        /// </summary>
        [Field(Name = "phpini_perm_allow_url_fopen", Length = 20)]
        public string PhpiniPermAllowUrlFopen;

        /// <summary>
        /// string domain.phpini_perm_display_errors [20]
        /// </summary>
        [Field(Name = "phpini_perm_display_errors", Length = 20)]
        public string PhpiniPermDisplayErrors;

        /// <summary>
        /// string domain.phpini_perm_disable_functions [20]
        /// </summary>
        [Field(Name = "phpini_perm_disable_functions", Length = 20)]
        public string PhpiniPermDisableFunctions;

        /// <summary>
        /// string domain.phpini_perm_mail_function [20]
        /// </summary>
        [Field(Name = "phpini_perm_mail_function", Length = 20)]
        public string PhpiniPermMailFunction;

        /// <summary>
        /// string domain.domain_external_mail [15]
        /// </summary>
        [Field(Name = "domain_external_mail", Length = 15)]
        public string ExternalMail;

#pragma warning disable 0649
        /// <summary>
        /// string domain.external_mail [15]
        /// </summary>
        [Field(Name = "external_mail", Length = 15)]
        string external_mail;
#pragma warning restore 0649

        /// <summary>
        /// string domain.web_folder_protection [5]
        /// </summary>
        [Field(Name = "web_folder_protection", Length = 5)]
        public string WebFolderProtection;

        /// <summary>
        /// ulong domain.mail_quota [20]
        /// </summary>
        [Field(Name = "mail_quota")]
        public ulong MailQuota;

        /// <summary>
        /// string domain.document_root [255]
        /// </summary>
        [Field(Name = "document_root", Length = 255)]
        public string DocumentRoot;

        /// <summary>
        /// string domain.url_forward [255]
        /// </summary>
        [Field(Name = "url_forward", Length = 255)]
        public string UrlForward;

        /// <summary>
        /// string domain.type_forward [5]
        /// </summary>
        [Field(Name = "type_forward", Length = 5)]
        public string TypeForward;

        /// <summary>
        /// string domain.host_forward [3]
        /// </summary>
        [Field(Name = "host_forward", Length = 3)]
        public string HostForward;


        /// <summary>Returns a <see cref="string" /> that represents this instance.</summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"[{ID}] {Name}";
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. </returns>
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        /// <summary>Determines whether the specified <see cref="object" />, is equal to this instance.</summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Domain)
            {
                Domain other = (Domain)obj;
                return ID == other.ID
                    && Name == other.Name
                    && AdminID == other.AdminID
                    && Created == other.Created
                    && Expires == other.Expires
                    && LastModified == other.LastModified
                    && MailaccLimit == other.MailaccLimit
                    && FtpaccLimit == other.FtpaccLimit
                    && TrafficLimit == other.TrafficLimit
                    && SqldLimit == other.SqldLimit
                    && SqluLimit == other.SqluLimit
                    && Status == other.Status
                    && AliasLimit == other.AliasLimit
                    && SubdLimit == other.SubdLimit
                    && IpID == other.IpID
                    && DiskLimit == other.DiskLimit
                    && DiskUsage == other.DiskUsage
                    && DiskFile == other.DiskFile
                    && DiskMail == other.DiskMail
                    && DiskSql == other.DiskSql
                    && Php == other.Php
                    && Cgi == other.Cgi
                    && Allowbackup == other.Allowbackup
                    && Dns == other.Dns
                    && SoftwareAllowed == other.SoftwareAllowed
                    && PhpiniPermSystem == other.PhpiniPermSystem
                    && PhpiniPermAllowUrlFopen == other.PhpiniPermAllowUrlFopen
                    && PhpiniPermDisplayErrors == other.PhpiniPermDisplayErrors
                    && PhpiniPermDisableFunctions == other.PhpiniPermDisableFunctions
                    && PhpiniPermMailFunction == other.PhpiniPermMailFunction
                    && ExternalMail == other.ExternalMail
                    && external_mail == other.external_mail
                    && WebFolderProtection == other.WebFolderProtection
                    && MailQuota == other.MailQuota
                    && DocumentRoot == other.DocumentRoot
                    && UrlForward == other.UrlForward
                    && TypeForward == other.TypeForward
                    && HostForward == other.HostForward;
            }
            return false;
        }
    }
}
