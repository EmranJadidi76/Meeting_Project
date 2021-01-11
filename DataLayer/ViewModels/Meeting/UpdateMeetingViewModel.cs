using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.Meeting
{
    public class UpdateMeetingViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? MeetingDate { get; set; }

    }
}
