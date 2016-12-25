using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ranaitfleur.Model;
using Ranaitfleur.Services;
using Ranaitfleur.ViewModels;
using Ranaitfleur.Infrastructure.SagePayApi;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Ranaitfleur.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IConfigurationRoot _config;
        private readonly IRanaitfleurRepository _repository;
        private readonly ILogger<AppController> _logger;

        public AppController(IMailService mailService, IConfigurationRoot config, IRanaitfleurRepository repository,
            ILogger<AppController> logger)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
            _logger = logger;
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
            try
            {
                return View(_repository.GetAllDresses().FirstOrDefault(d => d.Id == id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get dress products: {ex.Message}");
                return View();
                //return Redirect("/error"); // Need to make one
            }
        }

        [Authorize]
        public IActionResult MyAccount()
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

        public IActionResult OurEthics()
        {
            return View();
        }

        public IActionResult TermsConditions()
        {
            return View();
        }

        public async Task<IActionResult> Payments()
        {
            var sagePay = new SagePayClient();
            ViewBag.MerchantSessionKey = (await sagePay.CreateMerchantSessionKey())?.MerchantSessionKey;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Payments(string cardIdentifier, string sessionId)
        {
            var sagePay = new SagePayClient();
            var resp = await sagePay.CreateTransaction(cardIdentifier, sessionId);
            return View();
        }
    }
}
