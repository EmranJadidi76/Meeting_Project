using DataLayer.Entities.User;
using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entities.Meeting
{
    public class MeetingUsers : BaseEntity<int>
    {
        public int UserId { get; set; }

        public int MeetingId { get; set; }

        public string Vote { get; set; }

        public bool IsVote { get; set; }

        public string TimeIds { get; set; }

        public MeetingUserStatus? Status { get; set; }

     

        [ForeignKey(nameof(UserId))]
        public virtual Users Users { get; set; }

        [ForeignKey(nameof(MeetingId))]
        public virtual Meetings Meetings { get; set; }

    }
}
