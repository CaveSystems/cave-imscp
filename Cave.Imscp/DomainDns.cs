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
    /// Table structure imscp.domain_dns
    /// </summary>
    [Table("domain_dns")]
    public struct DomainDns
    {
        /// <summary>
        /// [ID, AutoIncrement] int domain_dns.domain_dns_id [11]
        /// </summary>
        [Field(Flags = FieldFlags.ID | FieldFlags.AutoIncrement, Name = "domain_dns_id")]
        public int ID;

        /// <summary>
        /// int domain_dns.domain_id [11]
        /// </summary>
        [Field(Name = "domain_id", Flags = FieldFlags.Index)]
        public int DomainID;

        /// <summary>
        /// int domain_dns.alias_id [11]
        /// </summary>
        [Field(Name = "alias_id", Flags = FieldFlags.Index)]
        public int AliasID;

        /// <summary>
        /// string domain_dns.domain_dns [65535]
        /// </summary>
        [Field(Name = "domain_dns", Length = 65535, Flags = FieldFlags.Index)]
        public string NameAndTTL;

        /// <summary>
        /// string domain_dns.domain_class [2]
        /// </summary>
        [Field(Name = "domain_class", Length = 2, Flags = FieldFlags.Index)]
        public string DomainClass;

        /// <summary>
        /// string domain_dns.domain_type [5]
        /// </summary>
        [Field(Name = "domain_type", Length = 5, Flags = FieldFlags.Index)]
        public string DomainType;

        /// <summary>
        /// string domain_dns.domain_text [65535]
        /// </summary>
        [Field(Name = "domain_text", Length = 65535, Flags = FieldFlags.Index)]
        public string DomainText;

        /// <summary>
        /// string domain_dns.owned_by [255]
        /// </summary>
        [Field(Name = "owned_by", Length = 255)]
        public string OwnedBy;

        /// <summary>
        /// string domain_dns.domain_dns_status [255]
        /// </summary>
        [Field(Name = "domain_dns_status", Length = 255, Flags = FieldFlags.Index)]
        public string Status;

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return NameAndTTL.Split('\t')[0];
            }
            set
            {
                NameAndTTL = NameAndTTL.ReplacePart('\t', 0, value);
            }
        }

        /// <summary>Gets or sets the TTL.</summary>
        /// <value>The TTL.</value>
        public uint TTL
        {
            get
            {
                return uint.Parse(NameAndTTL.Split('\t')[1]);
            }
            set
            {
                NameAndTTL = NameAndTTL.ReplacePart('\t', 1, value.ToString());
            }
        }

        /// <summary>Returns a <see cref="string" /> that represents this instance.</summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"{DomainType} {NameAndTTL} {DomainText}";
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
            if (obj is DomainDns)
            {
                DomainDns other = (DomainDns)obj;
                bool result = ID == other.ID
                    && DomainID == other.DomainID
                    && AliasID == other.AliasID
                    && NameAndTTL == other.NameAndTTL
                    && DomainClass == other.DomainClass
                    && DomainType == other.DomainType
                    && DomainText == other.DomainText
                    && OwnedBy == other.OwnedBy
                    && Status == other.Status;
                return result;
            }
            return false;
        }
    }
}
