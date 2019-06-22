namespace InternetX
{
    public class AutodnsRecordItem
    {
        public Autodns Autodns { get; }

        public string Zone { get; }
        
        public DnsResourceRecord[] Records { get; }

        public AutodnsRecordItem(Autodns a, string zone, DnsResourceRecord[] records)
        {
            Autodns = a;
            Zone = zone;
            Records = records;
        }

        public override string ToString()
        {
            return "Zone " + Zone;
        }
    }
}
