using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMVC01.Repository.Interfaces
{
    /// <summary>
    /// Interface genérica para definir os métodos base do repositorio
    /// </summary>
    /// <typeparam name="T">Tipo generico para a entidade</typeparam>
    public interface IBaseRepository<T>
    {
        #region Métodos abstratos

        void Create(T obj);
        void Update(T obj);
        void Delete(T obj);

        List<T> GetAll();
        T GetById(Guid id);

        #endregion
    }
}
