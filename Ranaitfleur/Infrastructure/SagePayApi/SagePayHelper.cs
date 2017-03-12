using Ranaitfleur.Infrastructure.SagePayApi.Models;
using Ranaitfleur.Model;
using Ranaitfleur.Helper;

namespace Ranaitfleur.Infrastructure.SagePayApi
{
    public class SagePayHelper
    {
        public static SagePayCryptModel GetCryptModel(Cart cart, Order order, string successUrl, string failureUrl)
        {
            var crypt = new SagePayCryptModel
            {
                Amount = cart.ComputeTotalValue(),
                Currency = "GBP", // TODO: Move to config
                Description = "This is some order description",
                Basket = "This is some order description",
                VendorTxCode = order.OrderId.ToString(),

                Apply3DSecure = 0,
                ApplyAVSCV2 = 0,
                AllowGiftAid = 0,
                
                BillingFirstnames = order.FirstName,
                BillingSurname = order.LastName,
                BillingAddress1 = order.Line1,
                BillingAddress2 = order.Line2 + " " + order.Line3,
                BillingCity = order.City,
                BillingCountry = CountryAndCodeHelper.GetCountryCodeFromName(order.Country), //Two letter country code defined in ISO 3166-1
                BillingPostCode = order.Postcode,

                DeliveryFirstnames = order.FirstName,
                DeliverySurname = order.LastName,
                DeliveryAddress1 = order.Line1,
                DeliveryAddress2 = order.Line2 + " " + order.Line3,
                DeliveryCity = order.City,
                DeliveryCountry = CountryAndCodeHelper.GetCountryCodeFromName(order.Country), //Two letter country code defined in ISO 3166-1
                DeliveryPostCode = order.Postcode,

                SuccessURL = successUrl,
                FailureURL = failureUrl
            };

            return crypt;
        }
    }
}
