namespace Ranaitfleur.Infrastructure.SagePayApi.Models
{
    public class ShippingDetails
    {
        public string RecipientFirstName { get; set; }
        public string RecipientLastName { get; set; }
        public string ShippingAddress1 { get; set; }
        public string ShippingAddress2 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingPostalCode { get; set; }
        public string ShippingCountry { get; set; }
        public string ShippingState { get; set; }
    }
}
