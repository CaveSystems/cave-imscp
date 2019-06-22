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
    /// Table structure imscp.tickets
    /// </summary>
    [Table("tickets")]
    public struct Tickets
    {
        /// <summary>
        /// [ID, AutoIncrement] uint tickets.ticket_id [10]
        /// </summary>
        [Field(Flags = FieldFlags.ID | FieldFlags.AutoIncrement, Name = "ticket_id")]
        public uint TicketID;

        /// <summary>
        /// int tickets.ticket_level [10]
        /// </summary>
        [Field(Name = "ticket_level")]
        public int TicketLevel;

        /// <summary>
        /// uint tickets.ticket_from [10]
        /// </summary>
        [Field(Name = "ticket_from")]
        public uint TicketFrom;

        /// <summary>
        /// uint tickets.ticket_to [10]
        /// </summary>
        [Field(Name = "ticket_to")]
        public uint TicketTo;

        /// <summary>
        /// uint tickets.ticket_status [10]
        /// </summary>
        [Field(Name = "ticket_status")]
        public uint TicketStatus;

        /// <summary>
        /// uint tickets.ticket_reply [10]
        /// </summary>
        [Field(Name = "ticket_reply")]
        public uint TicketReply;

        /// <summary>
        /// uint tickets.ticket_urgency [10]
        /// </summary>
        [Field(Name = "ticket_urgency")]
        public uint TicketUrgency;

        /// <summary>
        /// uint tickets.ticket_date [10]
        /// </summary>
        [Field(Name = "ticket_date")]
        public uint TicketDate;

        /// <summary>
        /// string tickets.ticket_subject [255]
        /// </summary>
        [Field(Name = "ticket_subject", Length = 255)]
        public string TicketSubject;

        /// <summary>
        /// string tickets.ticket_message [65535]
        /// </summary>
        [Field(Name = "ticket_message", Length = 65535)]
        public string TicketMessage;


        /// <summary>Returns a <see cref="string" /> that represents this instance.</summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"[{TicketID}] {TicketSubject}";
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. </returns>
        public override int GetHashCode()
        {
            return TicketID.GetHashCode();
        }

        /// <summary>Determines whether the specified <see cref="object" />, is equal to this instance.</summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Tickets)
            {
                Tickets other = (Tickets)obj;
                return TicketID == other.TicketID
                    && TicketLevel == other.TicketLevel
                    && TicketFrom == other.TicketFrom
                    && TicketTo == other.TicketTo
                    && TicketStatus == other.TicketStatus
                    && TicketReply == other.TicketReply
                    && TicketUrgency == other.TicketUrgency
                    && TicketDate == other.TicketDate
                    && TicketSubject == other.TicketSubject
                    && TicketMessage == other.TicketMessage;
            }
            return false;
        }
    }
}
