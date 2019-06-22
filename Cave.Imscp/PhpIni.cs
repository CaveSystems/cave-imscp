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
    /// Table structure imscp.php_ini
    /// </summary>
    [Table("php_ini")]
    public struct PhpIni
    {
        /// <summary>
        /// [ID, AutoIncrement] int php_ini.id [11]
        /// </summary>
        [Field(Flags = FieldFlags.ID | FieldFlags.AutoIncrement, Name = "id")]
        public int ID;

        /// <summary>
        /// int php_ini.admin_id [10]
        /// </summary>
        [Field(Name = "admin_id")]
        public int AdminID;

        /// <summary>
        /// int php_ini.domain_id [10]
        /// </summary>
        [Field(Name = "domain_id")]
        public int DomainID;

        /// <summary>
        /// string php_ini.domain_type [15]
        /// </summary>
        [Field(Name = "domain_type", Length = 15)]
        public string DomainType;

        /// <summary>
        /// string php_ini.disable_functions [255]
        /// </summary>
        [Field(Name = "disable_functions", Length = 255)]
        public string DisableFunctions;

        /// <summary>
        /// string php_ini.allow_url_fopen [10]
        /// </summary>
        [Field(Name = "allow_url_fopen", Length = 10)]
        public string AllowUrlFopen;

        /// <summary>
        /// string php_ini.display_errors [10]
        /// </summary>
        [Field(Name = "display_errors", Length = 10)]
        public string DisplayErrors;

        /// <summary>
        /// string php_ini.error_reporting [255]
        /// </summary>
        [Field(Name = "error_reporting", Length = 255)]
        public string ErrorReporting;

        /// <summary>
        /// int php_ini.post_max_size [11]
        /// </summary>
        [Field(Name = "post_max_size")]
        public int PostMaxSize;

        /// <summary>
        /// int php_ini.upload_max_filesize [11]
        /// </summary>
        [Field(Name = "upload_max_filesize")]
        public int UploadMaxFilesize;

        /// <summary>
        /// int php_ini.max_execution_time [11]
        /// </summary>
        [Field(Name = "max_execution_time")]
        public int MaxExecutionTime;

        /// <summary>
        /// int php_ini.max_input_time [11]
        /// </summary>
        [Field(Name = "max_input_time")]
        public int MaxInputTime;

        /// <summary>
        /// int php_ini.memory_limit [11]
        /// </summary>
        [Field(Name = "memory_limit")]
        public int MemoryLimit;


        /// <summary>Returns a <see cref="string" /> that represents this instance.</summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"[{ID}] {DomainType}";
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
            if (obj is PhpIni)
            {
                PhpIni other = (PhpIni)obj;
                return ID == other.ID
                    && AdminID == other.AdminID
                    && DomainID == other.DomainID
                    && DomainType == other.DomainType
                    && DisableFunctions == other.DisableFunctions
                    && AllowUrlFopen == other.AllowUrlFopen
                    && DisplayErrors == other.DisplayErrors
                    && ErrorReporting == other.ErrorReporting
                    && PostMaxSize == other.PostMaxSize
                    && UploadMaxFilesize == other.UploadMaxFilesize
                    && MaxExecutionTime == other.MaxExecutionTime
                    && MaxInputTime == other.MaxInputTime
                    && MemoryLimit == other.MemoryLimit;
            }
            return false;
        }
    }
}
