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
    /// Table structure imscp.login
    /// </summary>
    [Table("login")]
    public struct Login
    {
        /// <summary>
        /// [ID] string login.session_id [200]
        /// </summary>
        [Field(Flags = FieldFlags.ID, Name = "session_id", Length = 200)]
        public string SessionID;

        /// <summary>
        /// string login.ipaddr [40]
        /// </summary>
        [Field(Name = "ipaddr", Length = 40)]
        public string Ipaddr;

        /// <summary>
        /// uint login.lastaccess [10]
        /// </summary>
        [Field(Name = "lastaccess")]
        public uint Lastaccess;

        /// <summary>
        /// bool login.login_count [1]
        /// </summary>
        [Field(Name = "login_count")]
        public bool Count;

        /// <summary>
        /// bool login.captcha_count [1]
        /// </summary>
        [Field(Name = "captcha_count")]
        public bool CaptchaCount;

        /// <summary>
        /// string login.user_name [255]
        /// </summary>
        [Field(Name = "user_name", Length = 255)]
        public string UserName;


        /// <summary>Returns a <see cref="string" /> that represents this instance.</summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"[{SessionID}] {SessionID}";
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. </returns>
        public override int GetHashCode()
        {
            return SessionID.GetHashCode();
        }

        /// <summary>Determines whether the specified <see cref="object" />, is equal to this instance.</summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Login)
            {
                Login other = (Login)obj;
                return SessionID == other.SessionID
                    && Ipaddr == other.Ipaddr
                    && Lastaccess == other.Lastaccess
                    && Count == other.Count
                    && CaptchaCount == other.CaptchaCount
                    && UserName == other.UserName;
            }
            return false;
        }
    }
}
