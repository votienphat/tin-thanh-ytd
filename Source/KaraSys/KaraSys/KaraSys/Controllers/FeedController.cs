using BusinessObject.WebModule.Contract;
using EntitiesObject.Entities.WebEntities;
using KaraSys.Helper.DataTables;
using MyConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KaraSys.Controllers
{
    public class FeedController : Controller
    {
        #region Variables

        private IWebBusiness _webBusiness;
        public FeedController(IWebBusiness webBusiness)
        {
            _webBusiness = webBusiness;
        }

        #endregion
        // GET: Syntax
        public ActionResult Index()
        {
            return View();
        }
    }
}