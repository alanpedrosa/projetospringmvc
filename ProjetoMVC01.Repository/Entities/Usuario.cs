using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMVC01.Repository.Entities
{
    /// <summary>
    /// Classe de entidade para modelagem de Usuario
    /// </summary>
    public class Usuario
    {
        #region Propriedades

        public Guid IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }

        #endregion
    }
}
