using AutoMapper;
using Core.Utilities;
using DataLayer.Entities.Meeting;
using DataLayer.ViewModels.Meeting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DNTPersianUtils.Core;
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

        public List<string> GetTimeByTimeIds(string times)
        {
            if (string.IsNullOrEmpty(times)) return null;

            var json = JsonConvert.DeserializeObject<List<int>>(times);

            var model = GetList(a => json.Contains(a.Id));

            var stringTimes = new List<string>();

            foreach (var item in model)
            {
                stringTimes.Add($@"{item.Start.ToLongPersianDateTimeString()} --- {item.End.ToLongPersianDateTimeString()}");
            }

            return stringTimes;
        }
    }
}
