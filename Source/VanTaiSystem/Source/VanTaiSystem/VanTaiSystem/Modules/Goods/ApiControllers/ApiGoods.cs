using System.Linq;
using System.Net;
using BussinessObject.GoodsModule.Contract;
using MyUtility.Extensions;
using VanTaiSystem.Helper;
using VanTaiSystem.Modules.Base.Enums;
using VanTaiSystem.Modules.Base.Models;

namespace VanTaiSystem.Modules.Goods.ApiControllers
{
    public class ApiGoods : IApiGoods
    {
        private readonly IGoodsBusiness _goodsBusiness;

        public ApiGoods(IGoodsBusiness goodsBusiness)
        {
            _goodsBusiness = goodsBusiness;
        }

        public ApiBaseResponse POSearch(BaseSearchRequest request, out HttpStatusCode httpStatusCode)
        {
            var result = new ApiBaseResponse
            {
                Code = ApiStatusCode.Success,
                Message = string.Empty
            };

            int totalRows;
            var data = _goodsBusiness.POSearch(out totalRows, request.Keyword, request.StarTime, request.EndTime,
                request.StartIndex, request.EndIndex);
            result.Data = new BaseSearchResponse
            {
                TotalRow = totalRows,
                Items = data
            };

            httpStatusCode = HttpStatusCode.OK;
            return result;
        }
    }
}