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
    /// Table structure imscp.domain_aliasses
    /// </summary>
    [Table("domain_aliasses")]
    public struct DomainAliasses
    {
        /// <summary>
        /// [ID, AutoIncrement] uint domain_aliasses.alias_id [10]
        /// </summary>
        [Field(Flags = FieldFlags.ID | FieldFlags.AutoIncrement, Name = "alias_id")]
        public uint AliasID;

        /// <summary>
        /// uint domain_aliasses.domain_id [10]
        /// </summary>
        [Field(Name = "domain_id")]
        public uint DomainID;

        /// <summary>
        /// string domain_aliasses.alias_name [200]
        /// </summary>
        [Field(Name = "alias_name", Length = 200)]
        public string AliasName;

        /// <summary>
        /// string domain_aliasses.alias_status [255]
        /// </summary>
        [Field(Name = "alias_status", Length = 255)]
        public string AliasStatus;

        /// <summary>
        /// string domain_aliasses.alias_mount [200]
        /// </summary>
        [Field(Name = "alias_mount", Length = 200)]
        public string AliasMount;

        /// <summary>
        /// string domain_aliasses.alias_document_root [255]
        /// </summary>
        [Field(Name = "alias_document_root", Length = 255)]
        public string AliasDocumentRoot;

        /// <summary>
        /// uint domain_aliasses.alias_ip_id [10]
        /// </summary>
        [Field(Name = "alias_ip_id")]
        public uint AliasIpID;

        /// <summary>
        /// string domain_aliasses.url_forward [255]
        /// </summary>
        [Field(Name = "url_forward", Length = 255)]
        public string UrlForward;

        /// <summary>
        /// string domain_aliasses.type_forward [5]
        /// </summary>
        [Field(Name = "type_forward", Length = 5)]
        public string TypeForward;

        /// <summary>
        /// string domain_aliasses.host_forward [3]
        /// </summary>
        [Field(Name = "host_forward", Length = 3)]
        public string HostForward;

        /// <summary>
        /// string domain_aliasses.external_mail [15]
        /// </summary>
        [Field(Name = "external_mail", Length = 15)]
        public string ExternalMail;


        /// <summary>Returns a <see cref="string" /> that represents this instance.</summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"[{AliasID}] {AliasName}";
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. </returns>
        public override int GetHashCode()
        {
            return AliasID.GetHashCode();
        }

        /// <summary>Determines whether the specified <see cref="object" />, is equal to this instance.</summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is DomainAliasses)
            {
                DomainAliasses other = (DomainAliasses)obj;
                return AliasID == other.AliasID
                    && DomainID == other.DomainID
                    && AliasName == other.AliasName
                    && AliasStatus == other.AliasStatus
                    && AliasMount == other.AliasMount
                    && AliasDocumentRoot == other.AliasDocumentRoot
                    && AliasIpID == other.AliasIpID
                    && UrlForward == other.UrlForward
                    && TypeForward == other.TypeForward
                    && HostForward == other.HostForward
                    && ExternalMail == other.ExternalMail;
            }
            return false;
        }
    }
}
