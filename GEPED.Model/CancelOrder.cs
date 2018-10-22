namespace GEPED.Model
{
    public class CancelOrder
    {
        public int Status { get; set; }
        public int ReasonCode { get; set; }
        public string ReasonMessage { get; set; }
        public string ProviderReasonCode { get; set; }
        public string ProviderReasonMessage { get; set; }
        public Link[] Links { get; set; }
    }
}
