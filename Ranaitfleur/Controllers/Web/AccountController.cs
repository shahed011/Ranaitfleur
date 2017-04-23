using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly List<Item> _allDresses;

        public AccountController(UserManager<IdentityUser> userManager, IOrderRepository orderRepository,
            IRanaitfleurRepository repository)
        {
            _userManager = userManager;
            _orderRepository = orderRepository;
            _allDresses = repository.GetAllDresses().ToList();
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AdminAccount()
        {
            await _orderRepository.RemoveIncompleteOrders();
            var userOrders = await _orderRepository.GetAllOrders();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var userAccountVm = new AccountViewModel
            {
                UserName = user.UserName,
                UserEmail = user.Email
            };

            foreach (var order in userOrders.OrderByDescending(o => o.DateTime))
            {
                userAccountVm.UserOrders.Add(new UserOrder(order, _allDresses));
            }

            ViewBag.UserMessage = Convert.ToString(TempData["Message"]);
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

            foreach (var order in userOrders.OrderByDescending(o => o.DateTime))
            {
                userAccountVm.UserOrders.Add(new UserOrder(order, _allDresses));
            }

            return View(userAccountVm);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId)
        {
            var order = await _orderRepository.GetOrder(orderId);
            if (order != null)
            {
                order.Status = order.Status == OrderStatus.Authorised ? OrderStatus.Shipped : OrderStatus.Completed;
                await _orderRepository.SaveOrder(order);

                TempData["Message"] = "Order " + order.Status.ToString().ToLower();
            }

            return RedirectToAction(nameof(AdminAccount));
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var order = await _orderRepository.GetOrder(orderId);
            if (order != null)
            {
                order.Status = OrderStatus.AdminCancelled;
                await _orderRepository.SaveOrder(order);

                TempData["Message"] = "Order cancelled";
            }

            return RedirectToAction(nameof(AdminAccount));
        }
    }
}
