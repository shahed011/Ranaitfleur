using System.Threading.Tasks;
using MimeKit;

namespace Ranaitfleur.Services
{
    public interface IMailService
    {
        Task SendMail(string to, string form, string subject, BodyBuilder bodyBuilder);
    }
}
