using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities.User
{
    public class Users : IdentityUser<int>, IEntity
    {
        public Users()
        {
            CreateDate = DateTime.Now;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool IsActive { get; set; }

        public bool IsModerator { get; set; }

        public string NationalCode { get; set; }
    }
}
