using System;
using System.Collections.Generic;
using System.Linq;
using Ranaitfleur.Helper;
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
        public int Total { get; set; }
        public string PaymentTransactionId { get; set; }

        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }

        public List<UserOrderItem> OrderItems { get; set; }

        public UserOrder(Order order, List<Item> allDresses)
        {
            OrderId = order.OrderId;
            DateTime = order.DateTime;
            Status = order.Status;
            Total = 0;
            PaymentTransactionId = order.PaymentTransactionId;

            ShippingAddress = RanaitfleurHelper.GetShippingAddressFromOrder(order);
            BillingAddress = RanaitfleurHelper.GetBillingAddressFromOrder(order);

            OrderItems = new List<UserOrderItem>();
            foreach (var line in order.Lines)
            {
                var item = allDresses.FirstOrDefault(d => d.Id == line.ItemId);
                
                OrderItems.Add(new UserOrderItem
                {
                    ItemName = item?.Name ?? "",
                    ItemSize = line.Size,
                    ItemQuantity = line.Quantity,
                    Price = item?.Price ?? 0
                });

                Total += (item?.Price ?? 0)*line.Quantity;
            }
        }

        public class UserOrderItem
        {
            public string ItemName { get; set; }
            public int ItemSize { get; set; }
            public int ItemQuantity { get; set; }
            public int Price { get; set; }
        }
    }
}
