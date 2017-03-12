namespace Ranaitfleur.Infrastructure.SagePayApi.Models
{
    public class SagePayCryptModel
    {
        public string VendorTxCode { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string SuccessURL { get; set; }
        public string FailureURL { get; set; }

        public string CustomerEMail { get; set; }
        public string VendorEMail { get; set; }
        public int SendEMail { get; set; } // TODO: flag
        public string EmailMessage { get; set; }

        public string BillingSurname { get; set; }
        public string BillingFirstnames { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingPostCode { get; set; }
        public string BillingCountry { get; set; }
        public string BillingState { get; set; }
        public string BillingPhone { get; set; }

        public string DeliverySurname { get; set; }
        public string DeliveryFirstnames { get; set; }
        public string DeliveryAddress1 { get; set; }
        public string DeliveryAddress2 { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryPostCode { get; set; }
        public string DeliveryCountry { get; set; }
        public string DeliveryState { get; set; }
        public string DeliveryPhone { get; set; }

        public string Basket { get; set; }
        public string VendorData { get; set; }

        public int AllowGiftAid { get; set; } // TODO: flag
        public int ApplyAVSCV2 { get; set; } // TODO: flag
        public int Apply3DSecure { get; set; } // TODO: flag
    }
}
