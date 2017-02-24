using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ranaitfleur.Model;
using Ranaitfleur.Services;
using Ranaitfleur.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Ranaitfleur.Controllers.Web
{
    //[RequireHttps]
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

        public IActionResult Shop(string id)
        {
            var itemType = int.Parse(id);
            return View(_repository.GetAllDresses().Where(d => d.ItemType == itemType).ToList());
        }

        public IActionResult Product(int id)
        {
            try
            {
                var allDresses = _repository.GetAllDresses().ToList();
                var item = allDresses.FirstOrDefault(d => d.Id == id);
                var restOfTheSameDresses = allDresses.Where(d => d.ItemType == item.ItemType).ToList();

                restOfTheSameDresses.Remove(item);
                restOfTheSameDresses.Insert(0, item);
                var itemsAsViewModel = Mapper.Map<List<ItemViewModel>>(restOfTheSameDresses);
                return View(itemsAsViewModel);
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
                _mailService.SendMail(_config["MailSettings:ContactAddress"], viewModel.Email, "From Ranaitfleur",
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

        public IActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}
