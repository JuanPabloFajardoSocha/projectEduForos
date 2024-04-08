using System.Net;
using System.Net.Mail;
using eduForos.Application.Common.Interfaces.Services.Others;

namespace eduforos.Infrastructure.Settings.Others;

public class Email : IEmailService
{
    public bool SentEmail(string emailSent, string asunto, string mensaje)
    {
        // credenciales
        string EmailEduforos = "speearscollectionbbc@gmail.com";
        string contrasena = "dpzlxdotndzhalkj";

        using (var smtpClient = new SmtpClient("smtp.gmail.com"))
        {
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(EmailEduforos, contrasena);
            smtpClient.EnableSsl = true;

            // Mensage config
            var correo = new MailMessage
            {
                From = new MailAddress(EmailEduforos),
                Subject = asunto,
                Body = mensaje
            };

            correo.To.Add(emailSent);

            try
            {
                smtpClient.Send(correo);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("error en el envio del mensaje: " + ex);
            }
        };


    }
}