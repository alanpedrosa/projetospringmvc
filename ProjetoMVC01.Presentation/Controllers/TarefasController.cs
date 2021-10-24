using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoMVC01.Presentation.Models;
using ProjetoMVC01.Reports.Excel;
using ProjetoMVC01.Reports.Pdf;
using ProjetoMVC01.Repository.Entities;
using ProjetoMVC01.Repository.Enums;
using ProjetoMVC01.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC01.Presentation.Controllers
{
    [Authorize]
    public class TarefasController : Controller
    {
        //atributos
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        //construtor para inicialização dos atributos da classe
        public TarefasController(ITarefaRepository tarefaRepository, IUsuarioRepository usuarioRepository)
        {
            _tarefaRepository = tarefaRepository;
            _usuarioRepository = usuarioRepository;
        }

        //método para abrir a página /Tarefas/Cadastro
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost] //capturar os dados enviados pelo formulario (SUBMIT)
        public IActionResult Cadastro(TarefasCadastroModel model)
        {
            //verificar se todos os campos da model passaram nas validações
            if(ModelState.IsValid)
            {
                try
                {
                    //capturar o usuario autenticado no sistema
                    var usuario = _usuarioRepository.Get(User.Identity.Name);

                    var tarefa = new Tarefa();

                    tarefa.IdTarefa = Guid.NewGuid();
                    tarefa.Nome = model.Nome;
                    tarefa.Data = DateTime.Parse(model.Data);
                    tarefa.Hora = TimeSpan.Parse(model.Hora);
                    tarefa.Descricao = model.Descricao;
                    tarefa.Prioridade = (PrioridadeTarefa)int.Parse(model.Prioridade);
                    tarefa.IdUsuario = usuario.IdUsuario; //chave estrangeira

                    //gravar no banco de dados
                    _tarefaRepository.Create(tarefa);

                    TempData["MensagemSucesso"] = $"Tarefa {tarefa.Nome}, cadastrado com sucesso.";
                    ModelState.Clear();
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        //método para abrir a página /Tarefas/Consulta
        public IActionResult Consulta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Consulta(TarefasConsultaModel model)
        {
            //verificar se todos os campos passaram nas validações
            if(ModelState.IsValid)
            {
                try
                {
                    //capturar o usuario autenticado no sistema
                    var usuario = _usuarioRepository.Get(User.Identity.Name);

                    //capturar as datas informadas no formulario
                    var dataMin = DateTime.Parse(model.DataMin);
                    var dataMax = DateTime.Parse(model.DataMax);

                    //consultar as tarefas e armazenar o resultado obtido
                    model.Tarefas = _tarefaRepository.GetByDatas(dataMin, dataMax, usuario.IdUsuario);
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View(model); //devolver a 'model' para a página
        }

        //método para abrir a página /Tarefas/Relatorio
        public IActionResult Relatorio()
        {
            return View();
        }

        //método para capturar o SUBMIT do formulário de relatorio
        [HttpPost]
        public IActionResult Relatorio(TarefasRelatorioModel model)
        {
            if(ModelState.IsValid) //verificar se os campos passaram nas validações..
            {
                try
                {
                    //capturar o usuario autenticado no sistema
                    var usuario = _usuarioRepository.Get(User.Identity.Name);

                    //capturar as datas informadas no formulário..
                    var dataInicio = DateTime.Parse(model.DataInicio);
                    var dataTermino = DateTime.Parse(model.DataTermino);

                    //consultar as tarefas no banco de dados..
                    var tarefas = _tarefaRepository.GetByDatas(dataInicio, dataTermino, usuario.IdUsuario);

                    //verificar o formato do relatorio selecionado..
                    switch(model.Formato)
                    {
                        case "EXCEL":

                            //gerando um relatorio de tarefas em arquivo excel
                            var tarefasReportExcel = new TarefasReportExcel();
                            var excel = tarefasReportExcel.GerarRelatorio(tarefas);

                            //fazendo download do arquivo excel..
                            return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "tarefas.xlsx");

                        case "PDF":

                            //gerando um relatorio de tarefas em arquivo PDF
                            var tarefasReportPdf = new TarefasReportPdf();
                            var pdf = tarefasReportPdf.GerarRelatorio(tarefas);

                            //fazendo download do arquivo pdf..
                            return File(pdf, "application/pdf", "tarefas.pdf");
                    }
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        //método para abrir a página /Tarefas/Edicao?id={}
        public IActionResult Edicao(Guid id)
        {
            var model = new TarefasEdicaoModel();

            try
            {
                //buscar os dados da tarefa no banco atraves do ID..
                var tarefa = _tarefaRepository.GetById(id);

                //passar os dados da tarefa para a classe model
                model.IdTarefa = tarefa.IdTarefa;
                model.Nome = tarefa.Nome;
                model.Data = tarefa.Data.ToString("yyyy-MM-dd");
                model.Hora = tarefa.Hora.ToString();
                model.Descricao = tarefa.Descricao;
                model.Prioridade = ((int)tarefa.Prioridade).ToString();
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View(model); //enviando o objeto 'model' para a página
        }

        [HttpPost]
        public IActionResult Edicao(TarefasEdicaoModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    //capturar o usuario autenticado no sistema
                    var usuario = _usuarioRepository.Get(User.Identity.Name);

                    var tarefa = new Tarefa();

                    tarefa.IdTarefa = model.IdTarefa;
                    tarefa.Nome = model.Nome;
                    tarefa.Data = DateTime.Parse(model.Data);
                    tarefa.Hora = TimeSpan.Parse(model.Hora);
                    tarefa.Descricao = model.Descricao;
                    tarefa.Prioridade = (PrioridadeTarefa)int.Parse(model.Prioridade);
                    tarefa.IdUsuario = usuario.IdUsuario;

                    _tarefaRepository.Update(tarefa);

                    TempData["MensagemSucesso"] = $"Tarefa {tarefa.Nome}, atualizado com sucesso.";
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        //método para excluir uma tarefa URL: /Tarefas/Exclusao?id={}
        public IActionResult Exclusao(Guid id)
        {
            try
            {
                //capturar o usuario autenticado no sistema
                var usuario = _usuarioRepository.Get(User.Identity.Name);

                //buscar a tarefa no banco de dados atraves do ID..
                var tarefa = _tarefaRepository.GetById(id);
                tarefa.IdUsuario = usuario.IdUsuario;

                //excluir a tarefa
                _tarefaRepository.Delete(tarefa);

                //mensagem na página
                TempData["MensagemSucesso"] = $"Tarefa {tarefa.Nome}, excluída com sucesso.";
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            //redirecionar para a página de consulta
            return RedirectToAction("Consulta");
        }

    }
}
