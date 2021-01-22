using DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities.Meeting
{
    public class Meetings : BaseEntity<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public DateTime? MeetingStart { get; set; }
        public DateTime? MeetingEnd { get; set; }

        public string TimeIds { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsActive { get; set; }

        public string Vote { get; set; }

        public bool IsVote { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual Users User{ get; set; }

        public virtual ICollection<MeetingUsers> MeetingUsers { get; set; }

        public virtual ICollection<MeetingTimes> MeetingTimes { get; set; }

    }
}
