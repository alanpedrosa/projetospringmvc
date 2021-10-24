using ProjetoMVC01.Repository.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMVC01.Repository.Entities
{
    /// <summary>
    /// Classe de entidade para modelagem de Tarefa
    /// </summary>
    public class Tarefa
    {
        #region Propriedades

        public Guid IdTarefa { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
        public PrioridadeTarefa Prioridade { get; set; }
        public Guid IdUsuario { get; set; }

        #endregion
    }
}
