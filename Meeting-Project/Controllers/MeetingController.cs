using Core.Utilities;
using DataLayer.ViewModels.Meeting;
using Microsoft.AspNetCore.Mvc;
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

            TempData.AddResult(await _meetingRepository.NewMeeting(model, vm, users));

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

        public async Task<IActionResult> UserSelected(MeetingUserSelectedViewModel vm)
        {
            vm.UserId = this.UserId.Value;

            var model = await _meetingRepository.MeetingUsersRepository.UserSelectedTime(vm);

            TempData.AddResult(model);

            return RedirectToAction("Detail", "Home", new { id = vm.MeetingId });
        }


        public async Task<IActionResult> MeetingManagerSelectTime(MeetingManagerSelectedViewModel vm)
        {

            return View();
        }

    }
}
