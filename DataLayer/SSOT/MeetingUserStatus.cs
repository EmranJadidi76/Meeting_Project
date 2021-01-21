using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.SSOT
{
    public enum MeetingUserStatus
    {
        [Display(Name = "مشاهده نشده")]
        NotSee,

        [Display(Name = "مشاهده شده")]
        See,

        [Display(Name = "قبول کرده")]
        Accept,

        [Display(Name = "رد کرده")]
        Reject
    }
}
