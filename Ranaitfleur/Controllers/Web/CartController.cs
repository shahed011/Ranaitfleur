using Microsoft.AspNetCore.Mvc;
using Ranaitfleur.Model;
using System.Linq;
using AutoMapper;
using Ranaitfleur.ViewModels;

namespace Ranaitfleur.Controllers.Web
{
    public class CartController : Controller
    {
        private readonly IRanaitfleurRepository _repository;
        private readonly Cart _cart;

        public CartController(IRanaitfleurRepository repository, Cart cartService)
        {
            _repository = repository;
            _cart = cartService;
        }

        public ViewResult CartIndex(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public RedirectToActionResult AddToCart(ItemViewModel itemVm, string returnUrl)
        {
            //var item = _repository.GetAllDresses().FirstOrDefault(p => p.Id == itemVm.Id);
            var item = Mapper.Map<Item>(itemVm);

            if (item != null)
            {
                _cart.AddItem(item, 1);
            }
            return RedirectToAction("CartIndex", new { returnUrl });
        }

        //public RedirectToActionResult AddToCart(int productId, string returnUrl)
        //{
        //    var item = _repository.GetAllDresses().FirstOrDefault(p => p.Id == productId);

        //    if (item != null)
        //    {
        //        _cart.AddItem(item, 1);
        //    }
        //    return RedirectToAction("CartIndex", new { returnUrl });
        //}

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            var item = _repository.GetAllDresses().FirstOrDefault(p => p.Id == productId);

            if (item != null)
            {
                _cart.RemoveLine(item);
            }
            return RedirectToAction("CartIndex", new { returnUrl });
        }
    }
}
