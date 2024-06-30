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

        public void avisoDeSuspencionCurso(string emailDestino, string nombreCurso, string userName)
        {
            email = new MailMessage();
            //Se agrego el display ya que depende del correo le llegan los datos del From
            email.From = new MailAddress("noresponder@cursosPrograIII.com", "AVISO SUSPENCION DE CURSO - ProCursos");
            email.To.Add(emailDestino);
            email.Subject = "SUSPENCION DE CURSO: " + nombreCurso;
            email.IsBodyHtml = true;
            email.Body = "<div style=\"font-family: Arial, sans-serif; line-height: 1.6; color: #333; padding: 20px; border: 1px solid #ddd; border-radius: 10px; background-color: #f9f9f9;\">\r\n    <p style=\"font-size: 1.2em; font-weight: bold; color: #000;\">Estimado Usuario: "+ userName +",</p>\r\n    <p>Nos ponemos en contacto con usted para informarle que hemos recibido una denuncia relacionada con su curso <strong>"+ nombreCurso +"</strong> en nuestra plataforma.</p>\r\n    <p>Tras verificar la denuncia, hemos encontrado que el contenido del curso no cumple con las reglas y normativas de uso de nuestra plataforma. Por este motivo, hemos decidido suspender temporalmente el curso <strong>"+ nombreCurso +"</strong>.</p>\r\n    <p>Le solicitamos que, en un plazo máximo de 72 horas, realice las modificaciones necesarias en el contenido del curso para que cumpla con nuestras políticas. Una vez realizadas las modificaciones, por favor comuníquese con nosotros a través de esta dirección de correo electrónico.</p>\r\n    <p>Cuando verifiquemos que el contenido cumple con nuestras normativas, reactivaremos su curso en nuestra plataforma.</p>\r\n    <p style=\"font-style: italic; color: #666; margin-top: 20px;\">Agradecemos su comprensión y colaboración.</p>\r\n    <p><strong>Equipo de ProCurso</strong><br><em>Plataforma Educativa Colaborativa</em></p>\r\n</div>";

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
