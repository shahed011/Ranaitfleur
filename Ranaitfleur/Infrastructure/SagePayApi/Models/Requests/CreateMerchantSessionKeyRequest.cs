using System.Runtime.Serialization;

namespace Ranaitfleur.Infrastructure.SagePayApi.Models
{
    [DataContract]
    public class CreateMerchantSessionKeyRequest
    {
        [DataMember(Name = "vendorName")]
        public string VendorName { get; set; }
    }
}
