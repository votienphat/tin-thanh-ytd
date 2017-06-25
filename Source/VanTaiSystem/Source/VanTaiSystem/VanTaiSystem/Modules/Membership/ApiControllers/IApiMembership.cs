using System.Net;
using System.Web.Http.Controllers;
using VanTaiSystem.Modules.Base.Models;
using VanTaiSystem.Modules.Membership.Models;

namespace VanTaiSystem.Modules.Membership.ApiControllers
{
    public interface IApiMembership
    {
        ApiBaseResponse Login(ApiLoginRequest request, out HttpStatusCode httpStatusCode);

        ApiBaseResponse UpdatePassword(ApiUpdatePasswordRequest request, out HttpStatusCode httpStatusCode);
    }
}
