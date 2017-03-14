using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ranaitfleur.Infrastructure.SagePayApi;
using Ranaitfleur.Model;
using Ranaitfleur.Services;
using Ranaitfleur.ViewModels;
using Microsoft.Extensions.Configuration;
using Ranaitfleur.Helper;

namespace Ranaitfleur.Controllers.Web
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _repository;
        private readonly IMailService _emailService;
        private readonly IConfigurationRoot _configuration;
        private readonly ICryptography _cryptography;
        private readonly Cart _cart;

        public OrderController(IOrderRepository repoService, IMailService emailService,
            IConfigurationRoot configuration, ICryptography cryptography, Cart cartService)
        {
            _repository = repoService;
            _emailService = emailService;
            _cart = cartService;
            _configuration = configuration;
            _cryptography = cryptography;
        }

        public IActionResult CheckoutOptions(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Checkout));
            }

            return View();
        }

        public IActionResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (!_cart.Lines.Any())
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (order.BillSameAsShip)
            {
                var allBillErrors = ModelState.Where(m => m.Value.Errors.Any() && m.Key.Contains("Bill"));
                foreach (var item in allBillErrors)
                {
                    ModelState.Remove(item.Key);
                }
            }

            if (ModelState.IsValid)
            {
                order.Lines = new List<OrderItemsLine>();
                foreach (var line in _cart.Lines.ToList())
                {
                    order.Lines.Add(new OrderItemsLine
                    {
                        Size = line.Size,
                        ItemId = line.Item.Id,
                        Quantity = line.Quantity
                    });
                }

                if (User.Identity.IsAuthenticated)
                {
                    order.UserName = User.Identity.Name;
                }

                if (order.BillSameAsShip)
                {
                    order.BillFirstName = order.ShipFirstName;
                    order.BillLastName = order.ShipLastName;
                    order.BillLine1 = order.ShipLine1;
                    order.BillLine2 = order.ShipLine2;
                    order.BillLine3 = order.ShipLine3;
                    order.BillCity = order.ShipCity;
                    order.BillPostcode = order.ShipPostcode;
                    order.BillCountry = order.ShipCountry;
                    order.BillPhone = order.ShipPhone;
                    order.BillEmail = order.ShipEmail;
                }

                order.DateTime = DateTime.Now;
                await _repository.SaveOrder(order);

                return RedirectToAction(nameof(Payments), new { orderId = order.OrderId });
            }

            order.BillSameAsShip = false;
            return View(order);
        }

        public async Task<IActionResult> Payments(int orderId)
        {
            var order = await _repository.GetOrder(orderId).ConfigureAwait(false);
            var successUrl = Url.AbsoluteAction("PaymentSuccess", "Order");
            var failureUrl = Url.AbsoluteAction("PaymentFailure", "Order");

            var cryptModel = SagePayHelper.GetCryptModel(_cart, order, successUrl, failureUrl);

            var summary = new OrderSummaryViewModel()
            {
                Orders = _cart.Lines,
                Vendor = _configuration["SagePay:VendorName"],
                PaymentUrl = _configuration["SagePay:PaymentFormUrl"],
                Crypt = _cryptography.EncryptModel(cryptModel)
            };

            order.Status = OrderStatus.Processing;
            await _repository.SaveOrder(order).ConfigureAwait(false);

            return View(summary);
        }

        public async Task<ViewResult> PaymentSuccess(string crypt)
        {
            var response = _cryptography.DecryptModel(crypt);

            var orderId = 0;
            if (int.TryParse(response?.VendorTxCode, out orderId))
            {
                var order = await _repository.GetOrder(orderId).ConfigureAwait(false);
                if (order != null) 
                {
                    order.PaymentTransactionId = response?.VPSTxId;
                    order.Status = OrderStatus.Complete;
                    await _repository.SaveOrder(order).ConfigureAwait(false);
                }
            }

            _cart.Clear();
            return View("Completed", response.Status);
        }

        public async Task<ViewResult> PaymentFailure(string crypt)
        {
            var response = _cryptography.DecryptModel(crypt);

            var orderId = 0;
            if (int.TryParse(response?.VendorTxCode, out orderId))
            {
                //TODO: save status and description of failure and any other valid info
                // maybe it is worth to store decrypted crypt as well.
                var order = await _repository.GetOrder(orderId).ConfigureAwait(false);
                if (order != null)
                {
                    order.PaymentTransactionId = response?.VPSTxId;
                    order.Status = OrderStatus.Declined;
                    await _repository.SaveOrder(order).ConfigureAwait(false);
                }
            }

            return View("Completed", response.Status);
        }
    }
}
