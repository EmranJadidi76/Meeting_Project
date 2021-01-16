using Core.Utilities;
using DataLayer.Entities.Meeting;
using System;
using System.Collections.Generic;
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
                model.MeetingId = id;
                await AddAsync(model, false);
            }

            return await SaveAsync();
        }
    }
}
