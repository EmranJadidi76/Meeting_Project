using DataLayer.Entities.User;
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


    }
}
