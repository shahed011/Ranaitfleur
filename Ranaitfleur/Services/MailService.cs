using System.Diagnostics;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Ranaitfleur.Services
{
    public class MailService : IMailService
    {
        public void SendMailDummy(string to, string from, string subject, string body)
        {
            Debug.WriteLine($"Sending Mail: To:{to} From:{from} Subject:{subject}");
        }

        public async void SendMail(string to, string form, string subject, string body)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Client", to));
            emailMessage.To.Add(new MailboxAddress("", to));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = body };

            using (var client = new SmtpClient())
            {
                //TODO: Figure out how to make it work.
                client.LocalDomain = "some.domain.com";
                await client.ConnectAsync("smtp.relay.uri", 25, SecureSocketOptions.None).ConfigureAwait(false);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}
