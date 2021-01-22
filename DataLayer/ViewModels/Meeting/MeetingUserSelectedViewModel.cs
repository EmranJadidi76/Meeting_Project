using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.Meeting
{
    public class MeetingUserSelectedViewModel
    {
        public int UserId { get; set; }

        public int MeetingId { get; set; }

        public int? MeetingTimeId { get; set; }

        public string Vote { get; set; }

        public bool IsVote { get; set; } = true;

        public MeetingUserStatus? Status { get; set; }
    }
}
