using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Experimentator.Models;

namespace Experimentator.Controllers
{
    public class AdminController:Controller
    {
        private UserManager<ExperimentatorUser> userManager;

        public AdminController(UserManager<ExperimentatorUser> user) => userManager = user;

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateExperimentatorUserModel model)
        {
            if(ModelState.IsValid)
            {
                ExperimentatorUser user = new ExperimentatorUser { UserName = model.Name, Email = model.Email };

                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if(result.Succeeded)
                {
                    return Redirect("Index");
                }
                else
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }
    }
}
