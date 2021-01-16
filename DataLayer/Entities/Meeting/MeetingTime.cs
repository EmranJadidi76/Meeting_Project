using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataLayer.Entities.Meeting
{
    public class MeetingTimes : BaseEntity
    {
        public int MeetingId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        [ForeignKey(nameof(MeetingId))]
        public virtual Meetings Meetings { get; set; }
    }
}
