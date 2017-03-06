using Microsoft.AspNetCore.Mvc;
using Ranaitfleur.Model;
using System.Linq;
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
            var returnUrlLower = returnUrl.ToLower();
            if (!returnUrlLower.Contains("shop") && !returnUrlLower.Contains("product"))
                returnUrl = "\\";

            return View(new CartIndexViewModel
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public RedirectToActionResult AddToCart(int size, int productId, string returnUrl)
        {
            var item = _repository.GetAllDresses().FirstOrDefault(p => p.Id == productId);
            //var item = Mapper.Map<Item>(testItem);

            if (item != null)
            {
                _cart.AddItem(item, 1, size);
            }
            return RedirectToAction("CartIndex", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, int size, string returnUrl)
        {
            var item = _repository.GetAllDresses().FirstOrDefault(p => p.Id == productId);

            if (item != null)
            {
                _cart.RemoveLine(item, size);
            }
            return RedirectToAction("CartIndex", new { returnUrl });
        }
    }
}
