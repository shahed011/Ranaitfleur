namespace Ranaitfleur.Infrastructure.SagePayApi.Models
{
    public class Transaction
    {
        public string TransactionId { get; set; }
        public string TransactionType { get; set; }
        public string Status { get; set; }
        public string StatusCode { get; set; }
        public string StatusDetail { get; set; }
        public string RetrievalReference { get; set; }
        public string BankResponseCode { get; set; }
        public string BankAuthorisationCode { get; set; }
        public Amount Amount { get; set; }
        public string Currency { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Secure3D Secure3D { get; set; }
    }
}
