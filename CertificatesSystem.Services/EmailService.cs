using System.Diagnostics;
using CertificatesSystem.Models.Interfaces;
using System.Net;
using System.Net.Mail;

namespace CertificatesSystem.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var message = new MailMessage();

                message.From = new MailAddress("jaimenava321@gmail.com");
                message.To.Add(new MailAddress(email));

                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = htmlMessage;

                var smtp = GetSmtpClient();
                
                await smtp.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private SmtpClient GetSmtpClient()
        {
            var smtp = new SmtpClient();
                
            smtp.Port = 587;
            smtp.Host = "smtp-relay.sendinblue.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("jaimenava321@gmail.com", "COgjGHbLkFyDhXrQ");

            return smtp;
        }
    }
}