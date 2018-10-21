using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GEPED.Web.Models
{
    public class CustomerViewModel
    {
        [Required(ErrorMessage = "Preencher Nome")]
        public string Name { get; set; }
    }
}