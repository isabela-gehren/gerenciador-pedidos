namespace GEPED.Model
{
    public class Order
    {        
        public string MerchantOrderId { get; set; }
        public Customer Customer { get; set; }
        public Payment Payment { get; set; }
    }
}