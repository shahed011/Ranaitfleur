using System.Linq;
using Ranaitfleur.Infrastructure.SagePayApi.Models;
using Ranaitfleur.Model;
using Ranaitfleur.Helper;

namespace Ranaitfleur.Infrastructure.SagePayApi
{
    public class SagePayHelper
    {
        public static SagePayCryptModel GetCryptModel(Cart cart, Order order, string vendorEmail, string successUrl, string failureUrl)
        {
            var crypt = new SagePayCryptModel
            {
                Amount = cart.ComputeTotalValue(),
                Currency = "GBP",
                Description = "Products from Ranaitfleur Ltd",
                VendorTxCode = order.OrderId.ToString(),

                Apply3DSecure = Apply3DSecureFlag.YesIfRulesAllow,
                ApplyAVSCV2 = ApplyAVSCV2Flag.CheckIfAvsOrCv2Enabled,
                AllowGiftAid = AllowGiftAidFlag.No,
                
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
                FailureURL = failureUrl,

                Basket = PopulateBasketFromCart(cart),

                SendEMail = SendMailFlag.SendToCustomerAndVendor,
                CustomerEMail = order.BillEmail,
                VendorEMail = vendorEmail,
                EmailMessage = "We will ship your order within three working days from the day of purchase. You will receive email notification once we have shipped your order."
            };

            return crypt;
        }

        private static string PopulateBasketFromCart(Cart cart)
        {
            var basket = cart.Lines.Count().ToString();
            foreach (var line in cart.Lines)
            {
                basket += ":" + line.Item.Name + " (size " + line.Size + ")" + ":";
                basket += line.Quantity + ":";
                basket += line.Item.Price + ":::";
                basket += line.Item.Price;
            }

            return basket;
        }
    }
}
