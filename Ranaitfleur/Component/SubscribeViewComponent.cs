using Microsoft.AspNetCore.Mvc;

namespace Ranaitfleur.Component
{
    public class SubscribeViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
