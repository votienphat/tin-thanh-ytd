using System;
using System.Web.Mvc;
using BusinessObject.WebUserModule.Contract;
using MyAdmin.Controllers;
using MyAdmin.Helper;
using MyUtility.Extensions;
using MyAdmin.Models.Article;

namespace MyAdmin.Areas.WebUserModule.Controllers
{
    public class ArticleController : BaseController
    {
        private readonly IArticleBusiness _articleBusiness;
        private readonly ICategorieBusiness _categorieBusiness;
        public ArticleController(IArticleBusiness articleBusiness, ICategorieBusiness categorieBusiness)
        {
            _articleBusiness = articleBusiness;
            _categorieBusiness = categorieBusiness;
        }

        public ActionResult Index()
        {
            var listCate = _categorieBusiness.GetListCategorie();
            ViewBag.Categorie = listCate;
            return View();
        }
        [HttpPost]
        public ActionResult SaveArticle(CreateArticleModel model)
        {
            var ex = "";
            model.Content = HtmlFillter.FillterBase64SaveToService(model.Content, model.Title.TextId());
            var bytes = ImageHelper.Base64ToByte(model.stringImageArticle, out ex);
            var filename = string.Format("{0}.{1}", DateTime.Now.Ticks, ex);
            var responupload = WebHelper.UploadImage(filename, bytes);
            _articleBusiness.SaveArticle(model.Title, model.CategoryID, model.Content, responupload.ImagePath,
                model.Status, 1, model.ShortDescription, model.TextId, SessionManager.SessionData.NickName,
                model.ArticleID);
            return PartialView(model);
        }
    }
}