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
    /// Table structure imscp.plugin
    /// </summary>
    [Table("plugin")]
    public struct Plugin
    {
        /// <summary>
        /// [ID, AutoIncrement] uint plugin.plugin_id [11]
        /// </summary>
        [Field(Flags = FieldFlags.ID | FieldFlags.AutoIncrement, Name = "plugin_id")]
        public uint ID;

        /// <summary>
        /// string plugin.plugin_name [50]
        /// </summary>
        [Field(Name = "plugin_name", Length = 50)]
        public string Name;

        /// <summary>
        /// string plugin.plugin_type [20]
        /// </summary>
        [Field(Name = "plugin_type", Length = 20)]
        public string Type;

        /// <summary>
        /// string plugin.plugin_info [65535]
        /// </summary>
        [Field(Name = "plugin_info", Length = 65535)]
        public string Info;

        /// <summary>
        /// string plugin.plugin_config [65535]
        /// </summary>
        [Field(Name = "plugin_config", Length = 65535)]
        public string Config;

        /// <summary>
        /// string plugin.plugin_config_prev [65535]
        /// </summary>
        [Field(Name = "plugin_config_prev", Length = 65535)]
        public string ConfigPrev;

        /// <summary>
        /// uint plugin.plugin_priority [11]
        /// </summary>
        [Field(Name = "plugin_priority")]
        public uint Priority;

        /// <summary>
        /// string plugin.plugin_status [255]
        /// </summary>
        [Field(Name = "plugin_status", Length = 255)]
        public string Status;

        /// <summary>
        /// string plugin.plugin_error [65535]
        /// </summary>
        [Field(Name = "plugin_error", Length = 65535)]
        public string Error;

        /// <summary>
        /// string plugin.plugin_backend [3]
        /// </summary>
        [Field(Name = "plugin_backend", Length = 3)]
        public string Backend;

        /// <summary>
        /// string plugin.plugin_lockers [65535]
        /// </summary>
        [Field(Name = "plugin_lockers", Length = 65535)]
        public string Lockers;


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
            if (obj is Plugin)
            {
                Plugin other = (Plugin)obj;
                return ID == other.ID
                    && Name == other.Name
                    && Type == other.Type
                    && Info == other.Info
                    && Config == other.Config
                    && ConfigPrev == other.ConfigPrev
                    && Priority == other.Priority
                    && Status == other.Status
                    && Error == other.Error
                    && Backend == other.Backend
                    && Lockers == other.Lockers;
            }
            return false;
        }
    }
}
