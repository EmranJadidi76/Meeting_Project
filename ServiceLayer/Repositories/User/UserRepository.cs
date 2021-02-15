using DataLayer.Entities.User;
using Microsoft.AspNetCore.Identity;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repositories.User
{
    public class UserRepository : GenericRepository<Users>
    {
        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;

        public UserRepository(DatabaseContext context, SignInManager<Users> signInManager, UserManager<Users> userManager) :base(context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public UserManager<Users> UserManager => _userManager;
        public SignInManager<Users> SignInManager => _signInManager;


        public async Task<IEnumerable<Users>> GetAllUsers(int? userId)
        {
            var model = await GetListAsync(a=> a.Id != userId && a.ParentId == userId);

            return model;
        }
    }
}
