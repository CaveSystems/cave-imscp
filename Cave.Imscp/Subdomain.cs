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
    /// Table structure imscp.subdomain
    /// </summary>
    [Table("subdomain")]
    public struct Subdomain
    {
        /// <summary>
        /// [ID, AutoIncrement] uint subdomain.subdomain_id [10]
        /// </summary>
        [Field(Flags = FieldFlags.ID | FieldFlags.AutoIncrement, Name = "subdomain_id")]
        public uint ID;

        /// <summary>
        /// uint subdomain.domain_id [10]
        /// </summary>
        [Field(Name = "domain_id")]
        public uint DomainID;

        /// <summary>
        /// string subdomain.subdomain_name [200]
        /// </summary>
        [Field(Name = "subdomain_name", Length = 200)]
        public string Name;

        /// <summary>
        /// string subdomain.subdomain_mount [200]
        /// </summary>
        [Field(Name = "subdomain_mount", Length = 200)]
        public string Mount;

        /// <summary>
        /// string subdomain.subdomain_document_root [255]
        /// </summary>
        [Field(Name = "subdomain_document_root", Length = 255)]
        public string DocumentRoot;

        /// <summary>
        /// string subdomain.subdomain_url_forward [255]
        /// </summary>
        [Field(Name = "subdomain_url_forward", Length = 255)]
        public string UrlForward;

        /// <summary>
        /// string subdomain.subdomain_type_forward [5]
        /// </summary>
        [Field(Name = "subdomain_type_forward", Length = 5)]
        public string TypeForward;

        /// <summary>
        /// string subdomain.subdomain_host_forward [3]
        /// </summary>
        [Field(Name = "subdomain_host_forward", Length = 3)]
        public string HostForward;

        /// <summary>
        /// string subdomain.subdomain_status [255]
        /// </summary>
        [Field(Name = "subdomain_status", Length = 255)]
        public string Status;


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
            if (obj is Subdomain)
            {
                Subdomain other = (Subdomain)obj;
                return ID == other.ID
                    && DomainID == other.DomainID
                    && Name == other.Name
                    && Mount == other.Mount
                    && DocumentRoot == other.DocumentRoot
                    && UrlForward == other.UrlForward
                    && TypeForward == other.TypeForward
                    && HostForward == other.HostForward
                    && Status == other.Status;
            }
            return false;
        }
    }
}
