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
    /// Table structure imscp.server_traffic
    /// </summary>
    [Table("server_traffic")]
    public struct ServerTraffic
    {
        /// <summary>
        /// [ID, AutoIncrement] uint server_traffic.straff_id [10]
        /// </summary>
        [Field(Flags = FieldFlags.ID | FieldFlags.AutoIncrement, Name = "straff_id")]
        public uint StraffID;

        /// <summary>
        /// uint server_traffic.traff_time [10]
        /// </summary>
        [Field(Name = "traff_time")]
        public uint TraffTime;

        /// <summary>
        /// ulong server_traffic.bytes_in [20]
        /// </summary>
        [Field(Name = "bytes_in")]
        public ulong BytesIn;

        /// <summary>
        /// ulong server_traffic.bytes_out [20]
        /// </summary>
        [Field(Name = "bytes_out")]
        public ulong BytesOut;

        /// <summary>
        /// ulong server_traffic.bytes_mail_in [20]
        /// </summary>
        [Field(Name = "bytes_mail_in")]
        public ulong BytesMailIn;

        /// <summary>
        /// ulong server_traffic.bytes_mail_out [20]
        /// </summary>
        [Field(Name = "bytes_mail_out")]
        public ulong BytesMailOut;

        /// <summary>
        /// ulong server_traffic.bytes_pop_in [20]
        /// </summary>
        [Field(Name = "bytes_pop_in")]
        public ulong BytesPopIn;

        /// <summary>
        /// ulong server_traffic.bytes_pop_out [20]
        /// </summary>
        [Field(Name = "bytes_pop_out")]
        public ulong BytesPopOut;

        /// <summary>
        /// ulong server_traffic.bytes_web_in [20]
        /// </summary>
        [Field(Name = "bytes_web_in")]
        public ulong BytesWebIn;

        /// <summary>
        /// ulong server_traffic.bytes_web_out [20]
        /// </summary>
        [Field(Name = "bytes_web_out")]
        public ulong BytesWebOut;


        /// <summary>Returns a <see cref="string" /> that represents this instance.</summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"ServerTraffic [{StraffID}]";
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. </returns>
        public override int GetHashCode()
        {
            return StraffID.GetHashCode();
        }

        /// <summary>Determines whether the specified <see cref="object" />, is equal to this instance.</summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is ServerTraffic)
            {
                ServerTraffic other = (ServerTraffic)obj;
                return StraffID == other.StraffID
                    && TraffTime == other.TraffTime
                    && BytesIn == other.BytesIn
                    && BytesOut == other.BytesOut
                    && BytesMailIn == other.BytesMailIn
                    && BytesMailOut == other.BytesMailOut
                    && BytesPopIn == other.BytesPopIn
                    && BytesPopOut == other.BytesPopOut
                    && BytesWebIn == other.BytesWebIn
                    && BytesWebOut == other.BytesWebOut;
            }
            return false;
        }
    }
}
