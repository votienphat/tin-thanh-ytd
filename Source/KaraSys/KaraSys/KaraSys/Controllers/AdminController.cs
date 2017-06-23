using System.Web.Mvc;
using KaraSys.ActionFilter;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Linq;
using System;
using KaraSys.Helper;
using KaraSys.Models.Home;

namespace KaraSys.Controllers
{
    public class AdminController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}