using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModels.User
{
    public class CreateUserViewModel
    {
     
        [Required()]
        public string NationalCode { get; set; }


        public bool IsActive { get; set; } = true;
    }
}
