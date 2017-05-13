using System.Web.Mvc;
using Phystones.ActionFilter;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Linq;
using System;
using Phystones.Helper;
using Phystones.Models.Home;

namespace Phystones.Controllers
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