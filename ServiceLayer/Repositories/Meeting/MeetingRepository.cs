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
        public MeetingRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Meetings>> GetMyMeeting(int? userId)
        {
            var model = await GetListAsync(a => a.UserId == userId, o => o.OrderByDescending(x => x.CreateDate));

            return model;
        }

        public async Task<SweetAlertExtenstion> NewMeeting(NewMeetingViewModel vm)
        {
            var model = Mapper.Map<Meetings>(vm);

            await AddAsync(model);

            return await SaveAsync();
        }
    }
}
