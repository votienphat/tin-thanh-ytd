using System.Net;
using VanTaiSystem.Modules.Base.Models;

namespace VanTaiSystem.Modules.Goods.ApiControllers
{
    public interface IApiGoods
    {
        ApiBaseResponse POSearch(BaseSearchRequest request, out HttpStatusCode httpStatusCode);
    }
}
