using System.Web.Mvc;
using MyAdmin.ActionFilter;

namespace MyAdmin.Controllers
{
    //[Authorize]
    [HeaderAuthorizeFilter(IsCheckPermission = true)]
    public class BaseController : Controller
    {
    }
}