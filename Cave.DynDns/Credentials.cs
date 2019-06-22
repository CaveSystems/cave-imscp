#region Authors & Contributors
/*
   Author:
     Andreas Rohleder <a.rohleder@cavesystems.de>

   Contributors:

   Copyright (c) 2003-2014 CaveSystems UG (http://www.cavesystems.de)
 */
#endregion
#region LICENSE
/*
    This program/library/sourcecode is free software; you can redistribute it
    and/or modify it under the terms of the GNU General Public License
    version 3 as published by the Free Software Foundation subsequent called
    the License.

    You may not use this program/library/sourcecode except in compliance
    with the License. The License is included in the LICENSE.GPL30 file
    found at the installation directory or the distribution package.

    Permission is hereby granted, free of charge, to any person obtaining
    a copy of this software and associated documentation files (the
    "Software"), to deal in the Software without restriction, including
    without limitation the rights to use, copy, modify, merge, publish,
    distribute, sublicense, and/or sell copies of the Software, and to
    permit persons to whom the Software is furnished to do so, subject to
    the following conditions:

    The above copyright notice and this permission notice shall be included
    in all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
    EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
    NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
    LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
    OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion LICENSE

using Cave.IO;
using Imscp;

namespace Cave.DynDns
{
    public class Credentials
    {
        public string Email { get; }
        public DomainDns DomainDns { get; }
        public DynDnsDomain DynDnsDomain { get; }
        public string PlainPassword { get; }

        public Credentials(long id, string p_Email, DomainDns domainDns)
        {
            DomainDns = domainDns;
            Email = p_Email;
            PlainPassword = DefaultRNG.GetPassword(25, ASCII.Strings.SafeUrlOptions);
            var dom = new DynDnsDomain()
            {
                ID = id,
                Username = domainDns.Name,
            };
            dom.CreatePasswordHash(PlainPassword);
            DynDnsDomain = dom;
        }
    }
}