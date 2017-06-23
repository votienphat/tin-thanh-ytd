using BusinessObject.WebModule.Contract;
using EntitiesObject.Entities.WebEntities;
using KaraSys.Helper;
using KaraSys.Helper.DataTables;
using KaraSys.Models.ContentData;
using MyConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace KaraSys.Controllers
{
    public class ArticleController : Controller
    {
        #region Variables

        private IWebBusiness _webBusiness;
        public ArticleController(IWebBusiness webBusiness)
        {
            _webBusiness = webBusiness;
        }

        #endregion
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult SaveData(int Id)
        {
            ViewBag.CateList = _webBusiness.CategoryArticleGetList();
            var model = new ArticleModel();
            if (Id > 0)
            {
                var obj = _webBusiness.ArticleGetById(Id);
                if (obj != null)
                {
                    model.Id = obj.Id;
                    model.CategoryId = obj.CategoryId.GetValueOrDefault();
                    model.Title = obj.Title;
                    model.ContentBody = obj.ContentBody;
                }
            }

            return PartialView(model);
        }
        [Authorize]
        [HttpPost]
        public ActionResult SaveData(ArticleModel model)
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
                var result = _webBusiness.SaveDataArticle(model.Id, model.Title, pathImage, model.ContentBody, model.CategoryId);
                if (result > 0)
                {
                    ViewBag.Success = "Success";
                }
            }
            ViewBag.CateList = _webBusiness.CategoryArticleGetList();
            return PartialView();
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
            var items = new List<Out_Article_GetListData_Result>();

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
                    _webBusiness.ListDataArticle(startIndex, pageSize, orderColumn, orderDirection, out totalRow);
            }

            var list = items.Select(c => new
            {
                c.RowNumber,
                c.Name,
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

        public ActionResult ArticleDetail(string Id)
        {
            var obj = new Out_Article_GetByTextId_Result();
            if (!string.IsNullOrEmpty(Id))
            {
                obj = _webBusiness.ArticleGetByTextId(Id);
            }
            return View(obj);
        }
    }
}