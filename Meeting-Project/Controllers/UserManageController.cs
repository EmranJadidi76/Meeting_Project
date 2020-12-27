using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities;
using DataLayer.Entities.User;
using DataLayer.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Meeting_Project.Controllers
{
    public class UserManageController : Controller
    {
        #region DI

        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;

        public UserManageController(SignInManager<Users> signInManager, UserManager<Users> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        #endregion



        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();

            return Json(users);
        }


        public IActionResult CreateUser() => View();

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = AutoMapper.Mapper.Map<Users>(model);

                var userResult = await _userManager.FindByNameAsync(model.NationalCode);

                if (userResult == null)
                {
                    var resultCreatUser = await _userManager.CreateAsync(user, model.NationalCode);

                    if (resultCreatUser.Succeeded)
                    {
                        return RedirectToAction("Index");
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
            return View();
        }

    }
}
