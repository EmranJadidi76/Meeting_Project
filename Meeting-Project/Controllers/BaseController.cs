using Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Meeting_Project.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private int? _userId;

        public int? UserId
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (_userId != null)
                        return _userId;
                    _userId = int.Parse(User.Identity.FindFirstValue(ClaimTypes.NameIdentifier));
                    return _userId;
                }
                return null;
            }
        }

    }
}
