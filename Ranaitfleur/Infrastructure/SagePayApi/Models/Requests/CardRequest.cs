using System.Runtime.Serialization;

namespace Ranaitfleur.Infrastructure.SagePayApi.Models
{
    [DataContract]
    public class CardRequest
    {
        [DataMember(Name = "merchantSessionKey")]
        public string MerchantSessionKey { get; set; }
        [DataMember(Name = "cardIdentifier")]
        public string CardIdentifier { get; set; }
        [DataMember(Name = "reusable")]
        public bool Reusable { get; set; }
        [DataMember(Name = "save")]
        public bool Save { get; set; }
    }
}
