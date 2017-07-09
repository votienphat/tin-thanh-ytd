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

        private HoSoCongTyItem GetCompanyInfo(string mst)
        {
            string urlAddress = "http://www.hosocongty.vn/json/home.php?page=1&last_id=0&province_id=0&district_id=0&q=" + mst + "&ot=0";
            var html = new HtmlDocument();
            html.LoadHtml(new WebClient().DownloadString(urlAddress));
            var jsonContent = html.DocumentNode.InnerHtml;
            HoSoCongTySearch json = JsonConvert.DeserializeObject<HoSoCongTySearch>(jsonContent);
            HoSoCongTyItem result = new HoSoCongTyItem();
            var item = json.Items.FirstOrDefault();

            if (item != null)
            {
                urlAddress = "http://www.hosocongty.vn/" + item.Link.Replace("./", "");
                html.LoadHtml(new WebClient().DownloadString(urlAddress));
                byte[] bytes = Encoding.Default.GetBytes(html.DocumentNode.InnerHtml);
                var htmlContent = Encoding.UTF8.GetString(bytes);
                html.LoadHtml(htmlContent);
                var tag = html.DocumentNode
                    .Descendants("div")
                    .FirstOrDefault(x => x.Attributes["class"].Value == "companyDetail");
                if (tag != null)
                {
                    var companyNameTag = tag
                        .Descendants("h1")
                        .FirstOrDefault();
                    result.CompanyName = companyNameTag == null ? "" : companyNameTag.InnerText;

                    companyNameTag = tag
                        .Descendants("p")
                        .FirstOrDefault(x => x.InnerText.Contains("Địa chỉ"));
                    if (companyNameTag != null)
                    {
                        var addressTag = companyNameTag
                            .Descendants("strong")
                            .FirstOrDefault();
                        result.Address = addressTag == null ? "" : addressTag.InnerText;
                    }

                    companyNameTag = tag
                        .Descendants("p")
                        .FirstOrDefault(x => x.InnerText.Contains("Mã số thuế"));
                    if (companyNameTag != null)
                    {
                        var addressTag = companyNameTag
                            .Descendants("strong")
                            .FirstOrDefault();
                        result.MST = addressTag == null ? "" : addressTag.InnerText;
                    }

                    companyNameTag = tag
                        .Descendants("li")
                        .FirstOrDefault(x => x.InnerText.Contains("Giám đốc"));
                    if (companyNameTag != null)
                    {
                        var addressTag = companyNameTag
                            .Descendants("strong")
                            .LastOrDefault();
                        result.CEO = addressTag == null ? "" : addressTag.InnerText;
                    }
                }
            }

            return result;
        }
    }

    public class HoSoCongTyItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string CompanyName { get; set; }

        [JsonProperty("url")]
        public string Link { get; set; }

        [JsonProperty("mst")]
        public string MST { get; set; }

        [JsonProperty("add")]
        public string Address { get; set; }

        [JsonProperty("CEO")]
        public string CEO { get; set; }
    }

    public class HoSoCongTySearch
    {
        [JsonProperty("album")]
        public List<HoSoCongTyItem> Items { get; set; }
    }
}