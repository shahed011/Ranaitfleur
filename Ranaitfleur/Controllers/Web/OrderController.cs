using Microsoft.AspNetCore.Mvc;
using Ranaitfleur.Model;

namespace Ranaitfleur.Controllers.Web
{
    public class OrderController : Controller
    {
        public ViewResult Checkout() => View(new Order());
    }
}
