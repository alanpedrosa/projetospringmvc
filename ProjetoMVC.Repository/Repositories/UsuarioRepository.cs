using Dapper;
using ProjetoMVC01.Repository.Entities;
using ProjetoMVC01.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMVC01.Repository.Repositories
{
    /// <summary>
    /// Classe de repositorio de dados para Usuario
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        //atributo
        private readonly string _connectionstring;

        //construtor para receber o valor da connectionstring
        public UsuarioRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public void Create(Usuario obj)
        {
            var query = @"
                    INSERT INTO USUARIO(IDUSUARIO, NOME, EMAIL, SENHA, DATACADASTRO)
                    VALUES(
                        @IdUsuario,
                        @Nome,
                        @Email,
                        CONVERT(VARCHAR(32), HASHBYTES('MD5', @Senha), 2),
                        @DataCadastro
                    )
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, obj);
            }
        }

        public void Update(Usuario obj)
        {
            var query = @"
                    UPDATE USUARIO 
                    SET
                        NOME = @Nome,
                        EMAIL = @Email,
                        SENHA = CONVERT(VARCHAR(32), HASHBYTES('MD5', @Senha), 2)
                    WHERE
                        IDUSUARIO = @IdUsuario
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, obj);
            }
        }

        public void Delete(Usuario obj)
        {
            var query = @"
                    DELETE FROM USUARIO
                    WHERE IDUSUARIO = @IdUsuario
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, obj);
            }
        }

        public List<Usuario> GetAll()
        {
            var query = @"
                    SELECT * FROM USUARIO
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                    .Query<Usuario>(query)
                    .ToList();
            }
        }

        public Usuario GetById(Guid id)
        {
            var query = @"
                    SELECT * FROM USUARIO
                    WHERE IDUSUARIO = @id
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                    .Query<Usuario>(query, new { id })
                    .FirstOrDefault();
            }
        }

        public Usuario Get(string email)
        {
            var query = @"
                    SELECT * FROM USUARIO
                    WHERE EMAIL = @email
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                    .Query<Usuario>(query, new { email })
                    .FirstOrDefault();
            }
        }

        public Usuario Get(string email, string senha)
        {
            var query = @"
                    SELECT * FROM USUARIO
                    WHERE EMAIL = @email
                    AND SENHA = CONVERT(VARCHAR(32), HASHBYTES('MD5', @senha), 2)
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                    .Query<Usuario>(query, new { email, senha })
                    .FirstOrDefault();
            }
        }
    }
}





