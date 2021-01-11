using DataLayer.Entities.Meeting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public DateTime? CreateDate { get; set; }
        public bool IsActive { get; set; }

        public bool IsModerator { get; set; }

        public string NationalCode { get; set; }

        public ICollection<MeetingUsers> MeetingUsers { get; set; }
        public ICollection<Meetings> Meetings { get; set; }
    }
}
