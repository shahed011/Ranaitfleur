using Microsoft.AspNetCore.Mvc;
using Ranaitfleur.Model;

namespace Ranaitfleur.Component
{
    public class SubscribeViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new Subscribers());
        }
    }
}
