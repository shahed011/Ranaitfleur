using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ranaitfleur.Infrastructure.SagePayApi;
using Ranaitfleur.Model;
using Ranaitfleur.Services;

namespace Ranaitfleur.Controllers.Web
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _repository;
        private readonly IMailService _emailService;
        private readonly Cart _cart;

        public OrderController(IOrderRepository repoService, IMailService emailService, Cart cartService)
        {
            _repository = repoService;
            _emailService = emailService;
            _cart = cartService;
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
            var sagePay = new SagePayClient();
            ViewBag.MerchantSessionKey = (await sagePay.CreateMerchantSessionKey())?.MerchantSessionKey;
            ViewBag.OrderId = orderId;

            return View(_cart.Lines);
        }

        [HttpPost]
        public async Task<IActionResult> Payments(string cardIdentifier, string sessionId, int amount, int orderId)
        {
            var order = await _repository.GetOrder(orderId);

            var sagePay = new SagePayClient();
            var resp = await sagePay.CreateTransaction(cardIdentifier, sessionId, amount, "GBP", "This is new order", order);

            order.PaymentTransactionId = resp.Value.TransactionId;
            order.Status = resp.IsSuccess ? OrderStatus.Processing : OrderStatus.Declined;
            await _repository.SaveOrder(order);

            //_emailService.SendMail("email@email.com", "email@email.com", "Order", "Order paid");

            return RedirectToAction(nameof(Completed), new {status = resp.Value?.Status ?? resp.StatusCode.ToString()});
        }

        public ViewResult Completed(string status)
        {
            _cart.Clear();
            return View(nameof(Completed), status);
        }
    }
}
