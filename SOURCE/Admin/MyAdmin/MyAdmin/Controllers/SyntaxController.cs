using BusinessObject.WebModule.Contract;
using EntitiesObject.Entities.WebEntities;
using MyAdmin.Helper.DataTables;
using MyConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyAdmin.Controllers
{
    public class SyntaxController : Controller
    {
        #region Variables

        private IWebBusiness _webBusiness;
        public SyntaxController(IWebBusiness webBusiness)
        {
            _webBusiness = webBusiness;
        }

        #endregion
        // GET: Syntax
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
            var items = new List<Out_Syntax_GetListData_Result>();

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
                    _webBusiness.ListDataSyntax(startIndex, pageSize, orderColumn, orderDirection, out totalRow);
            }

            var list = items.Select(c => new
            {
                c.RowNumber,
                c.Name,
                c.ContentSyntax,
                c.KeyWord,
                c.Description,
            }).ToArray();

            response.sEcho = dataTablesParam.Draw;
            response.aaData = list.Cast<Object>().ToArray();
            response.iTotalRecords = totalRow;
            response.iTotalDisplayRecords = totalRow;
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region drop syntax
        public JsonResult Drop_Syntax(string text)
        {
            var query = _webBusiness.SyntaxGetAll(text);
            return Json(query.Select(x => new
            {
                Id = x.Id,
                Name = x.Name
            }), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}