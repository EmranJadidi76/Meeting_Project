using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Repositories.Meeting;
using System.Threading.Tasks;
using ServiceLayer.Repositories.User;
using DataLayer.SSOT;

namespace Meeting_Project.Controllers
{
    public class HomeController : BaseController
    {
        #region DI

        private readonly MeetingRepository _meetingRepository;
        private readonly UserRepository _userRepository;

        public HomeController(MeetingRepository meetingRepository, UserRepository userRepository)
        {
            _meetingRepository = meetingRepository;
            _userRepository = userRepository;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            var model = await _meetingRepository.GetUserMeetings(this.UserId.Value);

            return View(model);
        }


        public async Task<IActionResult> Detail(int id)
        {
            var model = await _meetingRepository.MeetingDetail(id,this.UserId??0);

           

            ViewBag.MeetingId = id;

            return View(model);
        }

       
    }
}
