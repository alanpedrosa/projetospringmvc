using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC01.Presentation.Models
{
    public class AccountLoginModel
    {
        [EmailAddress(ErrorMessage = "Por favor, informe um email válido.")]
        [Required(ErrorMessage = "Por favor, informe seu email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, informe sua senha.")]
        public string Senha { get; set; }
    }
}





