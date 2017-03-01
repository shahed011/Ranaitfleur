using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit.Net.Smtp;
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

        public async Task SendMail(string to, string from, string subject, BodyBuilder bodyBuilder)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Ranaitfleur", _config["MailSettings:Ranaitfleur"]));
            emailMessage.To.Add(new MailboxAddress("Ranaitfleur", to));
            emailMessage.Subject = subject;
            emailMessage.Body = bodyBuilder.ToMessageBody();
            //emailMessage.Body = new TextPart(TextFormat.Html) { Text = body };

            using (var client = new SmtpClient())
            {
                //client.Connect(_config["MailSettings:Host"], 25, false);
                //client.Authenticate(_config["MailSettings:Ranaitfleur"], _config["MailSettings:Password"]);
                //client.Send(emailMessage);
                //client.Disconnect(true);

                await client.ConnectAsync(_config["MailSettings:Host"], 25, false).ConfigureAwait(false);
                await client.AuthenticateAsync(_config["MailSettings:Ranaitfleur"], _config["MailSettings:Password"]);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);

                //// for accepting every ssl certificate, DO NOT USE ON PROD, used for testing with gmail
                //client.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;
            }
        }
    }
}
