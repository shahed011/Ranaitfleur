﻿using Ranaitfleur.Infrastructure.HttpSender;
using Ranaitfleur.Infrastructure.SagePayApi.Models;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Ranaitfleur.Infrastructure.SagePayApi
{
    public class SagePayClient
    {
        private readonly IHttpRequestSender _sender;

        // sandbox
        private string vendorName = "sandbox";
        private string integrationKey = "hJYxsw7HLbj40cB8udES8CDRFLhuJ8G54O6rDpUXvE6hYDrria";
        private string integrationPassword = "o2iHSrFybYMZpmWOQMuhsXP52V4fBtpuSDshrKDSWsBY1OiN6hwd9Kb12z4j5Us5u";

        private string CreateMerchantSessionKeyUrl = "https://pi-test.sagepay.com/api/v1/merchant-session-keys";
        private string GetMerchanSessionKeyUrl = "https://pi-test.sagepay.com/api/v1/merchant-session-keys/{merchantSessionKey}";

        private string CreateCardIdentifierUrl = "https://pi-test.sagepay.com/api/v1/card-identifiers";
        private string LinkCardIdentifierWithSecurityCodeUrl = "https://pi-test.sagepay.com/api/v1/card-identifiers/{cardIdentifier}/security-code";

        private string CreateTransactionUrl = "https://pi-test.sagepay.com/api/v1/transactions";
        private string GetTransactionUrl = "https://pi-test.sagepay.com/api/v1/transactions/{transactionId}";

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
            return response;
        }

        public async Task<Transaction> CreateTransaction(string cardId, string sessionKey, int amount)
        {
            var url = CreateTransactionUrl;
            var data = new CreateTransactionPaymentRequest
            {
                TransactionType = "Payment",
                Amount = amount,
                Currency = "GBP",
                Description = "Dupa description",
                PaymentMethod = new PaymentMethodRequest
                {
                    Card = new CardRequest
                    {
                        CardIdentifier = cardId,
                        MerchantSessionKey = sessionKey
                    },
                },
                VendorTxCode = "123456",
                CustomerFirstName = "Dupa",
                CustomerLastName = "Dupa",
                BillingAddress = new BillingAddress
                {
                    Address1 = "Dupa",
                    City = "London Dupa",
                    Country = "GB",
                    PostalCode = "D06A"
                }
            };

            var response = await _sender.SendPostRequest<Transaction, CreateTransactionPaymentRequest>(url, data);
            return response;
        }
    }
}
