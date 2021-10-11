using ProjetoMVC01.Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC01.Presentation.Models
{
    
    public class TarefasConsultaModel
    {
        [Required(ErrorMessage = "Por favor, informe a data de início")]
        public string DataMin { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de início")]
        public string DataMax { get; set; }

        public List<Tarefa> Tarefas { get; set; }

    }
}
