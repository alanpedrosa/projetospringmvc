using ProjetoMVC01.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMVC01.Repository.Interfaces
{
    interface ITarefaRepository : IBaseRepository<Tarefa>
    {
        #region Métodos Abstratos
        List<Tarefa> GetByDatas(DateTime datamin, DateTime datamax, Guid IdUsuario);
        #endregion
    }
}
