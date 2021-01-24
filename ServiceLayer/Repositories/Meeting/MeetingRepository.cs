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

        public MeetingTimesRepository MeetingTimesRepository => _meetingTimesRepository;
        public MeetingUsersRepository MeetingUsersRepository => _meetingUsersRepository;

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


        public async Task<Tuple<IEnumerable<MeetingTimes>, IEnumerable<MeetingUsers>, Meetings>> MeetingDetail(int id)
        {
            var times = await MeetingTimesRepository.GetListAsync(a => a.MeetingId == id);
            var users = await MeetingUsersRepository.GetListAsync(a => a.MeetingId == id, includes: "Users");
            var meetings = await GetByConditionAsync(a => a.Id == id);

            return new Tuple<IEnumerable<MeetingTimes>, IEnumerable<MeetingUsers>, Meetings>(times, users, meetings);
        }

        public async Task<IEnumerable<Meetings>> GetUserMeetings(int userId)
        {
            var meetingIds = await MeetingUsersRepository.ListMeetingsbyUserId(userId);

            var meetings = await GetListAsync(a => meetingIds.Contains(a.Id), o => o.OrderByDescending(a => a.CreateDate), includes: "User,MeetingUsers");

            return meetings;
        }

        public async Task<SweetAlertExtenstion> UpdateMeeting(MeetingEditViewModel vm)
        {
            var model = await GetByConditionAsync(a => a.Id == vm.Id);

            if (model == null)
                return SweetAlertExtenstion.Error("اطلاعاتی با این شناسه یافت نشد");

            var edit = Mapper.Map(vm, model);

            await UpdateAsync(edit);

            return SweetAlertExtenstion.Ok();
        }

        public async Task<SweetAlertExtenstion> ManagerSelectTime(MeetingManagerSelectedViewModel vm)
        {
            var model = await GetByConditionAsync(a => a.Id == vm.Id);

            if (model == null)
                return SweetAlertExtenstion.Error("خطایی در سرور رخ داده است لطفا مجددا تلاش بفرمایید");


            var updated = Mapper.Map(vm, model);

            await UpdateAsync(updated);

            return SweetAlertExtenstion.Ok();
        }

        public async Task<SweetAlertExtenstion> DeleteAll(int id)
        {
            var model = await GetByConditionAsync(a => a.Id == id, includes: "MeetingUsers,MeetingTimes");

            if (model.MeetingUsers.Any())
            {
                await MeetingUsersRepository.DeleteRangeAsync(model.MeetingUsers);
            }

            if (model.MeetingTimes.Any())
            {
                await MeetingTimesRepository.DeleteRangeAsync(model.MeetingTimes);
            }

            await DeleteAsync(model);

            return SweetAlertExtenstion.Ok();
        }
    }
}
