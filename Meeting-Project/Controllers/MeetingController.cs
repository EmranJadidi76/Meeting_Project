using Core.Utilities;
using DataLayer.ViewModels.Meeting;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Repositories.Meeting;
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

        public MeetingController(MeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            var model = await _meetingRepository.GetMyMeeting(this.UserId);

            return View(model);
        }

        public IActionResult NewMeeting() => View();

        [HttpPost]
        public async Task<IActionResult> NewMeeting(NewMeetingViewModel vm)
        {
            vm.UserId = UserId.Value;
            var serviceResult = await _meetingRepository.NewMeeting(vm);

            TempData.AddResult(serviceResult);

            return RedirectToAction(nameof(Index));
        }
    }
}
