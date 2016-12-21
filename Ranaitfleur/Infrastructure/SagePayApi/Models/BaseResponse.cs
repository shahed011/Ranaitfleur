namespace Ranaitfleur.Infrastructure.SagePayApi.Models
{
    public class BaseResponse<TResponse>
    {
        public TResponse Response { get; set; }
        public int StatusCode { get; set; }
        public Error[] Errors { get; set; }
    }
}
