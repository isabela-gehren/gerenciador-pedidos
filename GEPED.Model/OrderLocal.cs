using System;

namespace GEPED.Model
{
    public class OrderLocal : CrudBase
    {
        public DateTime DtOrder { get; set; }
        public Customer Customer { get; set; }
        public Payment Payment { get; set; }
        public OrderStatus Status { get; set; }
    }
}
