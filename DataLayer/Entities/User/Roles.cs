using Microsoft.AspNetCore.Identity;

namespace DataLayer.Entities.User
{
    public class Roles : IdentityRole<int>, IEntity
    {
        public string RoleTitle { get; set; }


    }
}
