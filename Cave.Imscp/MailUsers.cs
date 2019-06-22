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
    /// Table structure imscp.mail_users
    /// </summary>
    [Table("mail_users")]
    public struct MailUsers
    {
        /// <summary>
        /// [ID, AutoIncrement] uint mail_users.mail_id [10]
        /// </summary>
        [Field(Flags = FieldFlags.ID | FieldFlags.AutoIncrement, Name = "mail_id")]
        public uint MailID;

        /// <summary>
        /// string mail_users.mail_acc [65535]
        /// </summary>
        [Field(Name = "mail_acc", Length = 65535)]
        public string MailAcc;

        /// <summary>
        /// string mail_users.mail_pass [150]
        /// </summary>
        [Field(Name = "mail_pass", Length = 150)]
        public string MailPass;

        /// <summary>
        /// string mail_users.mail_forward [65535]
        /// </summary>
        [Field(Name = "mail_forward", Length = 65535)]
        public string MailForward;

        /// <summary>
        /// uint mail_users.domain_id [10]
        /// </summary>
        [Field(Name = "domain_id")]
        public uint DomainID;

        /// <summary>
        /// string mail_users.mail_type [30]
        /// </summary>
        [Field(Name = "mail_type", Length = 30)]
        public string MailType;

        /// <summary>
        /// uint mail_users.sub_id [10]
        /// </summary>
        [Field(Name = "sub_id")]
        public uint SubID;

        /// <summary>
        /// string mail_users.status [255]
        /// </summary>
        [Field(Name = "status", Length = 255)]
        public string Status;

        /// <summary>
        /// string mail_users.po_active [3]
        /// </summary>
        [Field(Name = "po_active", Length = 3)]
        public string PoActive;

        /// <summary>
        /// bool mail_users.mail_auto_respond [1]
        /// </summary>
        [Field(Name = "mail_auto_respond")]
        public bool MailAutoRespond;

        /// <summary>
        /// string mail_users.mail_auto_respond_text [65535]
        /// </summary>
        [Field(Name = "mail_auto_respond_text", Length = 65535)]
        public string MailAutoRespondText;

        /// <summary>
        /// ulong mail_users.quota [20]
        /// </summary>
        [Field(Name = "quota")]
        public ulong Quota;

        /// <summary>
        /// string mail_users.mail_addr [254]
        /// </summary>
        [Field(Name = "mail_addr", Length = 254)]
        public string MailAddr;


        /// <summary>Returns a <see cref="string" /> that represents this instance.</summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"[{MailID}] {MailAcc}";
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. </returns>
        public override int GetHashCode()
        {
            return MailID.GetHashCode();
        }

        /// <summary>Determines whether the specified <see cref="object" />, is equal to this instance.</summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is MailUsers)
            {
                MailUsers other = (MailUsers)obj;
                return MailID == other.MailID
                    && MailAcc == other.MailAcc
                    && MailPass == other.MailPass
                    && MailForward == other.MailForward
                    && DomainID == other.DomainID
                    && MailType == other.MailType
                    && SubID == other.SubID
                    && Status == other.Status
                    && PoActive == other.PoActive
                    && MailAutoRespond == other.MailAutoRespond
                    && MailAutoRespondText == other.MailAutoRespondText
                    && Quota == other.Quota
                    && MailAddr == other.MailAddr;
            }
            return false;
        }
    }
}
