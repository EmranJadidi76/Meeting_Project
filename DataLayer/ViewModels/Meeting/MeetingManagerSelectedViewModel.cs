using DataLayer.SSOT;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels.Meeting
{
    public class MeetingManagerSelectedViewModel
    {
        public int Id { get; set; }

        public string Vote { get; set; }

        public bool IsVote { get; set; } = true;

        public string TimeIds { get; set; }
    }
}
