using MimeKit;

namespace Ranaitfleur.Services
{
    public interface IMailService
    {
        void SendMail(string to, string form, string subject, BodyBuilder bodyBuilder);
    }
}
