using BusinessObject.WebModule.Contract;
using EntitiesObject.Entities.WebEntities;
using MyConfig;
using Newtonsoft.Json;
using Phystones.Enums;
using Phystones.Helper.DataTables;
using Phystones.Models.Config;
using Phystones.Models.ContentData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyAdmin.Controllers
{
    public class ContactController : Controller
    {
        #region Variables

        private IWebBusiness _webBusiness;
        public ContactController(IWebBusiness webBusiness)
        {
            _webBusiness = webBusiness;
        }

        #endregion
        // GET: Sample
        public ActionResult Index()
        {
            var model = new ContactConfigModel();
            var configData = _webBusiness.ConfigGetByKey(ConfigKeyEnum.ContactKey.ToString());
            if (configData != null)
            {
                var json = configData.Value;
                model = JsonConvert.DeserializeObject<ContactConfigModel>(json);
                return View(model);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult SendMess(SendContact model)
        {
            if (ModelState.IsValid) {
                var result = _webBusiness.SaveDataContact(model.Name,model.Phone,model.Email,model.Messenger);
            }
            return PartialView();
        }
        public ActionResult SendMess()
        {
            var model = new SendContact();
            return PartialView(model);
        }
        public ActionResult SaveData()
        {
            var model = new SimpleDataModel();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult SaveData(SimpleDataModel model)
        {
            if (ModelState.IsValid)
            {
                //_webBusiness.SaveDataSample(model.Id, model.Content, model.SyntaxId);
            }
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
            var items = new List<Out_Contact_GetListData_Result>();

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
                    _webBusiness.ListDataContact(startIndex, pageSize, orderColumn, orderDirection, out totalRow);
            }

            var list = items.Select(c => new
            {
                c.Id,
             CreateDate = c.CreateDate.GetValueOrDefault().ToString("dd/MM/yyyy HH:mm:ss"),
             c.Email,
             c.Phone,
             c.RowNumber,
             c.Messenger,
             c.Name,
            }).ToArray();

            response.sEcho = dataTablesParam.Draw;
            response.aaData = list.Cast<Object>().ToArray();
            response.iTotalRecords = totalRow;
            response.iTotalDisplayRecords = totalRow;
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}