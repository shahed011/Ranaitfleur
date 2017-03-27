using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Ranaitfleur.Model;
using Ranaitfleur.ViewModels;

namespace Ranaitfleur.Controllers.Web
{
    public class AccountController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(UserManager<IdentityUser> userManager, IOrderRepository orderRepository)
        {
            _userManager = userManager;
            _orderRepository = orderRepository;
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AdminAccount()
        {
            var userOrders = await _orderRepository.GetAllOrders();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var userAccountVm = new AccountViewModel
            {
                UserName = user.UserName,
                UserEmail = user.Email
            };

            foreach (var order in userOrders)
            {
                userAccountVm.UserOrders.Add(new UserOrder(order));
            }

            return View(userAccountVm);
        }

        [Authorize(Roles = "NormalUser")]
        public async Task<IActionResult> UserAccount()
        {
            var userOrders = await _orderRepository.GetOrdersByUserName(User.Identity.Name);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var userAccountVm = new AccountViewModel
            {
                UserName = user.UserName,
                UserEmail = user.Email
            };

            foreach (var order in userOrders)
            {
                userAccountVm.UserOrders.Add(new UserOrder(order));
            }

            return View(userAccountVm);
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
