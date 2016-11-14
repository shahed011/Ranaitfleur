using Microsoft.AspNetCore.Mvc;
using Ranaitfleur.Services;
using Ranaitfleur.ViewModels;

namespace Ranaitfleur.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;

        public AppController(IMailService mailService)
        {
            _mailService = mailService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel viewModel)
        {
            _mailService.SendMail("shahed011@hotmail.com", viewModel.Email, "From Ranaitfleur", viewModel.Message);

            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
