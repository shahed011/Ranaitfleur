using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ranaitfleur.Model;

namespace Ranaitfleur.Controllers.Web
{
    public class AccountController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public AccountController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult AdminAccount()
        {
            return View();
        }

        [Authorize(Roles = "NormalUser")]
        public async Task<IActionResult> UserAccount()
        {
            var userOrders = await _orderRepository.GetOrdersByUserName(User.Identity.Name);
            return View(userOrders);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId)
        {
            var order = await _orderRepository.GetOrder(orderId);
            if (order != null)
            {
                order.Status = OrderStatus.Shipped;
                await _orderRepository.SaveOrder(order);
            }

            return RedirectToAction(nameof(AdminAccount));
        }
    }
}
