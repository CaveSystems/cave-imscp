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
    /// Table structure imscp.web_software_depot
    /// </summary>
    [Table("web_software_depot")]
    public struct WebSoftwareDepot
    {
        /// <summary>
        /// [ID, AutoIncrement] uint web_software_depot.package_id [10]
        /// </summary>
        [Field(Flags = FieldFlags.ID | FieldFlags.AutoIncrement, Name = "package_id")]
        public uint PackageID;

        /// <summary>
        /// string web_software_depot.package_install_type [15]
        /// </summary>
        [Field(Name = "package_install_type", Length = 15)]
        public string PackageInstallType;

        /// <summary>
        /// string web_software_depot.package_title [100]
        /// </summary>
        [Field(Name = "package_title", Length = 100)]
        public string PackageTitle;

        /// <summary>
        /// string web_software_depot.package_version [20]
        /// </summary>
        [Field(Name = "package_version", Length = 20)]
        public string PackageVersion;

        /// <summary>
        /// string web_software_depot.package_language [15]
        /// </summary>
        [Field(Name = "package_language", Length = 15)]
        public string PackageLanguage;

        /// <summary>
        /// string web_software_depot.package_type [20]
        /// </summary>
        [Field(Name = "package_type", Length = 20)]
        public string PackageType;

        /// <summary>
        /// string web_software_depot.package_description [1,677722E+07]
        /// </summary>
        [Field(Name = "package_description")]
        public string PackageDescription;

        /// <summary>
        /// string web_software_depot.package_vendor_hp [100]
        /// </summary>
        [Field(Name = "package_vendor_hp", Length = 100)]
        public string PackageVendorHp;

        /// <summary>
        /// string web_software_depot.package_download_link [100]
        /// </summary>
        [Field(Name = "package_download_link", Length = 100)]
        public string PackageDownloadLink;

        /// <summary>
        /// string web_software_depot.package_signature_link [100]
        /// </summary>
        [Field(Name = "package_signature_link", Length = 100)]
        public string PackageSignatureLink;


        /// <summary>Returns a <see cref="string" /> that represents this instance.</summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"[{PackageID}] {PackageInstallType}";
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. </returns>
        public override int GetHashCode()
        {
            return PackageID.GetHashCode();
        }

        /// <summary>Determines whether the specified <see cref="object" />, is equal to this instance.</summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is WebSoftwareDepot)
            {
                WebSoftwareDepot other = (WebSoftwareDepot)obj;
                return PackageID == other.PackageID
                    && PackageInstallType == other.PackageInstallType
                    && PackageTitle == other.PackageTitle
                    && PackageVersion == other.PackageVersion
                    && PackageLanguage == other.PackageLanguage
                    && PackageType == other.PackageType
                    && PackageDescription == other.PackageDescription
                    && PackageVendorHp == other.PackageVendorHp
                    && PackageDownloadLink == other.PackageDownloadLink
                    && PackageSignatureLink == other.PackageSignatureLink;
            }
            return false;
        }
    }
}
