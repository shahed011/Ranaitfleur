using System.Collections.Generic;
using System.Net;

namespace Ranaitfleur.Infrastructure.SagePayApi.Models
{
    public class Response<TModel>
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string StatusDescription { get; set; }

        public TModel Value { get; set; }

        public ResponseFailed Failure { get; set; }
    }
}
