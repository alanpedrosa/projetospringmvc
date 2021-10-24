using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //validações
using ProjetoMVC01.Repository.Entities;

namespace ProjetoMVC01.Presentation.Models
{
    public class TarefasConsultaModel
    {
        //campo para capturar a data de inicio da consulta
        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public string DataMin { get; set; }

        //campo para capturar a data de termino da consulta
        [Required(ErrorMessage = "Por favor, informe a data de término.")]
        public string DataMax { get; set; }

        //resultado da consulta feito no banco de dados para exibir na página
        public List<Tarefa> Tarefas { get; set; }
    }
}
