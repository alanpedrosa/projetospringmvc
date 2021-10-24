using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoMVC01.Messages;
using ProjetoMVC01.Presentation.Models;
using ProjetoMVC01.Repository.Entities;
using ProjetoMVC01.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjetoMVC01.Presentation.Controllers
{
    public class AccountController : Controller
    {
        //atributo
        private readonly IUsuarioRepository _usuarioRepository;

        //construtor com entrada de argumentos
        public AccountController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountLoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //consultar o usuaro no banco de dados atraves do email e senha
                    var usuario = _usuarioRepository.Get(model.Email, model.Senha);

                    //verificar se o usuario foi encontrado..
                    if (usuario != null)
                    {
                        //criando uma autorização de acesso para o usuario
                        var autorizacao = new ClaimsIdentity(
                            new[] { new Claim(ClaimTypes.Name, usuario.Email) },
                            CookieAuthenticationDefaults.AuthenticationScheme
                            );

                        //gravar esta autorização em um arquivo de cookies
                        var claimPrincipal = new ClaimsPrincipal(autorizacao);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal);

                        //redirecionar para a página /Home/Index
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Acesso negado. Usuário inválido.";
                    }
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountRegisterModel model)
        {
            if (ModelState.IsValid) //todos os campos passaram na validação
            {
                try
                {
                    //verificar se o email informado ja esta cadastrado no banco de dados
                    if (_usuarioRepository.Get(model.Email) != null)
                    {
                        TempData["MensagemAlerta"] = $"O email {model.Email} já está cadastrado no sistema, tente outro.";
                    }
                    else
                    {
                        var usuario = new Usuario();

                        usuario.IdUsuario = Guid.NewGuid();
                        usuario.Nome = model.Nome;
                        usuario.Email = model.Email;
                        usuario.Senha = model.Senha;
                        usuario.DataCadastro = DateTime.Now;

                        _usuarioRepository.Create(usuario);

                        TempData["MensagemSucesso"] = "Sua conta de usuário foi criada!";
                        ModelState.Clear(); //limpar os campos do formulário
                    }
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        //método para abrir a página de dados do usuario (minha conta)
        [Authorize]
        public IActionResult UserData()
        {
            try
            {
                //buscar o usuario no banco de dados atraves do email..
                var usuario = _usuarioRepository.Get(User.Identity.Name);

                //imprimir os dados do usuario na página
                TempData["IdUsuario"] = usuario.IdUsuario;
                TempData["Nome"] = usuario.Nome;
                TempData["Email"] = usuario.Email;
                TempData["DataCadastro"] = usuario.DataCadastro.ToString("dd/MM/yyyy HH:mm");
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View();
        }

        //método para realizar o logout do usuário
        public IActionResult Logout()
        {
            //destruir o cookie que contem a autorização do usuário
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //redirecionar de volta para a página de login
            return RedirectToAction("Login", "Account");
        }

        //método para abrir a página /Account/ChangePassword
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        //método para capturar o submit POST do formulário
        [Authorize]
        [HttpPost]
        public IActionResult ChangePassword(AccountChangePasswordModel model)
        {
            //verificando se os campos passaram nas validações
            if(ModelState.IsValid)
            {
                try
                {
                    //capturar os dados do usuario autenticado no sistema
                    var usuario = _usuarioRepository.Get(User.Identity.Name);

                    //verificar se a senha atual informada está correta
                    if(_usuarioRepository.Get(usuario.Email, model.SenhaAtual) != null)
                    {
                        //alterar a senha do usuario
                        usuario.Senha = model.NovaSenha;
                        _usuarioRepository.Update(usuario);

                        TempData["MensagemSucesso"] = "Senha alterada com sucesso.";
                        ModelState.Clear(); //limpar os campos do formulario
                    }
                    else
                    {
                        TempData["MensagemAlerta"] = "Senha atual está incorreta.";
                    }
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        //método para abrir a página de recuperação de senha
        public IActionResult PasswordRecover()
        {
            return View();
        }

        //método para capturar o SUBMIT do formulário
        [HttpPost] //recebe o evento SUBMIT do formulário
        public IActionResult PasswordRecover(AccountPasswordRecoverModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    //buscar o usuario no banco de dados atraves do email
                    var usuario = _usuarioRepository.Get(model.Email);

                    //verificar se o usuario foi encontrado
                    if(usuario != null)
                    {
                        //gerar uma nova senha para o usuario
                        usuario.Senha = new Random().Next(999999999).ToString();
                        _usuarioRepository.Update(usuario);

                        //enviando email para o usuario
                        var assunto = "Recuperação de senha de usuário - Sistema de controle de tarefas";
                        var corpo = $@"
                            <div style='text-align: center; margin: 40px; padding: 60px; border: 2px solid #ccc; font-size: 16pt;'>
                            <img src='https://www.cotiinformatica.com.br/imagens/logo-coti-informatica.png' />
                            <br/><br/>
                            Olá <strong>{usuario.Nome}</strong>,
                            <br/><br/>    
                            O sistema gerou uma nova senha para que você possa acessar sua conta.<br/>
                            Por favor utilize a senha: <strong>{usuario.Senha}</strong>
                            <br/><br/>
                            Não esqueça de, ao acessar o sistema, atualizar esta senha para outra
                            de sua preferência.
                            <br/><br/>              
                            Att<br/>   
                            Equipe COTI Informatica
                            </div>
                        ";

                        var emailService = new EmailService();
                        emailService.SendMessage(usuario.Email, assunto, corpo);

                        TempData["MensagemSucesso"] = "Nova senha enviada para o email com sucesso.";
                        ModelState.Clear();
                    }
                    else
                    {
                        throw new Exception("Endereço de email inválido.");
                    }
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }
    }
}
