using AutoMapper;
using Core.Utilities;
using DataLayer.Entities.Meeting;
using DataLayer.ViewModels.Meeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repositories.Meeting
{
    public class MeetingRepository : GenericRepository<Meetings>
    {
        private readonly MeetingTimesRepository _meetingTimesRepository;
        private readonly MeetingUsersRepository _meetingUsersRepository;
        public MeetingRepository(DatabaseContext dbContext, MeetingTimesRepository meetingTimesRepository
            , MeetingUsersRepository meetingUsersRepository) : base(dbContext)
        {
            _meetingTimesRepository = meetingTimesRepository;
            _meetingUsersRepository = meetingUsersRepository;
        }

        public async Task<IEnumerable<Meetings>> GetMyMeeting(int? userId)
        {
            var model = await GetListAsync(a => a.UserId == userId, o => o.OrderByDescending(x => x.CreateDate));

            return model;
        }

        public async Task<SweetAlertExtenstion> NewMeeting(NewMeetingViewModel model, List<MeetingTimesViewModel> vm, List<int> users)
        {
            var meeting = Mapper.Map<Meetings>(model);

            await AddAsync(meeting);

            await _meetingTimesRepository.AddTimes(meeting.Id, vm);
            await _meetingUsersRepository.AddUsers(meeting.Id, users);

            return await SaveAsync();
        }
    }
}
