using System;
using System.Net;
using System.Net.Mail;

namespace ProjetoMVC01.Messages
{
    /// <summary>
    /// Classe para implementar as rotinas de envio de email do sistema
    /// </summary>
    public class EmailService
    {
        #region Atributos privados

        private readonly string _conta = "cotiaulasnoreply@gmail.com";
        private readonly string _senha = "@AdminCOTI123";
        private readonly string _smtp = "smtp.gmail.com";
        private readonly int _porta = 587;

        #endregion

        public void SendMessage(string email, string assunto, string corpo)
        {
            #region Montando o conteudo do email

            var mailMessage = new MailMessage(_conta, email);
            mailMessage.Subject = assunto;
            mailMessage.Body = corpo;
            mailMessage.IsBodyHtml = true;

            #endregion

            #region Enviando o email

            var smtpClient = new SmtpClient(_smtp, _porta);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_conta, _senha);
            smtpClient.Send(mailMessage);

            #endregion
        }
    }
}
