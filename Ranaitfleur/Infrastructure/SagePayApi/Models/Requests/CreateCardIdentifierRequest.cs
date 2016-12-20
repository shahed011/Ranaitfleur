namespace Ranaitfleur.Infrastructure.SagePayApi.Models
{
    public class CreateCardIdentifierRequest
    {
        public string CardholderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string SecurityCode { get; set; }
    }
}
