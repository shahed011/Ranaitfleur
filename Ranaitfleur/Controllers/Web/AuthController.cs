using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ranaitfleur.Model;
using Ranaitfleur.ViewModels;

namespace Ranaitfleur.Controllers.Web
{
    //[RequireHttps]
    public class AuthController : Controller
    {
        private readonly SignInManager<RanaitfleurUser> _signInManager;
        private readonly UserManager<RanaitfleurUser> _userManager;
        private readonly IRanaitfleurRepository _repository;

        public AuthController(SignInManager<RanaitfleurUser> signInManager, UserManager<RanaitfleurUser> userManager,
            IRanaitfleurRepository repository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _repository = repository;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MyAccount", "App");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, false);

                if (signInResult.Succeeded)
                {
                    return string.IsNullOrEmpty(returnUrl)
                        ? RedirectToAction("MyAccount", "App")
                        : (IActionResult)Redirect(returnUrl);
                }

                ModelState.AddModelError("", "Username or password incorrect");
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
        public async Task<ActionResult> Register(RegisterViewModel model)
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
                            return RedirectToAction("MyAccount", "App");
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
            return View((object)"Thank you for subscribing to Ranaitfleur");
        }
    }
}
