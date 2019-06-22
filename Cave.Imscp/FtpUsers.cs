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
    /// Table structure imscp.ftp_users
    /// </summary>
    [Table("ftp_users")]
    public struct FtpUsers
    {
        /// <summary>
        /// string ftp_users.userid [255]
        /// </summary>
        [Field(Name = "userid", Length = 255)]
        public string Userid;

        /// <summary>
        /// uint ftp_users.admin_id [10]
        /// </summary>
        [Field(Name = "admin_id")]
        public uint AdminID;

        /// <summary>
        /// string ftp_users.passwd [255]
        /// </summary>
        [Field(Name = "passwd", Length = 255)]
        public string Passwd;

        /// <summary>
        /// string ftp_users.rawpasswd [255]
        /// </summary>
        [Field(Name = "rawpasswd", Length = 255)]
        public string Rawpasswd;

        /// <summary>
        /// uint ftp_users.uid [10]
        /// </summary>
        [Field(Name = "uid")]
        public uint Uid;

        /// <summary>
        /// uint ftp_users.gid [10]
        /// </summary>
        [Field(Name = "gid")]
        public uint Gid;

        /// <summary>
        /// string ftp_users.shell [255]
        /// </summary>
        [Field(Name = "shell", Length = 255)]
        public string Shell;

        /// <summary>
        /// string ftp_users.homedir [255]
        /// </summary>
        [Field(Name = "homedir", Length = 255)]
        public string Homedir;

        /// <summary>
        /// string ftp_users.status [255]
        /// </summary>
        [Field(Name = "status", Length = 255)]
        public string Status;


        /// <summary>Returns a <see cref="string" /> that represents this instance.</summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"{Userid}";
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. </returns>
        public override int GetHashCode()
        {
            return Userid.GetHashCode();
        }

        /// <summary>Determines whether the specified <see cref="object" />, is equal to this instance.</summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is FtpUsers)
            {
                FtpUsers other = (FtpUsers)obj;
                return Userid == other.Userid
                    && AdminID == other.AdminID
                    && Passwd == other.Passwd
                    && Rawpasswd == other.Rawpasswd
                    && Uid == other.Uid
                    && Gid == other.Gid
                    && Shell == other.Shell
                    && Homedir == other.Homedir
                    && Status == other.Status;
            }
            return false;
        }
    }
}
