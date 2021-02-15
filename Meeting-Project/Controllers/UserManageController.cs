using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities;
using DataLayer.Entities.User;
using DataLayer.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Repositories.User;

namespace Meeting_Project.Controllers
{
    public class UserManageController : BaseController
    {
        #region DI

        private readonly UserRepository _userRepository;

        public UserManageController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers(this.UserId);

            return View(users);
        }


        public IActionResult CreateUser() => View();

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = AutoMapper.Mapper.Map<Users>(model);

                user.ParentId = this.UserId;
                var userResult = await _userRepository.UserManager.FindByNameAsync(model.NationalCode);

                if (userResult == null)
                {
                    var resultCreatUser = await _userRepository.UserManager.CreateAsync(user, model.NationalCode);

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


        [HttpPost]
        public async Task<IActionResult> CreateUserAjax(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = AutoMapper.Mapper.Map<Users>(model);
                user.ParentId = this.UserId;
                var userResult = await _userRepository.UserManager.FindByNameAsync(model.NationalCode);

                if (userResult == null)
                {
                    var resultCreatUser = await _userRepository.UserManager.CreateAsync(user, model.NationalCode);

                    if (resultCreatUser.Succeeded)
                    {
                        var users = await _userRepository.GetAllUsers(this.UserId);
                        return Json(new Tuple<bool, List<Users>>(true, users.ToList()));
                    }
                    else
                    {
                        return Json(new Tuple<bool, string>(false, "لطفا مجددا تلاش کنید"));
                    }
                }
                else
                {
                    return Json(new Tuple<bool, string>(false, "چنین کاربری از قبل وجود دارد"));
                }
            }

            return Json(new Tuple<bool, string>(false, "لطفا اطلاعاعات را به درستی وارد نمایید"));
        }


    }
}
