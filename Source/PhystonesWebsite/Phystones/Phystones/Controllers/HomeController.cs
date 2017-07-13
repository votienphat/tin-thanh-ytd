using System.Web.Mvc;
using BusinessObject.WebModule.Contract;
using Phystones.Models.Enum;
using MyUtility.Extensions;
using System.Linq;
using Phystones.Models.Article;
using Newtonsoft.Json;
using Phystones.Enums;

namespace Phystones.Controllers
{
    public class HomeController : WebBaseController
    {
        #region Variables

        private IWebBusiness _webBusiness;
        public HomeController(IWebBusiness webBusiness)
        {
            _webBusiness = webBusiness;
        }

        #endregion

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
    }
}