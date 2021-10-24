using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC01.Presentation.Models
{
    public class TarefasRelatorioModel
    {
        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public string DataInicio { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de término.")]
        public string DataTermino { get; set; }

        [Required(ErrorMessage = "Por favor, selecione o formato do relatório.")]
        public string Formato { get; set; }
    }
}
