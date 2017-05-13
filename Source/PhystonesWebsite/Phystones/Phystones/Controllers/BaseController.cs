using System.Web.Mvc;
using Phystones.ActionFilter;

namespace Phystones.Controllers
{
    //[Authorize]
    [HeaderAuthorizeFilter(IsCheckPermission = true)]
    public class BaseController : Controller
    {
    }
}