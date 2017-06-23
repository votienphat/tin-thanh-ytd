using System.Web.Mvc;
using BusinessObject.WebModule.Contract;
using KaraSys.Models.Enum;
using MyUtility.Extensions;
using System.Linq;
using KaraSys.Models.Article;
using KaraSys.Models.Config;
using Newtonsoft.Json;
using KaraSys.Enums;

namespace KaraSys.Controllers
{
    public class HomeController : BaseController
    {
        #region Variables

        private IWebBusiness _webBusiness;
        public HomeController(IWebBusiness webBusiness)
        {
            _webBusiness = webBusiness;
        }

        #endregion
        [AllowAnonymous]
        public ActionResult Index()
        {
            var plain = _webBusiness.GetByCategoryId(CategoryArticleEnum.Plain.Value());
            var resultplain = plain.Select(x => new ArticleViewModel
            {
                Title = x.Title,
                Image = x.Image,
                Link = x.TextId,
                Id = x.Id,
            }).ToList();
            var slogan = _webBusiness.SloganGet();
            ViewBag.Plain = resultplain;
            ViewBag.Slogan = slogan;
            return View();
        }
        [AllowAnonymous]
        public ActionResult Footer()
        {
            var model = new ContactConfigModel();
            var configData = _webBusiness.ConfigGetByKey(ConfigKeyEnum.ContactKey.ToString());
            if (configData != null)
            {
                var json = configData.Value;
                model = JsonConvert.DeserializeObject<ContactConfigModel>(json);
                return PartialView(model);
            }
            return PartialView(model);
        }
    }
}