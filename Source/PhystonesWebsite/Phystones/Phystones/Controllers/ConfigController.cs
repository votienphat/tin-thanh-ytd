using BusinessObject.WebModule.Contract;
using EntitiesObject.Entities.WebEntities;
using Phystones.Helper.DataTables;
using MyConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessObject.WebModule.Enums;
using BusinessObject.WebModule.Models.Config;
using MyUtility;
using MyUtility.Extensions;
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
            ViewBag.Contact = _webBusiness.ConfigGetByKey<ContactConfigModel>(ConfigKeyEnum.ContactKey);
            ViewBag.SEO = _webBusiness.ConfigGetByKey<SEOConfigModel>(ConfigKeyEnum.SEOKey);
            ViewBag.Website = _webBusiness.ConfigGetByKey<WebsiteConfigModel>(ConfigKeyEnum.WebsiteKey);

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ConfigContact(ContactConfigModel model)
        {
            if (ModelState.IsValid)
            {
                var key = ConfigKeyEnum.ContactKey;
                var value = JsonConvert.SerializeObject(model);
                _webBusiness.SaveConfigKey(key.Text(), value);
                ViewBag.Success = true;
            }
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult SEOConfig(SEOConfigModel model)
        {
            if (ModelState.IsValid)
            {
                var key = ConfigKeyEnum.SEOKey;
                var value = JsonConvert.SerializeObject(model);
                _webBusiness.SaveConfigKey(key.Text(), value);
                ViewBag.Success = true;
            }
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult WebsiteConfig(WebsiteConfigModel model)
        {
            if (ModelState.IsValid)
            {
                var key = ConfigKeyEnum.WebsiteKey;
                var value = JsonConvert.SerializeObject(model);
                _webBusiness.SaveConfigKey(key.Text(), value);
                ViewBag.Success = true;
            }
            return PartialView(model);
        }
    }
}