﻿using IT_Chronicles.Models.ViewModels;
using IT_Chronicles.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IT_Chronicles.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminUsersController : Controller
	{
		private readonly IUsersRepository usersRepository;
        private readonly UserManager<IdentityUser> userManager;

        public AdminUsersController(IUsersRepository usersRepository, UserManager<IdentityUser> userManager)
        {
			this.usersRepository = usersRepository;
            this.userManager = userManager;
        }

		[HttpGet]
        public async  Task<IActionResult> List()
		{
			var users = await usersRepository.GetAllAsync();

			var usersViewModel = new UserViewModel();
			usersViewModel.Users = new List<User>();

			foreach (var user in users)
			{
				usersViewModel.Users.Add(new Models.ViewModels.User
				{

					Id = Guid.Parse(user.Id),
					UserName = user.UserName,
					EmailAddress = user.Email

				});
			}
			return View(usersViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> List(UserViewModel request)
		{
			var identityUser = new IdentityUser
			{
				UserName = request.UserName,
				Email = request.EmailAddress
			};

			var identityResult = await userManager.CreateAsync(identityUser, request.Password);

			if(identityResult is not null)
			{
				if (identityResult.Succeeded)
				{
					var roles = new List<string> { "User" };

					if (request.AdminRoleCheckbox)
					{
						roles.Add("Admin");
					}

					identityResult = await userManager.AddToRolesAsync(identityUser, roles);

					if(identityResult is not null && identityResult.Succeeded)
					{
						return RedirectToAction("List", "AdminUsers");
					}
				}
			}

			return View();
			
		}

		[HttpPost]	
		public async Task<IActionResult> Delete(Guid id)
		{

			var user = await userManager.FindByIdAsync(id.ToString());

			if(user is not null)
			{
				var identityResult = await userManager.DeleteAsync(user);

				if(identityResult  is not null && identityResult.Succeeded)
				{
					return RedirectToAction("List","AdminUsers");
				}
			}

			return View();

		}
	}
}
