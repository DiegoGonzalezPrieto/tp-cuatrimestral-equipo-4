using System;
using System.Net.Mail;
using System.Net;

namespace negocio
{
    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;

        public EmailService()
        {
            server = new SmtpClient("sandbox.smtp.mailtrap.io", 2525);
            server.Credentials = new NetworkCredential("0035cc8ecd89ac", "2fb0a47777c1cb");
            server.EnableSsl = true;
        }

        public void RespuestaDenuncia(string emailDestino, string asunto)
        {
            email = new MailMessage();
            email.From = new MailAddress("noresponder@cursosPrograIII.com");
            email.To.Add(emailDestino);
            email.Subject = "Denuncia: " + asunto;
            email.IsBodyHtml = true;
            email.Body = "<h1>Denuncia recibida</h1> <br>Hola, hemos recibido tu denuncia. Te responderemos luego de un proceso de evaluación de la misma. Gracias por hacer de nuestra plataforma un lugar más seguro!";
        }

        public void enviarEmail()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
