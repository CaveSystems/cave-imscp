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
    /// Table structure imscp.reseller_props
    /// </summary>
    [Table("reseller_props")]
    public struct ResellerProps
    {
        /// <summary>
        /// [ID, AutoIncrement] uint reseller_props.id [10]
        /// </summary>
        [Field(Flags = FieldFlags.ID | FieldFlags.AutoIncrement, Name = "id")]
        public uint ID;

        /// <summary>
        /// uint reseller_props.reseller_id [10]
        /// </summary>
        [Field(Name = "reseller_id")]
        public uint ResellerID;

        /// <summary>
        /// int reseller_props.current_dmn_cnt [11]
        /// </summary>
        [Field(Name = "current_dmn_cnt")]
        public int CurrentDmnCnt;

        /// <summary>
        /// int reseller_props.max_dmn_cnt [11]
        /// </summary>
        [Field(Name = "max_dmn_cnt")]
        public int MaxDmnCnt;

        /// <summary>
        /// int reseller_props.current_sub_cnt [11]
        /// </summary>
        [Field(Name = "current_sub_cnt")]
        public int CurrentSubCnt;

        /// <summary>
        /// int reseller_props.max_sub_cnt [11]
        /// </summary>
        [Field(Name = "max_sub_cnt")]
        public int MaxSubCnt;

        /// <summary>
        /// int reseller_props.current_als_cnt [11]
        /// </summary>
        [Field(Name = "current_als_cnt")]
        public int CurrentAlsCnt;

        /// <summary>
        /// int reseller_props.max_als_cnt [11]
        /// </summary>
        [Field(Name = "max_als_cnt")]
        public int MaxAlsCnt;

        /// <summary>
        /// int reseller_props.current_mail_cnt [11]
        /// </summary>
        [Field(Name = "current_mail_cnt")]
        public int CurrentMailCnt;

        /// <summary>
        /// int reseller_props.max_mail_cnt [11]
        /// </summary>
        [Field(Name = "max_mail_cnt")]
        public int MaxMailCnt;

        /// <summary>
        /// int reseller_props.current_ftp_cnt [11]
        /// </summary>
        [Field(Name = "current_ftp_cnt")]
        public int CurrentFtpCnt;

        /// <summary>
        /// int reseller_props.max_ftp_cnt [11]
        /// </summary>
        [Field(Name = "max_ftp_cnt")]
        public int MaxFtpCnt;

        /// <summary>
        /// int reseller_props.current_sql_db_cnt [11]
        /// </summary>
        [Field(Name = "current_sql_db_cnt")]
        public int CurrentSqlCnt;

        /// <summary>
        /// int reseller_props.max_sql_db_cnt [11]
        /// </summary>
        [Field(Name = "max_sql_db_cnt")]
        public int MaxSqlCnt;

        /// <summary>
        /// int reseller_props.current_sql_user_cnt [11]
        /// </summary>
        [Field(Name = "current_sql_user_cnt")]
        public int CurrentSqlUserCnt;

        /// <summary>
        /// int reseller_props.max_sql_user_cnt [11]
        /// </summary>
        [Field(Name = "max_sql_user_cnt")]
        public int MaxSqlUserCnt;

        /// <summary>
        /// int reseller_props.current_disk_amnt [11]
        /// </summary>
        [Field(Name = "current_disk_amnt")]
        public int CurrentDiskAmnt;

        /// <summary>
        /// int reseller_props.max_disk_amnt [11]
        /// </summary>
        [Field(Name = "max_disk_amnt")]
        public int MaxDiskAmnt;

        /// <summary>
        /// int reseller_props.current_traff_amnt [11]
        /// </summary>
        [Field(Name = "current_traff_amnt")]
        public int CurrentTraffAmnt;

        /// <summary>
        /// int reseller_props.max_traff_amnt [11]
        /// </summary>
        [Field(Name = "max_traff_amnt")]
        public int MaxTraffAmnt;

        /// <summary>
        /// string reseller_props.support_system [3]
        /// </summary>
        [Field(Name = "support_system", Length = 3)]
        public string SupportSystem;

        /// <summary>
        /// string reseller_props.customer_id [200]
        /// </summary>
        [Field(Name = "customer_id", Length = 200)]
        public string CustomerID;

        /// <summary>
        /// string reseller_props.reseller_ips [65535]
        /// </summary>
        [Field(Name = "reseller_ips", Length = 65535)]
        public string ResellerIps;

        /// <summary>
        /// string reseller_props.software_allowed [15]
        /// </summary>
        [Field(Name = "software_allowed", Length = 15)]
        public string SoftwareAllowed;

        /// <summary>
        /// string reseller_props.softwaredepot_allowed [15]
        /// </summary>
        [Field(Name = "softwaredepot_allowed", Length = 15)]
        public string SoftwaredepotAllowed;

        /// <summary>
        /// string reseller_props.websoftwaredepot_allowed [15]
        /// </summary>
        [Field(Name = "websoftwaredepot_allowed", Length = 15)]
        public string WebsoftwaredepotAllowed;

        /// <summary>
        /// string reseller_props.php_ini_system [15]
        /// </summary>
        [Field(Name = "php_ini_system", Length = 15)]
        public string PhpIniSystem;

        /// <summary>
        /// string reseller_props.php_ini_al_disable_functions [15]
        /// </summary>
        [Field(Name = "php_ini_al_disable_functions", Length = 15)]
        public string PhpIniAlDisableFunctions;

        /// <summary>
        /// string reseller_props.php_ini_al_mail_function [15]
        /// </summary>
        [Field(Name = "php_ini_al_mail_function", Length = 15)]
        public string PhpIniAlMailFunction;

        /// <summary>
        /// string reseller_props.php_ini_al_allow_url_fopen [15]
        /// </summary>
        [Field(Name = "php_ini_al_allow_url_fopen", Length = 15)]
        public string PhpIniAlAllowUrlFopen;

        /// <summary>
        /// string reseller_props.php_ini_al_display_errors [15]
        /// </summary>
        [Field(Name = "php_ini_al_display_errors", Length = 15)]
        public string PhpIniAlDisplayErrors;

        /// <summary>
        /// int reseller_props.php_ini_max_post_max_size [11]
        /// </summary>
        [Field(Name = "php_ini_max_post_max_size")]
        public int PhpIniMaxPostMaxSize;

        /// <summary>
        /// int reseller_props.php_ini_max_upload_max_filesize [11]
        /// </summary>
        [Field(Name = "php_ini_max_upload_max_filesize")]
        public int PhpIniMaxUploadMaxFilesize;

        /// <summary>
        /// int reseller_props.php_ini_max_max_execution_time [11]
        /// </summary>
        [Field(Name = "php_ini_max_max_execution_time")]
        public int PhpIniMaxMaxExecutionTime;

        /// <summary>
        /// int reseller_props.php_ini_max_max_input_time [11]
        /// </summary>
        [Field(Name = "php_ini_max_max_input_time")]
        public int PhpIniMaxMaxInputTime;

        /// <summary>
        /// int reseller_props.php_ini_max_memory_limit [11]
        /// </summary>
        [Field(Name = "php_ini_max_memory_limit")]
        public int PhpIniMaxMemoryLimit;


        /// <summary>Returns a <see cref="string" /> that represents this instance.</summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"[{ID}] {SupportSystem}";
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
            if (obj is ResellerProps)
            {
                ResellerProps other = (ResellerProps)obj;
                return ID == other.ID
                    && ResellerID == other.ResellerID
                    && CurrentDmnCnt == other.CurrentDmnCnt
                    && MaxDmnCnt == other.MaxDmnCnt
                    && CurrentSubCnt == other.CurrentSubCnt
                    && MaxSubCnt == other.MaxSubCnt
                    && CurrentAlsCnt == other.CurrentAlsCnt
                    && MaxAlsCnt == other.MaxAlsCnt
                    && CurrentMailCnt == other.CurrentMailCnt
                    && MaxMailCnt == other.MaxMailCnt
                    && CurrentFtpCnt == other.CurrentFtpCnt
                    && MaxFtpCnt == other.MaxFtpCnt
                    && CurrentSqlCnt == other.CurrentSqlCnt
                    && MaxSqlCnt == other.MaxSqlCnt
                    && CurrentSqlUserCnt == other.CurrentSqlUserCnt
                    && MaxSqlUserCnt == other.MaxSqlUserCnt
                    && CurrentDiskAmnt == other.CurrentDiskAmnt
                    && MaxDiskAmnt == other.MaxDiskAmnt
                    && CurrentTraffAmnt == other.CurrentTraffAmnt
                    && MaxTraffAmnt == other.MaxTraffAmnt
                    && SupportSystem == other.SupportSystem
                    && CustomerID == other.CustomerID
                    && ResellerIps == other.ResellerIps
                    && SoftwareAllowed == other.SoftwareAllowed
                    && SoftwaredepotAllowed == other.SoftwaredepotAllowed
                    && WebsoftwaredepotAllowed == other.WebsoftwaredepotAllowed
                    && PhpIniSystem == other.PhpIniSystem
                    && PhpIniAlDisableFunctions == other.PhpIniAlDisableFunctions
                    && PhpIniAlMailFunction == other.PhpIniAlMailFunction
                    && PhpIniAlAllowUrlFopen == other.PhpIniAlAllowUrlFopen
                    && PhpIniAlDisplayErrors == other.PhpIniAlDisplayErrors
                    && PhpIniMaxPostMaxSize == other.PhpIniMaxPostMaxSize
                    && PhpIniMaxUploadMaxFilesize == other.PhpIniMaxUploadMaxFilesize
                    && PhpIniMaxMaxExecutionTime == other.PhpIniMaxMaxExecutionTime
                    && PhpIniMaxMaxInputTime == other.PhpIniMaxMaxInputTime
                    && PhpIniMaxMemoryLimit == other.PhpIniMaxMemoryLimit;
            }
            return false;
        }
    }
}
