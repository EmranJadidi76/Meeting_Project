using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModels.User
{
    public class CreateUserViewModel
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required()]
        public string NationalCode { get; set; }


    }
}
