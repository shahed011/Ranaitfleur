using System.Runtime.Serialization;

namespace Ranaitfleur.Infrastructure.SagePayApi.Models
{
    public class SagePayResponseModel
    {
        public string Status { get; set; }
        public string StatusDetail { get; set; }
        public string VendorTxCode { get; set; }
        public string VPSTxId { get; set; }
        public int TxAuthNo { get; set; }
        public decimal Amount { get; set; }
        public string AVSCV2 { get; set; }
        public string AddressResult { get; set; }
        public string PostCodeResult { get; set; }
        public string CV2Result { get; set; }
        public bool GiftAid { get; set; }
        [DataMember(Name = "3DSecureStatus")]
        public string ThreeDSecureStatus { get; set; }
        public string CAVV { get; set; }
        public string AddressStatus { get; set; }
        public string PayerStatus { get; set; }
        public string CardType { get; set; }
        public int Last4Digits { get; set; }
        public string FraudResponse { get; set; }
        public int Surcharge { get; set; }
        public int ExpiryDate { get; set; }
        public string BankAuthCode { get; set; }
        public int DeclineCode { get; set; }
    }
}
