using Cave.Data;
using System.Net;

namespace Cave.AutoDns
{
    public struct ImscpConfig
    {
        [Field]
        public string UserName;
        [Field]
        public string Password;
        [Field]
        public string Hostname;
        [Field]
        public string Database;
        [Field]
        public IPAddress DomainIP;
    }
}
