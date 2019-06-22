using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetX
{
    public class AutodnsRecordList
    {
        readonly Dictionary<string, AutodnsRecordItem> m_List = new Dictionary<string, AutodnsRecordItem>();

        public AutodnsRecordItem this[string zone]
        {
            get
            {
                return m_List[zone];
            }
            internal set
            {
                m_List[zone] = value;
            }
        }

        public ICollection<string> Zones
        {
            get { return m_List.Keys; }
        }

        public ICollection<AutodnsRecordItem> Items
        {
            get { return m_List.Values; }
        }

        public int Count { get { return m_List.Count; } }
    }
}
