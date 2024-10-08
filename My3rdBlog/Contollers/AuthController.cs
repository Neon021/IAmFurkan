﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Services.Email;
using MyBlog.ViewModels;
using System;
using System.Threading.Tasks;

namespace MyBlog.Contollers
{
    public class AuthController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private IEmailService _emailService;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            var result = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);
            if (!result.Succeeded)
            {
                return View(vm);

            }
            var user = await _userManager.FindByNameAsync(vm.UserName);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (isAdmin)
            {
                return RedirectToAction("Index", "Panel");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var user = new IdentityUser
            {
                UserName = vm.UserName,
                Email = vm.Email
            };
            var result = await _userManager.CreateAsync(user, vm.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                _emailService.SendEmail(user.Email, "Welcome", "Thank you for registering");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



    }
}
