using IT_Chronicles.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IT_Chronicles.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public AccountController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email
            };

           var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

            if (identityResult.Succeeded)
            {
                // assign user the userRole

              var roleIdentityResult =  await userManager.AddToRoleAsync(identityUser, "User");

                if (roleIdentityResult.Succeeded)
                {
                    //success
                    return RedirectToAction("Register");
                }


            }
            //failure
            return View();

        }
    }
}
