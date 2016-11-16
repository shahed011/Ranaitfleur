using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ranaitfleur.Services;
using Ranaitfleur.ViewModels;

namespace Ranaitfleur.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IConfigurationRoot _config;

        public AppController(IMailService mailService, IConfigurationRoot config)
        {
            _mailService = mailService;
            _config = config;
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
            if (ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSettings:ToAddress"], viewModel.Email, "From Ranaitfleur",
                    viewModel.Message);

                ModelState.Clear();
                ViewBag.UserMessage = "Message sent";
            }

            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
