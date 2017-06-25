using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Logger;
using MyUtility.Extensions;
using Newtonsoft.Json;
using VanTaiSystem.Modules.Base.Enums;
using VanTaiSystem.Modules.Base.Models;
using VanTaiSystem.Modules.Goods.ApiControllers;
using VanTaiSystem.Modules.Goods.Enums;

namespace VanTaiSystem.Controllers
{
    public class ApiAdapterController : ApiController
    {
        readonly MediaTypeFormatter _jsonFormater = GlobalConfiguration.Configuration.Formatters.JsonFormatter;

        private IApiGoods _apiGoods;

        public ApiAdapterController(IApiGoods apiGoods)
        {
            _apiGoods = apiGoods;
        }

        #region Public APIs

        /// <summary>
        /// API Adapter nhận tất cả request
        /// </summary>
        /// <param name="formData">
        /// Chỉ cần truyền vào 1 biến tên là input. 
        /// Trong input sẽ có cấu trúc JSON là {MethodName:'Tên method', Data: object}
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [ActionName(ModuleName.Goods)]
        public HttpResponseMessage Goods(HttpRequestMessage formData)
        {
            return ApiAdapter(formData, ModuleCode.Goods);
        }

        private HttpResponseMessage ApiAdapter(HttpRequestMessage formData, ModuleCode module)
        {
            var httpStatusCode = HttpStatusCode.Unauthorized;
            var response = new ApiBaseResponse();

            try
            {
                #region Check input

                if (formData == null)
                {
                    CommonLogger.DefaultLogger.DebugFormat("ApiAdapter | Form rỗng | Module: {0}", module);

                    response.Code = ApiStatusCode.InvalidInput;
                    response.Message = response.Code.Text();
                    httpStatusCode = HttpStatusCode.BadRequest;
                    return Request.CreateResponse(httpStatusCode, response, _jsonFormater);
                }

                var input = formData.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(input))
                {
                    CommonLogger.DefaultLogger.DebugFormat("ApiAdapter | Input rỗng | Module: {0}", module);

                    response.Code = ApiStatusCode.InvalidInput;
                    response.Message = response.Code.Text();
                    httpStatusCode = HttpStatusCode.BadRequest;
                    return Request.CreateResponse(httpStatusCode, response, _jsonFormater);
                }

                var request = JsonConvert.DeserializeObject<ApiBaseRequest>(input);

                if (request == null)
                {
                    CommonLogger.DefaultLogger.DebugFormat("ApiAdapter | Input rỗng | Module: {0} | Input: {1}", module,
                        input);

                    response.Code = ApiStatusCode.InvalidInput;
                    response.Message = response.Code.Text();
                    httpStatusCode = HttpStatusCode.BadRequest;
                    return Request.CreateResponse(httpStatusCode, response, _jsonFormater);
                }

                #endregion

                CommonLogger.DefaultLogger.DebugFormat("ApiName: {0}", request.ApiName);

                response.Code = ApiStatusCode.UnexistedApi;
                response.Message = response.Code.Text();

                switch (module)
                {

                    case ModuleCode.Goods:

                        #region PO

                        var apiGoodsName = request.ApiName.ToEnum<ApiGoodsName>();
                        switch (apiGoodsName)
                        {
                            case ApiGoodsName.POSearch:
                                response = _apiGoods.POSearch(JsonConvert.DeserializeObject<BaseSearchRequest>(input),
                                    out httpStatusCode);
                                break;
                        }

                        #endregion

                        break;

                }

            }
            catch (Exception ex)
            {
                httpStatusCode = HttpStatusCode.InternalServerError;
                response.Code = ApiStatusCode.SystemError;
                response.Message = response.Code.Text();

                CommonLogger.DefaultLogger.Error("ApiAdapter", ex);
            }

            return Request.CreateResponse(httpStatusCode, response, _jsonFormater);
        }

        #endregion
    }
}