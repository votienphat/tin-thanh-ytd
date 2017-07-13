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
using Phystones.Models.ContentData;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using System.Text;
using HtmlAgilityPack;
using System.Web;
using EntitiesObject.Message.Content;

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
            ViewBag.Register = _webBusiness.GetSlogan(SloganEnum.Register);
            return View();
        }

        [HttpPost]
        public ActionResult SendMess(Register model)
        {
            if (ModelState.IsValid)
            {
                _webBusiness.RegisterCompany(model.MST, model.CompanyName, model.Address, model.CEO, model.PackedRegister, model.TypeRegister, model.Email, model.ContactPreson, model.ReceiveAddress);
            }
            return PartialView(model);
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

        public JsonResult DataCompany(string Name)
        {
            var info = GetCompanyInfo(Name);
            return new JsonResult() { Data = new { MST = info.MST, CompanyName = info.CompanyName, Address = info.Address, CEO = info.CEO }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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
        public ActionResult SaveUploadedFile(int idCustommer)
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {

                        var originalDirectory = new DirectoryInfo(string.Format("{0}Customers", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), idCustommer.ToString());

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);
                    }
                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }


            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }

        #region List Data
        public ActionResult ListData()
        {
            return View();
        }
        public JsonResult List(DataTablesParam dataTablesParam,string keyWord)
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
            var items = new List<Out_RegisterCompany_GetListData_Result>();

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
                    _webBusiness.ListDataRegisterCompany(keyWord ?? string.Empty,startIndex, pageSize, orderColumn, orderDirection, out totalRow);
            }

            var list = items.Select(c => new
            {
                 c.id
                ,c.MST
                ,c.CompanyName
                ,c.Address
                ,c.CEO
                ,c.PackedRegister
                ,c.TypeRegister
                ,c.Email
                ,c.ContactPreson
                ,c.ReceiveAddress
                ,Action = c.id
            }).ToArray();
            response.sEcho = dataTablesParam.Draw;
            response.aaData = list.Cast<Object>().ToArray();
            response.iTotalRecords = totalRow;
            response.iTotalDisplayRecords = totalRow;
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion
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