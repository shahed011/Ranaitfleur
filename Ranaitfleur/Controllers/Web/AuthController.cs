using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ranaitfleur.Helper;
using Ranaitfleur.Model;
using Ranaitfleur.Services;
using Ranaitfleur.ViewModels;

namespace Ranaitfleur.Controllers.Web
{
    //[RequireHttps]
    public class AuthController : Controller
    {
        private readonly SignInManager<RanaitfleurUser> _signInManager;
        private readonly UserManager<RanaitfleurUser> _userManager;
        private readonly IRanaitfleurRepository _repository;
        private readonly IMailService _emailService;
        private readonly IHostingEnvironment _environment;

        public AuthController(SignInManager<RanaitfleurUser> signInManager, UserManager<RanaitfleurUser> userManager,
            IRanaitfleurRepository repository, IMailService emailService, IHostingEnvironment environment)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _repository = repository;
            _emailService = emailService;
            _environment = environment;
        }

        public IActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return string.IsNullOrEmpty(returnUrl)
                        ? RedirectToAction("MyAccount", "App")
                        : (IActionResult)Redirect(returnUrl);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(vm.Email);
                if (user != null)
                {
                    var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, vm.Password, true, false);

                    if (signInResult.Succeeded)
                    {
                        return string.IsNullOrEmpty(returnUrl)
                            ? RedirectToAction("MyAccount", "App")
                            : (IActionResult) Redirect(returnUrl);
                    }
                }

                ModelState.AddModelError("", "Email or password incorrect");
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Index", "App");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.FindByEmailAsync(model.Email) == null)
                {
                    var user = new RanaitfleurUser {UserName = model.Username, Email = model.Email};
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var signInResult = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, false);

                        if (signInResult.Succeeded)
                        {
                            return string.IsNullOrEmpty(returnUrl)
                                ? RedirectToAction("MyAccount", "App")
                                : (ActionResult) Redirect(returnUrl);
                        }
                    }
                    else
                    {
                        ViewBag.UserMessage = result.Errors.Select(e => e.Description).ToList()
                            .Aggregate(new StringBuilder(),
                                (sb, a) => sb.AppendLine(a),
                                sb => sb.ToString());
                    }
                }
                else
                {
                    ViewBag.UserMessage = "This email address is already registered";
                }


                //if (result.Succeeded)
                //{
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action(
                    //   "ConfirmEmail", "Account",
                    //   new { userId = user.Id, code = code },
                    //   protocol: Request.Url.Scheme);

                    //await _userManager.SendEmailAsync(user.Id,
                    //   "Confirm your account",
                    //   "Please confirm your account by clicking this link: <a href=\""
                    //                                   + callbackUrl + "\">link</a>");
                    //// ViewBag.Link = callbackUrl;   // Used only for initial demo.
                    //return View("DisplayEmail");
                //}
                //AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Subscribe(Subscribers newSubscriber)
        {
            if (_repository.GetAllSubscribers().Select(s => s.Email).Contains(newSubscriber.Email))
            {
                return View((object) "You are already subscribed");
            }

            _repository.AddSubscriber(newSubscriber);
            await _repository.SaveChangesAsync();

            await _emailService.SendMail(newSubscriber.Email, "", "Hello from Ranaitfleur", EmailHelper.GetSubscribeEmailBody(_environment.WebRootPath));

            return View((object)"Thank you for subscribing to Ranaitfleur");
        }

        [AllowAnonymous]
        public IActionResult Unsubscribe()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Unsubscribe(string email)
        {
            var result = _repository.RemoveSubscriber(email);

            var resultString = "Email could not be found to unsubscribe";
            if (result)
            {
                resultString = "You have been unsubscribed from Ranaitfleur";
                await _repository.SaveChangesAsync();
            }

            return View((object)resultString);
        }
    }
}
