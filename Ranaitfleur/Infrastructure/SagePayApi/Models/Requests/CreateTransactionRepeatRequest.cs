namespace Ranaitfleur.Infrastructure.SagePayApi.Models
{
    public class CreateTransactionRepeatRequest
    {
        public string TransactionType { get; set; }
        public string ReferenceTransactionId { get; set; }
        public string VendorTxCode { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public ShippingDetails ShippingDetails { get; set; }
        public bool GiftAid { get; set; }
    }
}
