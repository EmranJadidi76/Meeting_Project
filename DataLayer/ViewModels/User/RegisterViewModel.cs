using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModels.User
{
    public class RegisterViewModel
    {

        public string FirstName{ get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessage = "لطفا رمز عبور خود را وارد نمایید")]
        [StringLength(100, ErrorMessage = "تعداد کاراکتر ها بیشتر از حد مجاز می باشد")]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage = "رمز عبور با تاییدش مغایرت دارد" )]
        public string RePassword { get; set; }

        [Required()]
        public string NationalCode { get; set; }
    }
}
