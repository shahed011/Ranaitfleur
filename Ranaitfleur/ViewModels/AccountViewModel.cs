using System;
using System.Collections.Generic;
using Ranaitfleur.Model;

namespace Ranaitfleur.ViewModels
{
    public class AccountViewModel
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public List<UserOrder> UserOrders { get; set; } =  new List<UserOrder>();
    }

    public class UserOrder
    {
        public int OrderId { get; }

        public DateTime DateTime { get; set; }
        public OrderStatus Status { get; set; }
        public string PaymentTransactionId { get; set; }

        public string ShippingName { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingName { get; set; }
        public string BillingAddress { get; set; }

        public UserOrder(Order order)
        {
            OrderId = order.OrderId;
            DateTime = order.DateTime;
            Status = order.Status;
            PaymentTransactionId = order.PaymentTransactionId;

            ShippingName = order.ShipFirstName + " " + order.ShipLastName;
            ShippingAddress = order.ShipLine1 + ", " + order.ShipLine2 + ", " + order.ShipLine3
                              + ", " + order.ShipCity + ", " + order.ShipPostcode + ", " + order.ShipCountry
                              + ", " + order.ShipEmail + ", " + order.ShipPhone;

            BillingName = order.BillFirstName + " " + order.BillLastName;
            BillingAddress = order.BillLine1 + ", " + order.BillLine2 + ", " + order.BillLine3
                              + ", " + order.BillCity + ", " + order.BillPostcode + ", " + order.BillCountry
                              + ", " + order.BillEmail + ", " + order.BillPhone;
        }
    }
}
