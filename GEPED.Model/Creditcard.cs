using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GEPED.Model
{
    public class Creditcard
    {
        public string CardNumber { get; set; }
        public string Holder { get; set; }
        public string ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public string Brand { get; set; }
    }
}