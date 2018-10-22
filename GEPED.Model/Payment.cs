namespace GEPED.Model
{
    public class Payment
    {
        public string Provider { get { return "Simulado"; } }
        public string Type { get { return "CreditCard"; } }
        public int ServiceTaxAmount { get; set; }
        public int Installments { get; set; }
        public string Interest { get; set; }
        public bool Capture { get { return false; } }
        public bool Authenticate { get; set; }
        public bool Recurrent { get; set; }
        public Creditcard CreditCard { get; set; }
        public string ProofOfSale { get; set; }
        public string AcquirerTransactionId { get; set; }
        public string AuthorizationCode { get; set; }
        public string PaymentId { get; set; }
        public int Amount { get; set; }
        public string ReceivedDate { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
        public int ReasonCode { get; set; }
        public string ReasonMessage { get; set; }
        public int Status { get; set; }
        public string ProviderReturnCode { get; set; }
        public string ProviderReturnMessage { get; set; }
        public Link[] Links { get; set; }
    }
}