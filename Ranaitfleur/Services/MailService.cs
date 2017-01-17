using System.Diagnostics;
using System.Net;
using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit.Net.Smtp;

namespace Ranaitfleur.Services
{
    public class MailService : IMailService
    {
        private ILogger<MailService> _logger;

        public MailService(ILogger<MailService> logger)
        {
            _logger = logger;
        }

        public void SendMailDummy(string to, string from, string subject, string body)
        {
            Debug.WriteLine($"Sending Mail: To:{to} From:{from} Subject:{subject}");
        }

        public async void SendMail(string to, string from, string subject, string body)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Ranaitfleur", from));
            emailMessage.To.Add(new MailboxAddress("Ranaitfleur", to));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = body };

            using (var client = new SmtpClient())
            {
                var credentials = new NetworkCredential
                {
                    UserName = "YourEMAIL", // replace with valid value
                    Password = "YourPASSWORD" // replace with valid value
                };
                
                // for accepting every ssl certificate, DO NOT USE ON PROD, used for testing with gmail
                client.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;

                // check your smtp server setting and amend accordingly:
                await client.ConnectAsync("smtp.gmail.com", 465, true).ConfigureAwait(false);
                await client.AuthenticateAsync(credentials);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}
