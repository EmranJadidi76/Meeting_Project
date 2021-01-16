using AutoMapper;
using Core.Utilities;
using DataLayer.Entities.Meeting;
using DataLayer.ViewModels.Meeting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repositories.Meeting
{
    public class MeetingTimesRepository : GenericRepository<MeetingTimes>
    {
        public MeetingTimesRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<SweetAlertExtenstion> AddTimes(int meetingId, List<MeetingTimesViewModel> vm)
        {
            foreach (var item in vm)
            {
                var model = Mapper.Map<MeetingTimes>(item);
                model.MeetingId = meetingId;
                await AddAsync(model, false);
            }

            return await SaveAsync();
        }
    }
}
