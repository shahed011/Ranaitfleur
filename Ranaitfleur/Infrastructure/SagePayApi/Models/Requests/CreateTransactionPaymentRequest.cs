using System.Runtime.Serialization;

namespace Ranaitfleur.Infrastructure.SagePayApi.Models
{
    [DataContract]
    public class CreateTransactionPaymentRequest
    {
        [DataMember(Name = "transactionType")]
        public string TransactionType { get; set; }
        [DataMember(Name = "paymentMethod")]
        public PaymentMethodRequest PaymentMethod { get; set; }
        [DataMember(Name = "vendorTxCode")]
        public string VendorTxCode { get; set; }
        [DataMember(Name = "amount")]
        public int Amount { get; set; }
        [DataMember(Name = "currency")]
        public string Currency { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "customerFirstName")]
        public string CustomerFirstName { get; set; }
        [DataMember(Name = "customerLastName")]
        public string CustomerLastName { get; set; }
        [DataMember(Name = "billingAddress")]
        public BillingAddress BillingAddress { get; set; }
        [DataMember(Name = "entryMethod")]
        public string EntryMethod { get; set; }
        [DataMember(Name = "giftAid")]
        public bool GiftAid { get; set; }
        [DataMember(Name = "apply3DSecure")]
        public string Apply3DSecure { get; set; }
        [DataMember(Name = "applyAvsCvcCheck")]
        public string ApplyAvsCvcCheck { get; set; }
        [DataMember(Name = "customerEmail")]
        public string CustomerEmail { get; set; }
        [DataMember(Name = "customerPhone")]
        public string CustomerPhone { get; set; }
        [DataMember(Name = "shippingDetails")]
        public ShippingDetails ShippingDetails { get; set; }
        [DataMember(Name = "referrerId")]
        public string ReferrerId { get; set; }
    }
}
