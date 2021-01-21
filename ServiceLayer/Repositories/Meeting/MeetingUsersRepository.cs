using AutoMapper;
using Core.Utilities;
using DataLayer.Entities.Meeting;
using DataLayer.SSOT;
using DataLayer.ViewModels.Meeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repositories.Meeting
{
    public class MeetingUsersRepository : GenericRepository<MeetingUsers>
    {
        public MeetingUsersRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<SweetAlertExtenstion> AddUsers(int id, List<int> userId)
        {
            foreach (var item in userId)
            {
                var model = new MeetingUsers();
                model.UserId = item;
                model.Status = MeetingUserStatus.NotSee;
                model.MeetingId = id;
                await AddAsync(model, false);
            }

            return await SaveAsync();
        }

        public async Task<List<int>> ListMeetingsbyUserId(int userId)
        {
            var model = await GetListAsync(a => a.UserId == userId);

            return model.Select(a => a.MeetingId).ToList();
        }

        public async Task UpdateStatus(int meetingId, MeetingUserStatus status)
        {
            var model = await GetByConditionAsync(a => a.Id == meetingId && a.MeetingTimeId == null);

            if (model == null) return;

            model.Status = status;

            await UpdateAsync(model);

        }

        public async Task<SweetAlertExtenstion> UserSelectedTime(MeetingUserSelectedViewModel vm)
        {
            var model = await GetByConditionAsync(a => a.MeetingId == vm.MeetingId && a.UserId == vm.UserId);

            if (model == null)
                return SweetAlertExtenstion.Error("خطایی در سرور رخ داده است لطفا مجددا تلاش بفرمایید");

            if (vm.Status == MeetingUserStatus.Reject)
                vm.MeetingTimeId = null;

            var updated = Mapper.Map(vm, model);

            await UpdateAsync(updated);

            return SweetAlertExtenstion.Ok();
        }
    }
}
