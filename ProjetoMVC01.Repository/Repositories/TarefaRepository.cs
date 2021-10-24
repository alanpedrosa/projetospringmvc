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
    /// Classe de repositorio de dados para Tarefa
    /// </summary>
    public class TarefaRepository : ITarefaRepository
    {
        //atributo
        private readonly string _connectionstring;

        //construtor para receber o valor da connectionstring
        public TarefaRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public void Create(Tarefa obj)
        {
            var query = @"
                    INSERT INTO TAREFA(IDTAREFA, NOME, DESCRICAO, DATA, HORA, PRIORIDADE, IDUSUARIO)
                    VALUES(@IdTarefa, @Nome, @Descricao, @Data, @Hora, @Prioridade, @IdUsuario)
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, obj);
            }
        }

        public void Update(Tarefa obj)
        {
            var query = @"
                    UPDATE TAREFA
                    SET
                        NOME = @Nome,
                        DESCRICAO = @Descricao,
                        DATA = @Data,
                        HORA = @Hora,
                        PRIORIDADE = @Prioridade
                    WHERE
                        IDTAREFA = @IdTarefa
                    AND
                        IDUSUARIO = @IdUsuario
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, obj);
            }
        }

        public void Delete(Tarefa obj)
        {
            var query = @"
                    DELETE FROM TAREFA
                    WHERE IDTAREFA = @IdTarefa
                    AND IDUSUARIO = @IdUsuario
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, obj);
            }
        }

        public List<Tarefa> GetAll()
        {
            var query = @"
                    SELECT * FROM TAREFA
                    ORDER BY 
                        DATA DESC, 
                        HORA DESC
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                    .Query<Tarefa>(query)
                    .ToList();
            }
        }

        public Tarefa GetById(Guid id)
        {
            var query = @"
                    SELECT * FROM TAREFA
                    WHERE IDTAREFA = @id
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                    .Query<Tarefa>(query, new { id })
                    .FirstOrDefault();
            }
        }

        public List<Tarefa> GetByDatas(DateTime dataMin, DateTime dataMax, Guid idUsuario)
        {
            var query = @"
                    SELECT * FROM TAREFA
                    WHERE DATA BETWEEN @dataMin AND @dataMax
                    AND IDUSUARIO = @idUsuario
                    ORDER BY DATA ASC, HORA ASC
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                    .Query<Tarefa>(query, new { dataMin, dataMax, idUsuario })
                    .ToList();
            }
        }
    }
}
