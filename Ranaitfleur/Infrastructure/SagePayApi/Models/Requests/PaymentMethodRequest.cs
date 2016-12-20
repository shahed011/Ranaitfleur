using System.Runtime.Serialization;

namespace Ranaitfleur.Infrastructure.SagePayApi.Models
{
    [DataContract]
    public class PaymentMethodRequest
    {
        [DataMember(Name = "card")]
        public CardRequest Card { get; set; }
    }
}
