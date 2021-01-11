using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.Meeting
{
    public class NewMeetingViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public DateTime? MeetingDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
