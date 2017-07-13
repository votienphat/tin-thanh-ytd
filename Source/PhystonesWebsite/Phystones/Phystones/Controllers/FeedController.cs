using BusinessObject.WebModule.Contract;
using EntitiesObject.Entities.WebEntities;
using Phystones.Helper.DataTables;
using MyConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Phystones.Models.Enum;
using MyUtility.Extensions;
using Phystones.Helper;

namespace Phystones.Controllers
{
    public class FeedController : Controller
    {
        #region Variables

        private IWebBusiness _webBusiness;
        public FeedController(IWebBusiness webBusiness)
        {
            _webBusiness = webBusiness;
        }

        #endregion
        // GET: Syntax
        public ActionResult Index()
        {
            return View();
        }
          public JsonResult DataBlog(int StartIndex)
        {
            var pageSize = 5;
            int total;
            var rs = _webBusiness.GetArticleBlog((int)CategoryArticleEnum.Blog,
                (StartIndex - 1) * pageSize, pageSize, out total);
             var ListItem = rs.Select(c => new
            {
               ContentBody = StringExtension.CutNick(c.ContentBody,200,"..."),
               ImageLink = c.Image,
               c.Title,
                c.RowNumber,
                LinkDetail = MyExtention.GetUrlHelper().RouteUrl(RouteName.ArticleDetail.Text(), new { textid = c.TextId })
            });
            var TotalItem = total;
            return Json(new{ Data = ListItem,TotalItem =TotalItem}, JsonRequestBehavior.AllowGet);
        }
    }
}