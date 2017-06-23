using BusinessObject.WebModule.Contract;
using EntitiesObject.Entities.WebEntities;
using Phystones.Helper.DataTables;
using MyConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Phystones.Models.Config;
using Newtonsoft.Json;
using Phystones.Enums;

namespace Phystones.Controllers
{
    public class ConfigController : Controller
    {
        #region Variables

        private IWebBusiness _webBusiness;
        public ConfigController(IWebBusiness webBusiness)
        {
            _webBusiness = webBusiness;
        }

        #endregion
        // GET: Syntax
        public ActionResult Index()
        {
            var model = new ContactConfigModel();
            model.KeyConfig = ConfigKeyEnum.ContactKey.ToString();
            var configData = _webBusiness.ConfigGetByKey(ConfigKeyEnum.ContactKey.ToString());
            if (configData != null)
            {
                var json = configData.Value;
                model = JsonConvert.DeserializeObject<ContactConfigModel>(json);
                return View(model);
            }
            return View(model);
        }
        public ActionResult ConfigContact()
        {
            var model = new ContactConfigModel();
            model.KeyConfig = ConfigKeyEnum.ContactKey.ToString();
            var configData = _webBusiness.ConfigGetByKey(ConfigKeyEnum.ContactKey.ToString());
            if (configData != null)
            {
                var json = configData.Value;
                model = JsonConvert.DeserializeObject<ContactConfigModel>(json);
                return PartialView(model);
            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult ConfigContact(ContactConfigModel model)
        {
            if (ModelState.IsValid)
            {
                var key = model.KeyConfig;
                var value = JsonConvert.SerializeObject(model);
                var result = _webBusiness.SaveConfigKey(key, value);
                ViewBag.Success = true;
            }
            return PartialView(model);
        }
    }
}