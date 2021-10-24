using ProjetoMVC01.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMVC01.Repository.Interfaces
{
    /// <summary>
    /// Interface de repositorio especifica para Tarefa
    /// </summary>
    public interface ITarefaRepository : IBaseRepository<Tarefa>
    {
        #region Métodos abstratos

        List<Tarefa> GetByDatas(DateTime dataMin, DateTime dataMax, Guid idUsuario);

        #endregion
    }
}
