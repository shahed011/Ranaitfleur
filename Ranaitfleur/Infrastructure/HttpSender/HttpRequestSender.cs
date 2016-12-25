using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Ranaitfleur.Infrastructure.HttpSender
{
    public class HttpRequestSender : IHttpRequestSender
    {
        private readonly HttpClient _client;

        public HttpRequestSender(AuthenticationHeaderValue authHeader, string contentType)
        {
            _client = new HttpClient();

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Authorization = authHeader;
        }

        /// <summary>
        /// Sends some data to a URL using an HTTP POST.
        /// </summary>
        /// <param name="url">Url to send to</param>
        /// <param name="postData">The data to send</param>
        public async Task<TResponse> SendPostRequest<TResponse, TRequest>(string url, TRequest postData)
        {
            var uri = new Uri(url);

            var response = await _client.PostAsJsonAsync(uri, postData);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<TResponse>();
            }

            //TODO: Logging of some exceptions
            //TODO: Resializing errors

            return default(TResponse);
        }
    }
}
