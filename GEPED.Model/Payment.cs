namespace GEPED.Model
{
    public class Payment
    {
        public string Provider { get { return "Simulado"; } }
        public string Type { get { return "CreditCard"; } }
        public int Amount { get; set; }
        public bool Capture { get { return true; } }
        public int Installments { get; set; }
        public Creditcard CreditCard { get; set; }
    }
}