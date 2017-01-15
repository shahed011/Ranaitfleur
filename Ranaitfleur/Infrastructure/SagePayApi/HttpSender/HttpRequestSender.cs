using Ranaitfleur.Infrastructure.SagePayApi.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Ranaitfleur.Infrastructure.SagePayApi.HttpSender
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
        public async Task<Response<TResponse>> SendPostRequest<TResponse, TRequest>(string url, TRequest postData)
        {
            var uri = new Uri(url);
            var message = await _client.PostAsJsonAsync(uri, postData);
            return await HandleResponse<TResponse>(message);
        }

        /// <summary>
        /// Sends some data to a URL using an HTTP GET.
        /// </summary>
        /// <param name="url">Url to send to</param>
        public async Task<Response<TResponse>> SendGetRequest<TResponse>(string url)
        {
            var uri = new Uri(url);
            var message = await _client.GetAsync(uri);
            return await HandleResponse<TResponse>(message);
        }

        private async Task<Response<TResponse>> HandleResponse<TResponse>(HttpResponseMessage message)
        {
            var response = new Response<TResponse>();

            response.StatusCode = message.StatusCode;
            response.StatusDescription = message.ReasonPhrase;

            if (message.IsSuccessStatusCode)
            {
                response.Value = await message.Content.ReadAsAsync<TResponse>();
                response.IsSuccess = true;
            }
            else
            {
                response.Failure = await message.Content.ReadAsAsync<ResponseFailed>();
                response.IsSuccess = false;
            }

            //TODO: Logging

            return response;
        }
    }
}
