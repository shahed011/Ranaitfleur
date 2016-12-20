using System;

namespace Ranaitfleur.Infrastructure.SagePayApi.Models
{
    public class CardDetails
    {
        public string CardIdentifier { get; set; }
        public DateTime Expiry { get; set; }
        public string CardType { get; set; }
    }
}
