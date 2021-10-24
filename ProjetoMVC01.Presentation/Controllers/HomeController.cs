using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoMVC01.Presentation.Models;
using ProjetoMVC01.Repository.Enums;
using ProjetoMVC01.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC01.Presentation.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //atributos
        private readonly ITarefaRepository _tarefarepository;
        private readonly IUsuarioRepository _usuariorepository;

        //construtor para inicialização dos atributos
        public HomeController(ITarefaRepository tarefarepository, IUsuarioRepository usuariorepository)
        {
            _tarefarepository = tarefarepository;
            _usuariorepository = usuariorepository;
        }

        //método para abrir a página /Home/Index
        public IActionResult Index()
        {
            var model = new DashboardModel();

            try
            {
                //capturar o usuario autenticado no sistema
                var usuario = _usuariorepository.Get(User.Identity.Name);

                model.DataAtual = DateTime.Now.Date;
                model.Tarefas = _tarefarepository.GetByDatas(model.DataAtual, model.DataAtual, usuario.IdUsuario);

                //calculando a quantidade total de cada prioridade
                model.TotalPrioridadeBaixa = model.Tarefas.Count(t => t.Prioridade == PrioridadeTarefa.BAIXA);
                model.TotalPrioridadeMedia = model.Tarefas.Count(t => t.Prioridade == PrioridadeTarefa.MEDIA);
                model.TotalPrioridadeAlta = model.Tarefas.Count(t => t.Prioridade == PrioridadeTarefa.ALTA);
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            //enviando a model para a página
            return View(model);
        }
    }
}
