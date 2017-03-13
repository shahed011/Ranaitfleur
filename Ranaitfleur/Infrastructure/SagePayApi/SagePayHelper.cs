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
                
                BillingFirstnames = order.BillFirstName,
                BillingSurname = order.BillLastName,
                BillingAddress1 = order.BillLine1,
                BillingAddress2 = order.BillLine2 + " " + order.BillLine3,
                BillingCity = order.BillCity,
                BillingCountry = CountryAndCodeHelper.GetCountryCodeFromName(order.BillCountry), //Two letter country code defined in ISO 3166-1
                BillingPostCode = order.BillPostcode,
                BillingPhone = order.BillPhone,
                
                DeliveryFirstnames = order.ShipFirstName,
                DeliverySurname = order.ShipLastName,
                DeliveryAddress1 = order.ShipLine1,
                DeliveryAddress2 = order.ShipLine2 + " " + order.ShipLine3,
                DeliveryCity = order.ShipCity,
                DeliveryCountry = CountryAndCodeHelper.GetCountryCodeFromName(order.ShipCountry), //Two letter country code defined in ISO 3166-1
                DeliveryPostCode = order.ShipPostcode,
                DeliveryPhone = order.ShipPhone,

                SuccessURL = successUrl,
                FailureURL = failureUrl
            };

            return crypt;
        }
    }
}
