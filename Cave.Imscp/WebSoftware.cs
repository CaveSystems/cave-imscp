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
    /// Table structure imscp.web_software
    /// </summary>
    [Table("web_software")]
    public struct WebSoftware
    {
        /// <summary>
        /// [ID, AutoIncrement] uint web_software.software_id [10]
        /// </summary>
        [Field(Flags = FieldFlags.ID | FieldFlags.AutoIncrement, Name = "software_id")]
        public uint SoftwareID;

        /// <summary>
        /// uint web_software.software_master_id [10]
        /// </summary>
        [Field(Name = "software_master_id")]
        public uint SoftwareMasterID;

        /// <summary>
        /// uint web_software.reseller_id [10]
        /// </summary>
        [Field(Name = "reseller_id")]
        public uint ResellerID;

        /// <summary>
        /// string web_software.software_installtype [15]
        /// </summary>
        [Field(Name = "software_installtype", Length = 15)]
        public string SoftwareInstalltype;

        /// <summary>
        /// string web_software.software_name [100]
        /// </summary>
        [Field(Name = "software_name", Length = 100)]
        public string SoftwareName;

        /// <summary>
        /// string web_software.software_version [20]
        /// </summary>
        [Field(Name = "software_version", Length = 20)]
        public string SoftwareVersion;

        /// <summary>
        /// string web_software.software_language [15]
        /// </summary>
        [Field(Name = "software_language", Length = 15)]
        public string SoftwareLanguage;

        /// <summary>
        /// string web_software.software_type [20]
        /// </summary>
        [Field(Name = "software_type", Length = 20)]
        public string SoftwareType;

        /// <summary>
        /// bool web_software.software_db [1]
        /// </summary>
        [Field(Name = "software_db")]
        public bool Software;

        /// <summary>
        /// string web_software.software_archive [100]
        /// </summary>
        [Field(Name = "software_archive", Length = 100)]
        public string SoftwareArchive;

        /// <summary>
        /// string web_software.software_installfile [100]
        /// </summary>
        [Field(Name = "software_installfile", Length = 100)]
        public string SoftwareInstallfile;

        /// <summary>
        /// string web_software.software_prefix [50]
        /// </summary>
        [Field(Name = "software_prefix", Length = 50)]
        public string SoftwarePrefix;

        /// <summary>
        /// string web_software.software_link [100]
        /// </summary>
        [Field(Name = "software_link", Length = 100)]
        public string SoftwareLink;

        /// <summary>
        /// string web_software.software_desc [1,677722E+07]
        /// </summary>
        [Field(Name = "software_desc")]
        public string SoftwareDesc;

        /// <summary>
        /// int web_software.software_active [1]
        /// </summary>
        [Field(Name = "software_active")]
        public int SoftwareActive;

        /// <summary>
        /// string web_software.software_status [15]
        /// </summary>
        [Field(Name = "software_status", Length = 15)]
        public string SoftwareStatus;

        /// <summary>
        /// uint web_software.rights_add_by [10]
        /// </summary>
        [Field(Name = "rights_add_by")]
        public uint RightsAddBy;

        /// <summary>
        /// string web_software.software_depot [15]
        /// </summary>
        [Field(Name = "software_depot", Length = 15)]
        public string SoftwareDepot;


        /// <summary>Returns a <see cref="string" /> that represents this instance.</summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"[{SoftwareID}] {SoftwareInstalltype}";
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. </returns>
        public override int GetHashCode()
        {
            return SoftwareID.GetHashCode();
        }

        /// <summary>Determines whether the specified <see cref="object" />, is equal to this instance.</summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is WebSoftware)
            {
                WebSoftware other = (WebSoftware)obj;
                return SoftwareID == other.SoftwareID
                    && SoftwareMasterID == other.SoftwareMasterID
                    && ResellerID == other.ResellerID
                    && SoftwareInstalltype == other.SoftwareInstalltype
                    && SoftwareName == other.SoftwareName
                    && SoftwareVersion == other.SoftwareVersion
                    && SoftwareLanguage == other.SoftwareLanguage
                    && SoftwareType == other.SoftwareType
                    && Software == other.Software
                    && SoftwareArchive == other.SoftwareArchive
                    && SoftwareInstallfile == other.SoftwareInstallfile
                    && SoftwarePrefix == other.SoftwarePrefix
                    && SoftwareLink == other.SoftwareLink
                    && SoftwareDesc == other.SoftwareDesc
                    && SoftwareActive == other.SoftwareActive
                    && SoftwareStatus == other.SoftwareStatus
                    && RightsAddBy == other.RightsAddBy
                    && SoftwareDepot == other.SoftwareDepot;
            }
            return false;
        }
    }
}
