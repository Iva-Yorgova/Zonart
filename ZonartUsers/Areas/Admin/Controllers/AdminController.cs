using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ZonartUsers.Areas.Admin.AdminConstants;

namespace ZonartUsers.Areas.Admin.Controllers
{

        [Area(AreaName)]
        [Authorize(Roles = AdminRoleName)]
        public abstract class AdminController : Controller
        {

        }

}
