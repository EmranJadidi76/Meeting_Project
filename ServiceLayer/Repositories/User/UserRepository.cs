using DataLayer.Entities.User;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Repositories.User
{
    public class UserRepository : GenericRepository<Users>
    {
        public UserRepository(DatabaseContext context) :base(context)
        {

        }
    }
}
