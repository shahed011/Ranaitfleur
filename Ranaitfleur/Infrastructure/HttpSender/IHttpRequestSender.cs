using System.Threading.Tasks;

namespace Ranaitfleur.Infrastructure.HttpSender
{
    public interface IHttpRequestSender
    {
        Task<TResponse> SendPostRequest<TResponse, TRequest>(string url, TRequest postData);
    }
}