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
    /// Table structure imscp.admin
    /// </summary>
    [Table("admin")]
    public struct Admin
    {
        /// <summary>
        /// [ID, AutoIncrement] uint admin.admin_id [10]
        /// </summary>
        [Field(Flags = FieldFlags.ID | FieldFlags.AutoIncrement, Name = "admin_id")]
        public uint ID;

        /// <summary>
        /// string admin.admin_name [200]
        /// </summary>
        [Field(Name = "admin_name", Length = 200)]
        public string Name;

        /// <summary>
        /// string admin.admin_pass [200]
        /// </summary>
        [Field(Name = "admin_pass", Length = 200)]
        public string Pass;

        /// <summary>
        /// string admin.admin_type [10]
        /// </summary>
        [Field(Name = "admin_type", Length = 10)]
        public string Type;

        /// <summary>
        /// string admin.admin_sys_name [16]
        /// </summary>
        [Field(Name = "admin_sys_name", Length = 16)]
        public string SysName;

        /// <summary>
        /// uint admin.admin_sys_uid [10]
        /// </summary>
        [Field(Name = "admin_sys_uid")]
        public uint SysUid;

        /// <summary>
        /// string admin.admin_sys_gname [32]
        /// </summary>
        [Field(Name = "admin_sys_gname", Length = 32)]
        public string SysGname;

        /// <summary>
        /// uint admin.admin_sys_gid [10]
        /// </summary>
        [Field(Name = "admin_sys_gid")]
        public uint SysGid;

        /// <summary>
        /// uint admin.domain_created [10]
        /// </summary>
        [Field(Name = "domain_created")]
        public uint DomainCreated;

        /// <summary>
        /// string admin.customer_id [200]
        /// </summary>
        [Field(Name = "customer_id", Length = 200)]
        public string CustomerID;

        /// <summary>
        /// uint admin.created_by [10]
        /// </summary>
        [Field(Name = "created_by")]
        public uint CreatedBy;

        /// <summary>
        /// string admin.fname [200]
        /// </summary>
        [Field(Name = "fname", Length = 200)]
        public string Fname;

        /// <summary>
        /// string admin.lname [200]
        /// </summary>
        [Field(Name = "lname", Length = 200)]
        public string Lname;

        /// <summary>
        /// string admin.gender [1]
        /// </summary>
        [Field(Name = "gender", Length = 1)]
        public string Gender;

        /// <summary>
        /// string admin.firm [200]
        /// </summary>
        [Field(Name = "firm", Length = 200)]
        public string Firm;

        /// <summary>
        /// string admin.zip [10]
        /// </summary>
        [Field(Name = "zip", Length = 10)]
        public string Zip;

        /// <summary>
        /// string admin.city [200]
        /// </summary>
        [Field(Name = "city", Length = 200)]
        public string City;

        /// <summary>
        /// string admin.state [200]
        /// </summary>
        [Field(Name = "state", Length = 200)]
        public string State;

        /// <summary>
        /// string admin.country [200]
        /// </summary>
        [Field(Name = "country", Length = 200)]
        public string Country;

        /// <summary>
        /// string admin.email [200]
        /// </summary>
        [Field(Name = "email", Length = 200)]
        public string Email;

        /// <summary>
        /// string admin.phone [200]
        /// </summary>
        [Field(Name = "phone", Length = 200)]
        public string Phone;

        /// <summary>
        /// string admin.fax [200]
        /// </summary>
        [Field(Name = "fax", Length = 200)]
        public string Fax;

        /// <summary>
        /// string admin.street1 [200]
        /// </summary>
        [Field(Name = "street1", Length = 200)]
        public string street1;

        /// <summary>
        /// string admin.street2 [200]
        /// </summary>
        [Field(Name = "street2", Length = 200)]
        public string street2;

        /// <summary>
        /// string admin.uniqkey [255]
        /// </summary>
        [Field(Name = "uniqkey", Length = 255)]
        public string Uniqkey;

        /// <summary>
        /// DateTime Native.Utc admin.uniqkey_time [19]
        /// </summary>
        [Field(Name = "uniqkey_time")]
        public DateTime UniqkeyTime;

        /// <summary>
        /// string admin.admin_status [255]
        /// </summary>
        [Field(Name = "admin_status", Length = 255)]
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
            if (obj is Admin)
            {
                Admin other = (Admin)obj;
                return ID == other.ID
                    && Name == other.Name
                    && Pass == other.Pass
                    && Type == other.Type
                    && SysName == other.SysName
                    && SysUid == other.SysUid
                    && SysGname == other.SysGname
                    && SysGid == other.SysGid
                    && DomainCreated == other.DomainCreated
                    && CustomerID == other.CustomerID
                    && CreatedBy == other.CreatedBy
                    && Fname == other.Fname
                    && Lname == other.Lname
                    && Gender == other.Gender
                    && Firm == other.Firm
                    && Zip == other.Zip
                    && City == other.City
                    && State == other.State
                    && Country == other.Country
                    && Email == other.Email
                    && Phone == other.Phone
                    && Fax == other.Fax
                    && street1 == other.street1
                    && street2 == other.street2
                    && Uniqkey == other.Uniqkey
                    && UniqkeyTime == other.UniqkeyTime
                    && Status == other.Status;
            }
            return false;
        }
    }
}
