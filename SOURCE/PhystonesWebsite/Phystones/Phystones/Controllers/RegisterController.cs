using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessObject.WebModule.Contract;
using EntitiesObject.Entities.WebEntities;
using MyConfig;
using Newtonsoft.Json;
using Phystones.Enums;
using Phystones.Helper.DataTables;
using Phystones.Models.Config;
using Phystones.Models.ContentData;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using System.Text;
using HtmlAgilityPack;

namespace Phystones.Controllers
{
    public class RegisterController : Controller
    {
        #region Variables

        private IWebBusiness _webBusiness;
        public RegisterController(IWebBusiness webBusiness)
        {
            _webBusiness = webBusiness;
        }

        #endregion
        // GET: Sample
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendMess(SendContact model)
        {
            if (ModelState.IsValid)
            {
                _webBusiness.SaveDataContact(model.Name, model.Phone, model.Email, model.Messenger,
                    MyConfiguration.Mail.SendingMail, MyConfiguration.Mail.SendingMail2,
                    MyConfiguration.Mail.SendingMailName, MyConfiguration.Mail.SendingMailTitle,
                    MyConfiguration.Mail.ReceiveMails, MyConfiguration.Mail.HostMail, MyConfiguration.Mail.Port);
            }
            return PartialView();
        }
        public ActionResult SendMess()
        {
            var model = new Register();
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
        public JsonResult DataCompany(string Name, int Type)
        {
            string urlAddress = "http://www.hosocongty.vn/search.php?key=" + Name + "&ot=" + Type + "&p=0&d=0";
            HtmlWeb website = new HtmlWeb();
            HtmlDocument rootDocument = website.Load(urlAddress);


            return new JsonResult() { Data = rootDocument, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}