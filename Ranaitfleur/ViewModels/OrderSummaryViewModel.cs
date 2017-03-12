using Ranaitfleur.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ranaitfleur.ViewModels
{
    public class OrderSummaryViewModel
    {
        public IEnumerable<CartLine> Orders { get; set; }
        public string VendorName { get; set; }
        public string Crypt { get; set; }

        [DisplayFormat(DataFormatString = "C")]
        public int TotalPrice => Orders.Select(l => l.Item.Price * l.Quantity).Sum();

        public string PaymentUrl { get; set; }
    }
}
