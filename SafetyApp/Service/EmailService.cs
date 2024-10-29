using Microsoft.Extensions.Options;
using SafetyApp.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SafetyApp.Service
{
    public class EmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _fromEmail;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            var settings = emailSettings.Value;
            _smtpServer = settings.SmtpServer;
            _smtpPort = settings.SmtpPort;
            _smtpUsername = settings.SmtpUsername;
            _smtpPassword = settings.SmtpPassword;
            _fromEmail = settings.FromEmail;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using (var client = new SmtpClient(_smtpServer, _smtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_fromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
