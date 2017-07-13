using BusinessObject.WebModule.Contract;
using EntitiesObject.Entities.WebEntities;
using MyConfig;
using Phystones.Helper.DataTables;
using Phystones.Models.Slogan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phystones.Controllers
{
    public class SloganController : Controller
    {
        #region Variables
        private IWebBusiness _webBusiness;
        public SloganController(IWebBusiness webBusiness)
        {
            _webBusiness = webBusiness;
        }
        #endregion
        // GET: Slogan
        public ActionResult Index()
        {
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
            var items = new List<Out_Slogan_GetListData_Result>();

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
                items = _webBusiness.ListDataSlogan(startIndex, pageSize, orderColumn, orderDirection, out totalRow);
            }

            var list = items.Select(c => new
            {
                c.RowNumber,
                c.Title,
                c.Id,
                Action = c.Id,
            }).ToArray();
            response.sEcho = dataTablesParam.Draw;
            response.aaData = list.Cast<Object>().ToArray();
            response.iTotalRecords = totalRow;
            response.iTotalDisplayRecords = totalRow;
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Save Data Slogan
        [HttpPost]
        public ActionResult SaveData(SloganViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _webBusiness.SaveDataSlogan(model.Id, model.Title, model.ContentBody, model.Author,model.Language,model.IsActive);
                if (result > 0)
                {
                    ViewBag.Success = "Success";
                }
            }
            return PartialView();
        }
        #endregion
    }
}