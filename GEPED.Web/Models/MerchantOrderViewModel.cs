namespace GEPED.Web.Models
{
    public class MerchantOrderViewModel
    {        
        public string Id { get; set; }
        public CustomerViewModel Customer { get; set; }
        public PaymentViewModel Payment { get; set; }
        public string Status { get; set; }
    }
}