using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities;
using DataLayer.Entities.User;
using DataLayer.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Meeting_Project.Controllers
{
    public class AccountController : BaseController
    {
        #region DI

        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<Roles> _roleManager;
        public AccountController(SignInManager<Users> signInManager, UserManager<Users> userManager, RoleManager<Roles> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        #endregion


        [AllowAnonymous()]
        public IActionResult Login()
        {

            return View();
        }

        [AllowAnonymous()]
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            if (ModelState.IsValid)
            {
                var model = await _userManager.FindByNameAsync(userName);

                if (model == null)
                {
                    TempData.AddResult(SweetAlertExtenstion.Error("کاربری با این نام کاربری یافت نشد!"));
                    return RedirectToAction("Login");
                }

                if (model.IsActive == false)
                {
                    TempData.AddResult(SweetAlertExtenstion.Error("شما فعال نیستید!"));
                    return RedirectToAction("Login");
                }

                var result = await _signInManager.PasswordSignInAsync(model, password, true, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                TempData.AddResult(SweetAlertExtenstion.Error("کلمه عبور یا نام کاربری نادرست است"));

                return Redirect("Index");
            }

            TempData.AddResult(SweetAlertExtenstion.Error("لطفا اطلاعات را به درستی وارد کنید"));

            return View();
        }


        [AllowAnonymous()]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }



        [AllowAnonymous]
        public IActionResult Register(string ReturnUrl = null)
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string ReturnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = AutoMapper.Mapper.Map<Users>(model);

                var userResult = await _userManager.FindByNameAsync(model.NationalCode);

                if (userResult == null)
                {
                    var resultCreatUser = await _userManager.CreateAsync(user, model.Password);

                    if (resultCreatUser.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        TempData.AddResult(SweetAlertExtenstion.Error("عملیات با خطا مواجه شد لطفا مجددا تلاش نمایید"));
                        return View(model);
                    }
                }
                else
                {
                    TempData.AddResult(SweetAlertExtenstion.Error("چنین کاربری از قبل وجود دارد"));
                    return View(model);
                }
            }

            TempData.AddResult(SweetAlertExtenstion.Error("لطفا اطلاعاعات را به درستی وارد نمایید"));
            return View(model);
        }



    }
}
