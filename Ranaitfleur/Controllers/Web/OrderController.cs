using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ranaitfleur.Infrastructure.SagePayApi;
using Ranaitfleur.Model;

namespace Ranaitfleur.Controllers.Web
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _repository;
        private readonly Cart _cart;

        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            _repository = repoService;
            _cart = cartService;
        }

        //public ViewResult List() => View(_repository.Orders.Where(o => o.Status == OrderStatus.Processing));

        //[HttpPost]
        //public IActionResult MarkShipped(int orderId)
        //{
        //    var order = _repository.Orders.FirstOrDefault(o => o.OrderId == orderId);
        //    if (order != null)
        //    {
        //        order.Status = OrderStatus.Shipped;
        //        _repository.SaveOrder(order);
        //    }
        //    return RedirectToAction(nameof(List));
        //}

        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (!_cart.Lines.Any())
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToList();
                await _repository.SaveOrder(order);

                var amount = _cart.Lines.Select(l => l.Item.Price).Sum();
                return RedirectToAction(nameof(Payments), new {amount});
            }

            return View(order);
        }

        public async Task<IActionResult> Payments(int amount)
        {
            var sagePay = new SagePayClient();
            ViewBag.MerchantSessionKey = (await sagePay.CreateMerchantSessionKey())?.MerchantSessionKey;

            return View(amount);
        }

        [HttpPost]
        public async Task<IActionResult> Payments(string cardIdentifier, string sessionId, int amount)
        {
            var sagePay = new SagePayClient();
            var resp = await sagePay.CreateTransaction(cardIdentifier, sessionId, amount);
            return RedirectToAction(nameof(Completed), new {status = resp.Status});
        }

        public ViewResult Completed(string status)
        {
            _cart.Clear();
            return View(status);
        }
    }
}
