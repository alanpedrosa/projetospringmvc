using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjetoMVC01.Presentation.Models
{
    public class AccountPasswordRecoverModel
    {
        [EmailAddress(ErrorMessage = "Por favor, informe um email válido.")]
        [Required(ErrorMessage = "Por favor, informe o seu email.")]
        public string Email { get; set; }
    }
}
