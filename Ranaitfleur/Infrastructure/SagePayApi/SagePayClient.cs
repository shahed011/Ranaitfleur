using Ranaitfleur.Infrastructure.SagePayApi.HttpSender;
using Ranaitfleur.Infrastructure.SagePayApi.Models;
using Ranaitfleur.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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
                    Country = await GetCountryCode(order.Country), //Two letter country code defined in ISO 3166-1
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

        static async Task<string> GetCountryCode(string countryEnglishName)
        {
            using (var client = new HttpClient())
            {
                var baseUri = "http://country.io/names.json";
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.GetAsync(baseUri);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                }
            }
            return "";
        }

        private Dictionary<string, string> _countryToCodeMap = new Dictionary<string, string>
        {
            {"BD", "Bangladesh"},
            { "BE", "Belgium"}, "BF": "Burkina Faso", "BG": "Bulgaria", "BA": "Bosnia and Herzegovina", "BB": "Barbados", "WF": "Wallis and Futuna", "BL": "Saint Barthelemy", "BM": "Bermuda", "BN": "Brunei", "BO": "Bolivia", "BH": "Bahrain", "BI": "Burundi", "BJ": "Benin", "BT": "Bhutan", "JM": "Jamaica", "BV": "Bouvet Island", "BW": "Botswana", "WS": "Samoa", "BQ": "Bonaire, Saint Eustatius and Saba ", "BR": "Brazil", "BS": "Bahamas", "JE": "Jersey", "BY": "Belarus", "BZ": "Belize", "RU": "Russia", "RW": "Rwanda", "RS": "Serbia", "TL": "East Timor", "RE": "Reunion", "TM": "Turkmenistan", "TJ": "Tajikistan", "RO": "Romania", "TK": "Tokelau", "GW": "Guinea-Bissau", "GU": "Guam", "GT": "Guatemala", "GS": "South Georgia and the South Sandwich Islands", "GR": "Greece", "GQ": "Equatorial Guinea", "GP": "Guadeloupe", "JP": "Japan", "GY": "Guyana", "GG": "Guernsey", "GF": "French Guiana", "GE": "Georgia", "GD": "Grenada", "GB": "United Kingdom", "GA": "Gabon", "SV": "El Salvador", "GN": "Guinea", "GM": "Gambia", "GL": "Greenland", "GI": "Gibraltar", "GH": "Ghana", "OM": "Oman", "TN": "Tunisia", "JO": "Jordan", "HR": "Croatia", "HT": "Haiti", "HU": "Hungary", "HK": "Hong Kong", "HN": "Honduras", "HM": "Heard Island and McDonald Islands", "VE": "Venezuela", "PR": "Puerto Rico", "PS": "Palestinian Territory", "PW": "Palau", "PT": "Portugal", "SJ": "Svalbard and Jan Mayen", "PY": "Paraguay", "IQ": "Iraq", "PA": "Panama", "PF": "French Polynesia", "PG": "Papua New Guinea", "PE": "Peru", "PK": "Pakistan", "PH": "Philippines", "PN": "Pitcairn", "PL": "Poland", "PM": "Saint Pierre and Miquelon", "ZM": "Zambia", "EH": "Western Sahara", "EE": "Estonia", "EG": "Egypt", "ZA": "South Africa", "EC": "Ecuador", "IT": "Italy", "VN": "Vietnam", "SB": "Solomon Islands", "ET": "Ethiopia", "SO": "Somalia", "ZW": "Zimbabwe", "SA": "Saudi Arabia", "ES": "Spain", "ER": "Eritrea", "ME": "Montenegro", "MD": "Moldova", "MG": "Madagascar", "MF": "Saint Martin", "MA": "Morocco", "MC": "Monaco", "UZ": "Uzbekistan", "MM": "Myanmar", "ML": "Mali", "MO": "Macao", "MN": "Mongolia", "MH": "Marshall Islands", "MK": "Macedonia", "MU": "Mauritius", "MT": "Malta", "MW": "Malawi", "MV": "Maldives", "MQ": "Martinique", "MP": "Northern Mariana Islands", "MS": "Montserrat", "MR": "Mauritania", "IM": "Isle of Man", "UG": "Uganda", "TZ": "Tanzania", "MY": "Malaysia", "MX": "Mexico", "IL": "Israel", "FR": "France", "IO": "British Indian Ocean Territory", "SH": "Saint Helena", "FI": "Finland", "FJ": "Fiji", "FK": "Falkland Islands", "FM": "Micronesia", "FO": "Faroe Islands", "NI": "Nicaragua", "NL": "Netherlands", "NO": "Norway", "NA": "Namibia", "VU": "Vanuatu", "NC": "New Caledonia", "NE": "Niger", "NF": "Norfolk Island", "NG": "Nigeria", "NZ": "New Zealand", "NP": "Nepal", "NR": "Nauru", "NU": "Niue", "CK": "Cook Islands", "XK": "Kosovo", "CI": "Ivory Coast", "CH": "Switzerland", "CO": "Colombia", "CN": "China", "CM": "Cameroon", "CL": "Chile", "CC": "Cocos Islands", "CA": "Canada", "CG": "Republic of the Congo", "CF": "Central African Republic", "CD": "Democratic Republic of the Congo", "CZ": "Czech Republic", "CY": "Cyprus", "CX": "Christmas Island", "CR": "Costa Rica", "CW": "Curacao", "CV": "Cape Verde", "CU": "Cuba", "SZ": "Swaziland", "SY": "Syria", "SX": "Sint Maarten", "KG": "Kyrgyzstan", "KE": "Kenya", "SS": "South Sudan", "SR": "Suriname", "KI": "Kiribati", "KH": "Cambodia", "KN": "Saint Kitts and Nevis", "KM": "Comoros", "ST": "Sao Tome and Principe", "SK": "Slovakia", "KR": "South Korea", "SI": "Slovenia", "KP": "North Korea", "KW": "Kuwait", "SN": "Senegal", "SM": "San Marino", "SL": "Sierra Leone", "SC": "Seychelles", "KZ": "Kazakhstan", "KY": "Cayman Islands", "SG": "Singapore", "SE": "Sweden", "SD": "Sudan", "DO": "Dominican Republic", "DM": "Dominica", "DJ": "Djibouti", "DK": "Denmark", "VG": "British Virgin Islands", "DE": "Germany", "YE": "Yemen", "DZ": "Algeria", "US": "United States", "UY": "Uruguay", "YT": "Mayotte", "UM": "United States Minor Outlying Islands", "LB": "Lebanon", "LC": "Saint Lucia", "LA": "Laos", "TV": "Tuvalu", "TW": "Taiwan", "TT": "Trinidad and Tobago", "TR": "Turkey", "LK": "Sri Lanka", "LI": "Liechtenstein", "LV": "Latvia", "TO": "Tonga", "LT": "Lithuania", "LU": "Luxembourg", "LR": "Liberia", "LS": "Lesotho", "TH": "Thailand", "TF": "French Southern Territories", "TG": "Togo", "TD": "Chad", "TC": "Turks and Caicos Islands", "LY": "Libya", "VA": "Vatican", "VC": "Saint Vincent and the Grenadines", "AE": "United Arab Emirates", "AD": "Andorra", "AG": "Antigua and Barbuda", "AF": "Afghanistan", "AI": "Anguilla", "VI": "U.S. Virgin Islands", "IS": "Iceland", "IR": "Iran", "AM": "Armenia", "AL": "Albania", "AO": "Angola", "AQ": "Antarctica", "AS": "American Samoa", "AR": "Argentina", "AU": "Australia", "AT": "Austria", "AW": "Aruba", "IN": "India", "AX": "Aland Islands", "AZ": "Azerbaijan", "IE": "Ireland", "ID": "Indonesia", "UA": "Ukraine", "QA": "Qatar", "MZ": "Mozambique"
        };
    }
}
