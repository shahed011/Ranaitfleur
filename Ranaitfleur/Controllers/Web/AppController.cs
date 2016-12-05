using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ranaitfleur.Model;
using Ranaitfleur.Services;
using Ranaitfleur.ViewModels;

namespace Ranaitfleur.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IConfigurationRoot _config;
        private readonly IRanaitfleurRepository _repository;

        public AppController(IMailService mailService, IConfigurationRoot config, IRanaitfleurRepository repository)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Shop()
        {
            return View(_repository.GetAllDresses());
        }

        public IActionResult ShopWomensCollection()
        {
            return View(_repository.GetAllDresses().Where(d => d.ItemType == 1));
        }

        public IActionResult ShopRfSlipDress()
        {
            return View();
        }

        public IActionResult ShopMensCollection()
        {
            return View();
        }

        public IActionResult Product(int id)
        {
            return View(_repository.GetAllDresses().FirstOrDefault(d => d.ItemId == id));
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

        public IActionResult OurEthics()
        {
            return View();
        }

        public IActionResult TermsConditions()
        {
            return View();
        }
    }
}
