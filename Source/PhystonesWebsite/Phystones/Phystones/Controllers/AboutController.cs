<<<<<<< HEAD
﻿using BusinessObject.WebModule.Contract;
using EntitiesObject.Entities.WebEntities;
using Phystones.Helper;
using Phystones.Helper.DataTables;
using Phystones.Models.ContentData;
using MyConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phystones.Controllers
{
    public class AboutController : Controller
    {
        #region Variables

        private IWebBusiness _webBusiness;
        public AboutController(IWebBusiness webBusiness)
        {
            _webBusiness = webBusiness;
        }

        #endregion
        // GET: Syntax
        public ActionResult Index()
        {
            var portfolio = _webBusiness.GetList();
            ViewBag.Portfolio = portfolio;
            return View();
        }
        #region List Data
        public ActionResult ListData()
        {
            return View();
        }
        public JsonResult List(DataTablesParam dataTablesParam)
        {
            var response = new DataTablesData
            {
                aaData = new object[0],
                sEcho = dataTablesParam.Draw,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0
            };

            var pageSize = dataTablesParam.Length;
            if (pageSize <= 0)
            {
                pageSize = MyConfiguration.Default.PageSize;
            }

            var totalRow = 0;
            var items = new List<Out_Portfolio_GetListData_Result>();

            if (pageSize > 0)
            {
                var startIndex = (dataTablesParam.Start < 0 ? 0 : dataTablesParam.Start);
                var orderColumn = 0;
                switch (dataTablesParam.OrderColumn)
                {
                    case 1:
                        orderColumn = 0;
                        break;
                }
                var orderDirection = dataTablesParam.IsAscOrdering ? true : false;
                items =
                    _webBusiness.ListDataPortfolio(startIndex, pageSize, orderColumn, orderDirection, out totalRow);
            }

            var list = items.Select(c => new
            {
                c.RowNumber,
                c.Name,
                c.Id,
                c.CategoryPortfolioName
            }).ToArray();

            response.sEcho = dataTablesParam.Draw;
            response.aaData = list.Cast<Object>().ToArray();
            response.iTotalRecords = totalRow;
            response.iTotalDisplayRecords = totalRow;
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region drop syntax
        //public JsonResult Drop_Syntax(string text)
        //{
        //    var query = _webBusiness.SyntaxGetAll(text);
        //    return Json(query.Select(x => new
        //    {
        //        Id = x.Id,
        //        Name = x.Name
        //    }), JsonRequestBehavior.AllowGet);
        //}
        #endregion


        public ActionResult Content(int Id)
        {
            var modeldata = _webBusiness.PortfolioGetById(Id);
            var model = new PortfolioView();
            if (modeldata != null) {
                model.Id = modeldata.Id;
                model.Name = modeldata.Name;
                model.Avatar = modeldata.Avatar;
                model.About = modeldata.About;
                model.CategoryId = modeldata.CategoryId;
                model.CategoryName = modeldata.CategoryName;
                model.LinkWeb = modeldata.LinkWeb;
                model.LinkProfile = modeldata.LinkProfile;
            }
            return PartialView(model);
        }

        [Authorize]
        public ActionResult SaveData(int Id)
        {
            ViewBag.CateList = _webBusiness.GetListCategoryPortfolio();
            var model = new PortfolioModel();
            if (Id > 0)
            {
                var obj = _webBusiness.PortfolioGetById(Id);
                if (obj != null)
                {
                    model.Id= obj.Id;
                    model.Name= obj.Name;
                    model.Avatar= obj.Avatar;
                    model.About= obj.About;
                    model.CategoryId= obj.CategoryId;
                    model.CategoryName= obj.CategoryName;
                    model.LinkWeb= obj.LinkWeb;
                    model.LinkProfile = obj.LinkProfile;
                }
            }
            return PartialView(model);
        }
        [Authorize]
        [HttpPost]
        public ActionResult SaveData(PortfolioModel model)
        {
            if (ModelState.IsValid)
            {
                var ext = ImageHelper.GetFileExtension(model.ImgeString);
                var file = ImageHelper.Base64ToImage(model.ImgeString);
                var filename = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss").Replace(@"/", string.Empty);
                filename = filename.Replace(@":", string.Empty);
                filename = filename.Replace(@" ", string.Empty) + "." + ext;
                var path = Path.Combine(Server.MapPath("~/Content/Upload/"), filename);
                file.Save(path);
                var pathImage = "../Content/Upload/" + filename;
                var result = _webBusiness.SaveDataPortfolio(model.Id,model.Name, pathImage, model.About, model.CategoryId,model.LinkWeb,model.LinkProfile);
                if (result > 0)
                {
                    ViewBag.Success = "Success";
                }
            }
            ViewBag.CateList = _webBusiness.CategoryArticleGetList();
            return PartialView();
        }

    }
=======
﻿using BusinessObject.WebModule.Contract;
using EntitiesObject.Entities.WebEntities;
using Phystones.Helper;
using Phystones.Helper.DataTables;
using Phystones.Models.ContentData;
using MyConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntitiesObject.Message.Content;
using EntitiesObject.Message.Enum;

namespace Phystones.Controllers
{
    public class AboutController : Controller
    {
        #region Variables

        private IWebBusiness _webBusiness;
        public AboutController(IWebBusiness webBusiness)
        {
            _webBusiness = webBusiness;
        }

        #endregion
        // GET: Syntax
        public ActionResult Index()
        {
            var portfolio = _webBusiness.GetList();
            ViewBag.Portfolio = portfolio;
            ViewBag.Content = _webBusiness.GetSlogan(SloganEnum.About);
            ViewBag.WhatWeDo = _webBusiness.GetPlainByType(PlainEnum.WhatWedo);
            ViewBag.ThePlayers = _webBusiness.GetPlainByType(PlainEnum.ThePlayers);
            ViewBag.ThePartners = _webBusiness.GetPlainByType(PlainEnum.ThePartners);

            return View();
        }
        #region List Data
        public ActionResult ListData()
        {
            return View();
        }
        public JsonResult List(DataTablesParam dataTablesParam)
        {
            var response = new DataTablesData
            {
                aaData = new object[0],
                sEcho = dataTablesParam.Draw,
                iTotalRecords = 0,
                iTotalDisplayRecords = 0
            };

            var pageSize = dataTablesParam.Length;
            if (pageSize <= 0)
            {
                pageSize = MyConfiguration.Default.PageSize;
            }

            var totalRow = 0;
            var items = new List<Out_Portfolio_GetListData_Result>();

            if (pageSize > 0)
            {
                var startIndex = (dataTablesParam.Start < 0 ? 0 : dataTablesParam.Start);
                var orderColumn = 0;
                switch (dataTablesParam.OrderColumn)
                {
                    case 1:
                        orderColumn = 0;
                        break;
                }
                var orderDirection = dataTablesParam.IsAscOrdering ? true : false;
                items =
                    _webBusiness.ListDataPortfolio(startIndex, pageSize, orderColumn, orderDirection, out totalRow);
            }

            var list = items.Select(c => new
            {
                c.RowNumber,
                c.Name,
                c.Id,
                c.CategoryPortfolioName
            }).ToArray();

            response.sEcho = dataTablesParam.Draw;
            response.aaData = list.Cast<Object>().ToArray();
            response.iTotalRecords = totalRow;
            response.iTotalDisplayRecords = totalRow;
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region drop syntax
        //public JsonResult Drop_Syntax(string text)
        //{
        //    var query = _webBusiness.SyntaxGetAll(text);
        //    return Json(query.Select(x => new
        //    {
        //        Id = x.Id,
        //        Name = x.Name
        //    }), JsonRequestBehavior.AllowGet);
        //}
        #endregion


        public ActionResult Content(int Id)
        {
            var modeldata = _webBusiness.PortfolioGetById(Id);
            var model = new PortfolioView();
            if (modeldata != null) {
                model.Id = modeldata.Id;
                model.Name = modeldata.Name;
                model.Avatar = modeldata.Avatar;
                model.About = modeldata.About;
                model.CategoryId = modeldata.CategoryId;
                model.CategoryName = modeldata.CategoryName;
                model.LinkWeb = modeldata.LinkWeb;
                model.LinkProfile = modeldata.LinkProfile;
            }
            return PartialView(model);
        }

        [Authorize]
        public ActionResult SaveData(int Id)
        {
            ViewBag.CateList = _webBusiness.GetListCategoryPortfolio();
            var model = new PortfolioModel();
            if (Id > 0)
            {
                var obj = _webBusiness.PortfolioGetById(Id);
                if (obj != null)
                {
                    model.Id= obj.Id;
                    model.Name= obj.Name;
                    model.Avatar= obj.Avatar;
                    model.About= obj.About;
                    model.CategoryId= obj.CategoryId;
                    model.CategoryName= obj.CategoryName;
                    model.LinkWeb= obj.LinkWeb;
                    model.LinkProfile = obj.LinkProfile;
                }
            }
            return PartialView(model);
        }
        [Authorize]
        [HttpPost]
        public ActionResult SaveData(PortfolioModel model)
        {
            if (ModelState.IsValid)
            {
                var ext = ImageHelper.GetFileExtension(model.ImgeString);
                var file = ImageHelper.Base64ToImage(model.ImgeString);
                var filename = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss").Replace(@"/", string.Empty);
                filename = filename.Replace(@":", string.Empty);
                filename = filename.Replace(@" ", string.Empty) + "." + ext;
                var path = Path.Combine(Server.MapPath("~/Content/Upload/"), filename);
                file.Save(path);
                var pathImage = "../Content/Upload/" + filename;
                var result = _webBusiness.SaveDataPortfolio(model.Id,model.Name, pathImage, model.About, model.CategoryId,model.LinkWeb,model.LinkProfile);
                if (result > 0)
                {
                    ViewBag.Success = "Success";
                }
            }
            ViewBag.CateList = _webBusiness.CategoryArticleGetList();
            return PartialView();
        }

    }
>>>>>>> 7c95c30dc45d72cd825a1900048f07bb52b4624c
}