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
    /// Table structure imscp.error_pages
    /// </summary>
    [Table("error_pages")]
    public struct ErrorPages
    {
        /// <summary>
        /// [ID, AutoIncrement] uint error_pages.ep_id [10]
        /// </summary>
        [Field(Flags = FieldFlags.ID | FieldFlags.AutoIncrement, Name = "ep_id")]
        public uint EpID;

        /// <summary>
        /// uint error_pages.user_id [10]
        /// </summary>
        [Field(Name = "user_id")]
        public uint UserID;

        /// <summary>
        /// string error_pages.error_401 [65535]
        /// </summary>
        [Field(Name = "error_401", Length = 65535)]
        public string Error401;

        /// <summary>
        /// string error_pages.error_403 [65535]
        /// </summary>
        [Field(Name = "error_403", Length = 65535)]
        public string Error403;

        /// <summary>
        /// string error_pages.error_404 [65535]
        /// </summary>
        [Field(Name = "error_404", Length = 65535)]
        public string Error404;

        /// <summary>
        /// string error_pages.error_500 [65535]
        /// </summary>
        [Field(Name = "error_500", Length = 65535)]
        public string Error500;


        /// <summary>Returns a <see cref="string" /> that represents this instance.</summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"[{EpID}] {Error401}";
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. </returns>
        public override int GetHashCode()
        {
            return EpID.GetHashCode();
        }

        /// <summary>Determines whether the specified <see cref="object" />, is equal to this instance.</summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is ErrorPages)
            {
                ErrorPages other = (ErrorPages)obj;
                return EpID == other.EpID
                    && UserID == other.UserID
                    && Error401 == other.Error401
                    && Error403 == other.Error403
                    && Error404 == other.Error404
                    && Error500 == other.Error500;
            }
            return false;
        }
    }
}
