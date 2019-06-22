using System.Collections.Generic;

namespace InternetX
{
    enum CommandCode
    {
        ZoneCreate ,
        ZoneUpdate ,
        ZoneUpdateBulk ,
        ZoneImport ,
        ZoneDelete ,
        ZoneInquire ,
    }

    class CommandCodeLookup
    {
        readonly Dictionary<CommandCode, string> m_Dictionary = new Dictionary<CommandCode, string>();

        public CommandCodeLookup()
        {
            m_Dictionary.Add(CommandCode.ZoneCreate, "0201");
            m_Dictionary.Add(CommandCode.ZoneUpdate, "0202");
            m_Dictionary.Add(CommandCode.ZoneUpdateBulk, "0202001");
            m_Dictionary.Add(CommandCode.ZoneImport, "0204");
            m_Dictionary.Add(CommandCode.ZoneDelete, "0203");
            m_Dictionary.Add(CommandCode.ZoneInquire, "0205");
        }

        public string this[CommandCode code]
        {
            get
            {
                return m_Dictionary[code];
            }
        }
    }
}
