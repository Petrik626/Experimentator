using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Experimentator.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Experimentator.Controllers
{
    [Authorize]
    public class AccountController:Controller
    {
        private UserManager<ExperimentatorUser> userManager;
        private SignInManager<ExperimentatorUser> signInManager;

        public AccountController(UserManager<ExperimentatorUser> manager, SignInManager<ExperimentatorUser> sign)
        {
            userManager = manager;
            signInManager = sign;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                ExperimentatorUser user = await userManager.FindByEmailAsync(details.Email);

                if(user!=null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, details.Password, false, false);

                    if (result.Succeeded)
                    {
                        return Redirect("Index");
                    }
                }

                ModelState.AddModelError(nameof(LoginModel.Email), "Invalid user or password");

            }
            return View(details);
        }
    }
}
