using System.Diagnostics;
using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;

namespace Ranaitfleur.Services
{
    public class MailService : IMailService
    {
        private ILogger<MailService> _logger;
        private readonly IConfigurationRoot _config;

        public MailService(IConfigurationRoot config, ILogger<MailService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public void SendMailDummy(string to, string from, string subject, string body)
        {
            Debug.WriteLine($"Sending Mail: To:{to} From:{from} Subject:{subject}");
        }

        public void SendMail(string to, string from, string subject, string body)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Ranaitfleur", _config["MailSettings:Ranaitfleur"]));
            emailMessage.To.Add(new MailboxAddress("Ranaitfleur", to));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = body };

            using (var client = new SmtpClient())
            {
                client.Connect(_config["MailSettings:Host"], 25, false);
                client.Authenticate(_config["MailSettings:Ranaitfleur"], _config["MailSettings:Password"]);
                client.Send(emailMessage);
                client.Disconnect(true);

                //var credentials = new NetworkCredential
                //{
                //    UserName = "postmaster@ranaitfleur.com", // replace with valid value
                //    Password = "#Gunners#69#", // replace with valid value
                //};

                //// for accepting every ssl certificate, DO NOT USE ON PROD, used for testing with gmail
                //client.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;

                //// check your smtp server setting and amend accordingly:
                //await client.ConnectAsync("mail5009.smarterasp.net", 25, false).ConfigureAwait(false);
                //await client.AuthenticateAsync(credentials);
                //await client.SendAsync(emailMessage).ConfigureAwait(false);
                //await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}
