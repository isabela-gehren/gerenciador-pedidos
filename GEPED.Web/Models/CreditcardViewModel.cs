using System;
using System.ComponentModel.DataAnnotations;

namespace GEPED.Web.Models
{
    public class CreditcardViewModel
    {
        [Required(ErrorMessage = "Preencher Número do Cartão")]
        [RegularExpression("([0-9]{16})$", ErrorMessage = "Preencher somente Números")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Preencher Nome do Titular do Cartão")]
        public string Holder { get; set; }

        [Required(ErrorMessage = "Preencher Data de Validade do Cartão")]
        [RegularExpression("([0-9]{2}/[0-9]{4})", ErrorMessage = "Preencher com uma Data válida")]
        [DataValidation]
        public string ExpirationDate { get; set; }

        [Required(ErrorMessage = "Preencher Código de Segurança do Cartão")]
        [Display(Name = "Código de Segurança")]
        [RegularExpression("([0-9]+)$", ErrorMessage = "Preencher somente Números")]
        [StringLength(3)]
        public string SecurityCode { get; set; }

        [Required(ErrorMessage = "Preencher Bandeira")]
        public string Brand { get; set; }
    }

    public class DataValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dataParts = value.ToString().Split('/');
            var data = new System.DateTime(Convert.ToInt32(dataParts[1]), Convert.ToInt32(dataParts[0]), DateTime.Today.Day);
            if (data >= DateTime.Today)
                return ValidationResult.Success;
            else
                return new ValidationResult("Preencher com uma Data válida");
        }
    }
}