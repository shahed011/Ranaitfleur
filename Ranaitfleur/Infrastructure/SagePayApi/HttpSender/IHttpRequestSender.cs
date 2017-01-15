using Ranaitfleur.Infrastructure.SagePayApi.Models;
using System.Threading.Tasks;

namespace Ranaitfleur.Infrastructure.SagePayApi.HttpSender
{
    public interface IHttpRequestSender
    {
        Task<Response<TResponse>> SendGetRequest<TResponse>(string url);
        Task<Response<TResponse>> SendPostRequest<TResponse, TRequest>(string url, TRequest postData);
    }
}