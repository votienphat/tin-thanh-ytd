using System.Web.Mvc;
using MyAdmin.ActionFilter;

namespace MyAdmin.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {
        }
        [HeaderAuthorizeFilter(IsCheckPermission = false)]
        public ActionResult Index()
        {

            return View();
        }
        [AllowAnonymous]
        public ActionResult FormData()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult TableData()
        {
            return View();
        }
    }
}