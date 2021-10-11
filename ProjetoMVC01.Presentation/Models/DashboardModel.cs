using ProjetoMVC01.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC01.Presentation.Models
{
    public class DashboardModel
    {
        public DateTime DataAtual { get; set; }
        public List<Tarefa> Tarefas { get; set; }

        public int TotalPrioridadeBaixa { get; set; }
        public int TotalPrioridadeMedia { get; set; }
        public int TotalPrioridadeAlta { get; set; }
    }
}





