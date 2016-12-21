using System;

namespace Ranaitfleur.Infrastructure.SagePayApi.Models
{
    public class MerchantSession
    {
        public string MerchantSessionKey { get; set; }
        public DateTime Expiry { get; set; }
    }
}
