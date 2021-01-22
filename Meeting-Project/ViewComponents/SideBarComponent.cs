using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meeting_Project.ViewComponents
{
    public class SideBarComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            var isModerator = bool.Parse(User.Identity.FindFirstValue("IsMod"));

            ViewBag.IsModerator = isModerator;

            return View();
        }
    }
}
