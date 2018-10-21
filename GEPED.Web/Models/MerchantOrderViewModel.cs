using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GEPED.Web.Models
{
    public class MerchantOrderViewModel
    {        
        public string MerchantOrderId { get; set; }
        public CustomerViewModel Customer { get; set; }
        public PaymentViewModel Payment { get; set; }
    }
}