using ProjetoMVC01.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMVC01.Repository.Interfaces
{
    /// <summary>
    /// Interface de repositorio especifica para Usuario
    /// </summary>

    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {

        #region Métodos Abstratos

        Usuario Get(string email);
        Usuario Get(string email, string senha);



        #endregion


    }
}
