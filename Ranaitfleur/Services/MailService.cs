using System.Diagnostics;
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

        public void SendMail(string to, string from, string subject, string body)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Ranaitfleur", "postmaster@ranaitfleur.com"));
            emailMessage.To.Add(new MailboxAddress("Ranaitfleur", "info@ranaitfleur.com"));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = body };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;
                client.Connect("mail.ranaitfleur.com", 465, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("postmaster@ranaitfleur.com", "#Gunners#69#");

                client.Send(emailMessage);
                client.Disconnect(true);
                //var credentials = new NetworkCredential
                //{
                //    UserName = "postmaster@ranaitfleur.com", // replace with valid value
                //    Password = "#Gunners#69#", // replace with valid value
                //    Domain = "mail.ranaitfleur.com"
                //};

                //// for accepting every ssl certificate, DO NOT USE ON PROD, used for testing with gmail
                //client.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;

                //// check your smtp server setting and amend accordingly:
                //await client.ConnectAsync("mail.ranaitfleur.com", 465, true).ConfigureAwait(false);
                //await client.AuthenticateAsync(credentials);
                //await client.SendAsync(emailMessage).ConfigureAwait(false);
                //await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}
