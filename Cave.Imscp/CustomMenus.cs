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
    /// Table structure imscp.custom_menus
    /// </summary>
    [Table("custom_menus")]
    public struct CustomMenus
    {
        /// <summary>
        /// [ID, AutoIncrement] uint custom_menus.menu_id [10]
        /// </summary>
        [Field(Flags = FieldFlags.ID | FieldFlags.AutoIncrement, Name = "menu_id")]
        public uint MenuID;

        /// <summary>
        /// string custom_menus.menu_level [10]
        /// </summary>
        [Field(Name = "menu_level", Length = 10)]
        public string MenuLevel;

        /// <summary>
        /// uint custom_menus.menu_order [10]
        /// </summary>
        [Field(Name = "menu_order")]
        public uint MenuOrder;

        /// <summary>
        /// string custom_menus.menu_name [255]
        /// </summary>
        [Field(Name = "menu_name", Length = 255)]
        public string MenuName;

        /// <summary>
        /// string custom_menus.menu_link [200]
        /// </summary>
        [Field(Name = "menu_link", Length = 200)]
        public string MenuLink;

        /// <summary>
        /// string custom_menus.menu_target [200]
        /// </summary>
        [Field(Name = "menu_target", Length = 200)]
        public string MenuTarget;


        /// <summary>Returns a <see cref="string" /> that represents this instance.</summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"[{MenuID}] {MenuLevel}";
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. </returns>
        public override int GetHashCode()
        {
            return MenuID.GetHashCode();
        }

        /// <summary>Determines whether the specified <see cref="object" />, is equal to this instance.</summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is CustomMenus)
            {
                CustomMenus other = (CustomMenus)obj;
                return MenuID == other.MenuID
                    && MenuLevel == other.MenuLevel
                    && MenuOrder == other.MenuOrder
                    && MenuName == other.MenuName
                    && MenuLink == other.MenuLink
                    && MenuTarget == other.MenuTarget;
            }
            return false;
        }
    }
}
