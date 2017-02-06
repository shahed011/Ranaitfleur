using Ranaitfleur.Infrastructure.SagePayApi.HttpSender;
using Ranaitfleur.Infrastructure.SagePayApi.Models;
using Ranaitfleur.Model;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Ranaitfleur.Helper;

namespace Ranaitfleur.Infrastructure.SagePayApi
{
    public class SagePayClient
    {
        private readonly IHttpRequestSender _sender;

        // sandbox
        // user details may be moved to config file
        private string vendorName = "sandbox";
        private string integrationKey = "hJYxsw7HLbj40cB8udES8CDRFLhuJ8G54O6rDpUXvE6hYDrria";
        private string integrationPassword = "o2iHSrFybYMZpmWOQMuhsXP52V4fBtpuSDshrKDSWsBY1OiN6hwd9Kb12z4j5Us5u";


        // maybe we can move all these urls to config files as well
        private string CreateMerchantSessionKeyUrl = "https://pi-test.sagepay.com/api/v1/merchant-session-keys";
        private string GetMerchantSessionKeyUrl = "https://pi-test.sagepay.com/api/v1/merchant-session-keys/{merchantSessionKey}";

        // this action is not required in code because CardIdentifier is obtained in javascript
        private string CreateCardIdentifierUrl = "https://pi-test.sagepay.com/api/v1/card-identifiers";
        private string LinkCardIdentifierWithSecurityCodeUrl = "https://pi-test.sagepay.com/api/v1/card-identifiers/{cardIdentifier}/security-code";

        private string CreateTransactionUrl = "https://pi-test.sagepay.com/api/v1/transactions";
        private string GetTransactionUrl = "https://pi-test.sagepay.com/api/v1/transactions/{transactionId}";

        // This is not used so far
        private string CreateInstructionUrl = "https://pi-test.sagepay.com/api/v1/transactions/{transactionId}/instructions";
        private string GetInstructionUrl = "https://pi-test.sagepay.com/api/v1/transactions/{transactionId}/instructions";

        public SagePayClient()
        {
            var clientPassword = integrationKey + ":" + integrationPassword;
            var byteArray = Encoding.UTF8.GetBytes(clientPassword);
            var base64ByteArray = Convert.ToBase64String(byteArray);
            var auth = new AuthenticationHeaderValue("Basic", base64ByteArray);

            _sender = new HttpRequestSender(auth, "application/json");
        }

        public async Task<MerchantSession> CreateMerchantSessionKey()
        {
            var url = CreateMerchantSessionKeyUrl;
            var data = new CreateMerchantSessionKeyRequest {VendorName = vendorName};

            var response = await _sender.SendPostRequest<MerchantSession, CreateMerchantSessionKeyRequest>(url, data);
            return response?.Value;
        }

        public async Task<MerchantSession> GetMerchantSessionKey(string merchantSessionKey)
        {
            var url = GetMerchantSessionKeyUrl.Replace("{merchantSessionKey}", merchantSessionKey);
            var response = await _sender.SendGetRequest<MerchantSession>(url);
            return response?.Value;
        }

        public async Task<Response<Transaction>> CreateTransaction(string cardId, string sessionKey, int amount, string currency, string desc, Order order)
        {
            var url = CreateTransactionUrl;
            var data = new CreateTransactionPaymentRequest
            {
                TransactionType = "Payment",
                Amount = amount,
                Currency = currency, //The currency of the amount in 3 letter ISO 4217 format.
                Description = desc,
                PaymentMethod = new PaymentMethodRequest
                {
                    Card = new CardRequest
                    {
                        CardIdentifier = cardId,
                        MerchantSessionKey = sessionKey
                    },
                },
                VendorTxCode = order.OrderId.ToString(),
                CustomerFirstName = order.FirstName,
                CustomerLastName = order.LastName,
                BillingAddress = new BillingAddress
                {
                    Address1 = order.Line1,
                    Address2 = order.Line2 + " " + order.Line3,
                    City = order.City,
                    Country = CountryAndCodeHelper.GetCountryCodeFromName(order.Country), //Two letter country code defined in ISO 3166-1
                    PostalCode = order.Postcode
                    // State - Two letter state code defined in ISO 3166-2. Required when shippingCountry is US.
                }
            };

            var response = await _sender.SendPostRequest<Transaction, CreateTransactionPaymentRequest>(url, data);
            return response;
        }

        public async Task<Transaction> GetTransaction(string transactionId)
        {
            var url = GetTransactionUrl.Replace("{transactionId}", transactionId);
            var response = await _sender.SendGetRequest<Transaction>(url);
            return response?.Value;
        }
    }
}
