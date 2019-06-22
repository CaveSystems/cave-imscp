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
    /// Table structure imscp.quotalimits
    /// </summary>
    [Table("quotalimits")]
    public struct Quotalimits
    {
        /// <summary>
        /// [ID] string quotalimits.name [255]
        /// </summary>
        [Field(Flags = FieldFlags.ID, Name = "name", Length = 255)]
        public string Name;

        /// <summary>
        /// string quotalimits.quota_type [5]
        /// </summary>
        [Field(Name = "quota_type", Length = 5)]
        public string QuotaType;

        /// <summary>
        /// string quotalimits.per_session [5]
        /// </summary>
        [Field(Name = "per_session", Length = 5)]
        public string PerSession;

        /// <summary>
        /// string quotalimits.limit_type [4]
        /// </summary>
        [Field(Name = "limit_type", Length = 4)]
        public string LimitType;

        /// <summary>
        /// float quotalimits.bytes_in_avail [12]
        /// </summary>
        [Field(Name = "bytes_in_avail")]
        public float BytesInAvail;

        /// <summary>
        /// float quotalimits.bytes_out_avail [12]
        /// </summary>
        [Field(Name = "bytes_out_avail")]
        public float BytesOutAvail;

        /// <summary>
        /// float quotalimits.bytes_xfer_avail [12]
        /// </summary>
        [Field(Name = "bytes_xfer_avail")]
        public float BytesXferAvail;

        /// <summary>
        /// uint quotalimits.files_in_avail [10]
        /// </summary>
        [Field(Name = "files_in_avail")]
        public uint FilesInAvail;

        /// <summary>
        /// uint quotalimits.files_out_avail [10]
        /// </summary>
        [Field(Name = "files_out_avail")]
        public uint FilesOutAvail;

        /// <summary>
        /// uint quotalimits.files_xfer_avail [10]
        /// </summary>
        [Field(Name = "files_xfer_avail")]
        public uint FilesXferAvail;


        /// <summary>Returns a <see cref="string" /> that represents this instance.</summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"[{Name}] {Name}";
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. </returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        /// <summary>Determines whether the specified <see cref="object" />, is equal to this instance.</summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Quotalimits)
            {
                Quotalimits other = (Quotalimits)obj;
                return Name == other.Name
                    && QuotaType == other.QuotaType
                    && PerSession == other.PerSession
                    && LimitType == other.LimitType
                    && BytesInAvail == other.BytesInAvail
                    && BytesOutAvail == other.BytesOutAvail
                    && BytesXferAvail == other.BytesXferAvail
                    && FilesInAvail == other.FilesInAvail
                    && FilesOutAvail == other.FilesOutAvail
                    && FilesXferAvail == other.FilesXferAvail;
            }
            return false;
        }
    }
}
