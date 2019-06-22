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
    /// Table structure imscp.domain_traffic
    /// </summary>
    [Table("domain_traffic")]
    public struct DomainTraffic
    {
        /// <summary>
        /// [ID, AutoIncrement] uint domain_traffic.dtraff_id [10]
        /// </summary>
        [Field(Flags = FieldFlags.ID | FieldFlags.AutoIncrement, Name = "dtraff_id")]
        public uint DtraffID;

        /// <summary>
        /// uint domain_traffic.domain_id [10]
        /// </summary>
        [Field(Name = "domain_id")]
        public uint DomainID;

        /// <summary>
        /// ulong domain_traffic.dtraff_time [20]
        /// </summary>
        [Field(Name = "dtraff_time")]
        public ulong DtraffTime;

        /// <summary>
        /// ulong domain_traffic.dtraff_web [20]
        /// </summary>
        [Field(Name = "dtraff_web")]
        public ulong DtraffWeb;

        /// <summary>
        /// ulong domain_traffic.dtraff_ftp [20]
        /// </summary>
        [Field(Name = "dtraff_ftp")]
        public ulong DtraffFtp;

        /// <summary>
        /// ulong domain_traffic.dtraff_mail [20]
        /// </summary>
        [Field(Name = "dtraff_mail")]
        public ulong DtraffMail;

        /// <summary>
        /// ulong domain_traffic.dtraff_pop [20]
        /// </summary>
        [Field(Name = "dtraff_pop")]
        public ulong DtraffPop;


        /// <summary>Returns a <see cref="string" /> that represents this instance.</summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"DomainTraffic [{DtraffID}]";
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. </returns>
        public override int GetHashCode()
        {
            return DtraffID.GetHashCode();
        }

        /// <summary>Determines whether the specified <see cref="object" />, is equal to this instance.</summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is DomainTraffic)
            {
                DomainTraffic other = (DomainTraffic)obj;
                return DtraffID == other.DtraffID
                    && DomainID == other.DomainID
                    && DtraffTime == other.DtraffTime
                    && DtraffWeb == other.DtraffWeb
                    && DtraffFtp == other.DtraffFtp
                    && DtraffMail == other.DtraffMail
                    && DtraffPop == other.DtraffPop;
            }
            return false;
        }
    }
}
