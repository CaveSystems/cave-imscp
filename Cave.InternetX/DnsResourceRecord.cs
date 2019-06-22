namespace InternetX
{
    public struct DnsResourceRecord
    {
        public uint Pref;
        public uint TTL;
        public string Name;
        public string Type;
        public string Value;

        public override bool Equals(object obj)
        {
            if (obj is DnsResourceRecord)
            {
                DnsResourceRecord other = (DnsResourceRecord)obj;
                bool result = Pref == other.Pref && TTL == other.TTL && Name == other.Name && Type == other.Type && Value == other.Value;
                return result;
            }
            return false;
        }

        public override string ToString()
        {
            return Name + " " + Type + " " + Value;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
