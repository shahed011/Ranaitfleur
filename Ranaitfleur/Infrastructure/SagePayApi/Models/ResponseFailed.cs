using System.Collections.Generic;

namespace Ranaitfleur.Infrastructure.SagePayApi.Models
{
    public class ResponseFailed
    {
        public int StatusCode { get; set; }
        public string StatusDetail { get; set; }
        public string TransactionId { get; set; }
        public string TransactionType { get; set; }
        public string Status { get; set; }

        // used when there is more then one error
        public List<Error> Errors { get; set; }
    }
}
