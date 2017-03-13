using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ranaitfleur.Model
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }
        [BindNever]
        public ICollection<OrderItemsLine> Lines { get; set; }
        [BindNever]
        public string UserName { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [DisplayName("First Name")]
        [StringLength(160)]
        public string ShipFirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [DisplayName("Last Name")]
        [StringLength(160)]
        public string ShipLastName { get; set; }

        [Required(ErrorMessage = "Please enter the first address line")]
        public string ShipLine1 { get; set; }
        public string ShipLine2 { get; set; }
        public string ShipLine3 { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "Please enter a city name")]
        public string ShipCity { get; set; }

        [DisplayName("Postcode")]
        public string ShipPostcode { get; set; }

        [DisplayName("Country")]
        [Required(ErrorMessage = "Please enter a country name")]
        public string ShipCountry { get; set; }

        [DisplayName("Phone")]
        [StringLength(24)]
        public string ShipPhone { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [DisplayName("Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string ShipEmail { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [DisplayName("First Name")]
        [StringLength(160)]
        public string BillFirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [DisplayName("Last Name")]
        [StringLength(160)]
        public string BillLastName { get; set; }

        [Required(ErrorMessage = "Please enter the first address line")]
        public string BillLine1 { get; set; }
        public string BillLine2 { get; set; }
        public string BillLine3 { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "Please enter a city name")]
        public string BillCity { get; set; }

        [DisplayName("Postcode")]
        public string BillPostcode { get; set; }

        [DisplayName("Country")]
        [Required(ErrorMessage = "Please enter a country name")]
        public string BillCountry { get; set; }

        [DisplayName("Phone")]
        [StringLength(24)]
        public string BillPhone { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [DisplayName("Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string BillEmail { get; set; }

        public bool GiftWrap { get; set; }
        public bool BillSameAsShip { get; set; }

        [BindNever]
        public DateTime DateTime { get; set; }
        [BindNever]
        public OrderStatus Status { get; set; }

        [ScaffoldColumn(false)]
        public string PaymentTransactionId { get; set; }
    }

    public class OrderItemsLine
    {
        public int OrderItemsLineId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public int Size { get; set; }
    }

    public enum OrderStatus
    {
        Processing,
        Shipped,
        Complete,
        Declined
    }
}
