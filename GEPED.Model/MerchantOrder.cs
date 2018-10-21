namespace GEPED.Model
{
    public class MerchantOrder
    {        
        public string MerchantOrderId { get; set; }
        public Customer Customer { get; set; }
        public Payment Payment { get; set; }
    }
}