﻿using BusinessObject.WebModule.Contract;
using EntitiesObject.Entities.WebEntities;
using Phystones.Helper.DataTables;
using MyConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phystones.Controllers
{
    public class JobController : Controller
    {
        #region Variables

        private IWebBusiness _webBusiness;
        public JobController(IWebBusiness webBusiness)
        {
            _webBusiness = webBusiness;
        }

        #endregion
        // GET: Syntax
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult JobDetail(string textId)
        {
            return View();
        }
    }
}