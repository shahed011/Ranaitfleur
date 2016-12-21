namespace Ranaitfleur.Infrastructure.SagePayApi.Models
{
    public class CreateTransactionRefundRequest
    {
        public string TransactionType { get; set; }
        public string ReferenceTransactionId { get; set; }
        public string VendorTxCode { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
    }
}
