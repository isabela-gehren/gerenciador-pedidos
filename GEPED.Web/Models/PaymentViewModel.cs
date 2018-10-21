using System.ComponentModel.DataAnnotations;

namespace GEPED.Web.Models
{
    public class PaymentViewModel
    {
        [Required(ErrorMessage = "Preencher Valor")]
        public int Amount { get; set; }
        public bool Capture { get; set; }
        [Required(ErrorMessage = "Preencher Quantidade de Parcelas")]
        public int Installments { get; set; }
        public CreditcardViewModel CreditCard { get; set; }
    }
}