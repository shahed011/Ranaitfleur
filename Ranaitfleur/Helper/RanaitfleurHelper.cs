using Ranaitfleur.Model;

namespace Ranaitfleur.Helper
{
    public static class RanaitfleurHelper
    {
        public static string GetShippingAddressFromOrder(Order order)
        {
            var name = order.ShipFirstName + " " + order.ShipLastName;
            return name + ", " + order.ShipLine1
                              + (!string.IsNullOrEmpty(order.ShipLine2) ? ", " + order.ShipLine2 : "")
                              + (!string.IsNullOrEmpty(order.ShipLine3) ? ", " + order.ShipLine3 : "")
                              + ", " + order.ShipCity + ", " + order.ShipPostcode + ", " + order.ShipCountry
                              + ", " + order.ShipEmail + ", " + order.ShipPhone;
        }

        public static string GetBillingAddressFromOrder(Order order)
        {
            var name = order.BillFirstName + " " + order.BillLastName;
            return name + ", " + order.BillLine1
                             + (!string.IsNullOrEmpty(order.BillLine2) ? ", " + order.BillLine2 : "")
                             + (!string.IsNullOrEmpty(order.BillLine3) ? ", " + order.BillLine3 : "")
                             + ", " + order.BillCity + ", " + order.BillPostcode + ", " + order.BillCountry
                             + ", " + order.BillEmail + ", " + order.BillPhone;
        }
    }
}