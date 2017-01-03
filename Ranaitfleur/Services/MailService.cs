using System.Diagnostics;
using System.Net;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;

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

        public async void SendMail(string to, string form, string subject, string body)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Ranaitfleur", to));
            emailMessage.To.Add(new MailboxAddress("Ranaitfleur", to));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = body };

            using (var client = new SmtpClient())
            {
                //TODO: Figure out how to make it work.
                var credentials = new NetworkCredential
                {
                    UserName = "shahed011", // replace with valid value
                    Password = "#Compaq#11#", // replace with valid value
                    Domain = "hotmail.com"
                };

                client.LocalDomain = "hotmail.com";
                // check your smtp server setting and amend accordingly:
                await client.ConnectAsync("smtp.outlook.com", 587, false).ConfigureAwait(false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(credentials);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);

                //client.LocalDomain = "smtp-mail.outlook.com";
                //await client.ConnectAsync("smtp-mail.outlook.com", 587, SecureSocketOptions.None).ConfigureAwait(false);
                //client.AuthenticationMechanisms.Remove("XOAUTH2");
                //await client.AuthenticateAsync(to, "#Compaq#11#").ConfigureAwait(false);
                //await client.SendAsync(emailMessage).ConfigureAwait(false);
                //await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}
