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
            server = new SmtpClient("smtp.gmail.com", 587);
            server.Credentials = new NetworkCredential("jpablo903r@gmail.com", "penf fiwd bwpf ikbf");
            server.DeliveryMethod = SmtpDeliveryMethod.Network;
            server.EnableSsl = true;
        }

        public void RespuestaDenuncia(string emailDestino, string asunto)
        {
            email = new MailMessage();
            //Se agrego el display ya que depende del correo le llegan los datos del From
            email.From = new MailAddress("noresponder@cursosPrograIII.com", "noresponder@cursosPrograIII.com");
            email.To.Add(emailDestino);
            email.Subject = "Denuncia: " + asunto;
            email.IsBodyHtml = true;
            email.Body = "<h1>Denuncia recibida</h1> <br>Hola, hemos recibido tu denuncia. Te responderemos luego de un proceso de evaluación de la misma. Gracias por hacer de nuestra plataforma un lugar más seguro!";

            email.Headers.Add("X-Mailer", "MyAppMailer");
        }

        public void RecuperoPassword(string emailDestino, string pass)
        {
            email = new MailMessage();
            //Se agrego el display ya que depende del correo le llegan los datos del From
            email.From = new MailAddress("noresponder@cursosPrograIII.com", "noresponder@cursosPrograIII.com");
            email.To.Add(emailDestino);
            email.Subject = "Recuperar contraseña";
            email.IsBodyHtml = true;
            email.Body = "<h1>Contraseña: " + pass + "</h1> <br>Hola, esta es tu contraseña. Te recomendamos cambiarla para mayor seguridad";

            email.Headers.Add("X-Mailer", "MyAppMailer");
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
