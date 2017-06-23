using System.Web.Mvc;
using KaraSys.ActionFilter;

namespace KaraSys.Controllers
{
    //[Authorize]
    [HeaderAuthorizeFilter(IsCheckPermission = true)]
    public class BaseController : Controller
    {
    }
}