 using Core.Utilities;
using DataLayer.ViewModels.Meeting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceLayer.Repositories.Meeting;
using ServiceLayer.Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meeting_Project.Controllers
{
    public class MeetingController : BaseController
    {
        #region DI

        private readonly MeetingRepository _meetingRepository;
        private readonly UserRepository _userRepository;
        public MeetingController(MeetingRepository meetingRepository, UserRepository userRepository)
        {
            _meetingRepository = meetingRepository;
            _userRepository = userRepository;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            var model = await _meetingRepository.GetMyMeeting(this.UserId);

            return View(model);
        }

        public async Task<IActionResult> NewMeeting()
        {
            ViewBag.Users = await _userRepository.GetAllUsers(this.UserId);

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> NewMeeting(NewMeetingViewModel model, List<MeetingTimesViewModel> vm, List<int> users)
        {
            if (!vm.Any() || !users.Any())
            {
                TempData.AddResult(SweetAlertExtenstion.Error("لطفا زمان جلسه و یا اعضای جلسه را به درستی وارد نمایید"));
                return RedirectToAction(nameof(NewMeeting));
            }

            model.UserId = UserId.Value;
            await _meetingRepository.NewMeeting(model, vm, users);
            TempData.AddResult(SweetAlertExtenstion.Ok());

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _meetingRepository.GetByConditionAsync(a => a.Id == id);

            if(model == null)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("اطلاعاتی با این شناسه یافت نشد"));

                return RedirectToAction(nameof(Index));
            }

            if(model.IsVote)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("این جلسه به دلیل ثبت نهایی قابل ویرایش نمی باشد"));

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MeetingEditViewModel vm)
        {
            var model = await _meetingRepository.UpdateMeeting(vm);

            TempData.AddResult(model);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MeetingTimes(int id)
        {
            var model = await _meetingRepository.MeetingTimesRepository.GetListAsync(a => a.MeetingId == id);

            return PartialView(model);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var model = await _meetingRepository.MeetingDetail(id);

            ViewBag.MeetingId = id;

            return View(model);
        }

        public async Task<IActionResult> UserSelected(MeetingUserSelectedViewModel vm,List<int> times)
        {
            if (!times.Any() && vm.Status == DataLayer.SSOT.MeetingUserStatus.Accept) 
            {
                TempData.AddResult(SweetAlertExtenstion.Error("لطفا تایم جلسه را انتخاب کنید"));
                return RedirectToAction("Detail", "Home", new { id = vm.MeetingId });
            }

            vm.UserId = this.UserId.Value;
            vm.TimeIds = JsonConvert.SerializeObject(times);
            
            var model = await _meetingRepository.MeetingUsersRepository.UserSelectedTime(vm);

            TempData.AddResult(model);

            return RedirectToAction("Detail", "Home", new { id = vm.MeetingId });
        }


        public async Task<IActionResult> MeetingManagerSelectTime(MeetingManagerSelectedViewModel vm, List<int> times)
        {
            if (!times.Any() )
            {
                TempData.AddResult(SweetAlertExtenstion.Error("لطفا تایم جلسه را انتخاب کنید"));
                return RedirectToAction(nameof(Detail), new { id = vm.Id });
            }

            vm.TimeIds = JsonConvert.SerializeObject(times);

            var model = await _meetingRepository.ManagerSelectTime(vm);

            TempData.AddResult(SweetAlertExtenstion.Ok());

            return RedirectToAction(nameof(Detail), new { id = vm.Id });
        }


        public async Task<IActionResult> DeleteMeeting(int id)
        {
            var model = await _meetingRepository.DeleteAll(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
