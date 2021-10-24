using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjetoMVC01.Presentation.Models
{
    public class TarefasEdicaoModel
    {
        public Guid IdTarefa { get; set; } //campo oculto na página

        [Required(ErrorMessage = "Por favor, informe o nome da tarefa.")]
        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data da tarefa.")]
        public string Data { get; set; }

        [Required(ErrorMessage = "Por favor, informe a hora da tarefa.")]
        public string Hora { get; set; }

        [MinLength(10, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(250, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a descrição da tarefa.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Por favor, informe a prioridade da tarefa.")]
        public string Prioridade { get; set; }
    }
}
