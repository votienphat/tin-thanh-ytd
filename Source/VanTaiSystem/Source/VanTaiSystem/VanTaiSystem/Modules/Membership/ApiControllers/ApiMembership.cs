using System.Net;
using BusinessObject.MembershipModule.Contract;
using BusinessObject.MembershipModule.Enums;
using BussinessObject.MembershipModule.Contract;
using VanTaiSystem.Helper;
using MyUtility.Extensions;
using VanTaiSystem.Modules.Base.Enums;
using VanTaiSystem.Modules.Base.Models;
using VanTaiSystem.Modules.Membership.Models;
using WebGrease.Css.Ast;

namespace VanTaiSystem.Modules.Membership.ApiControllers
{
    public class ApiMembership : IApiMembership
    {
        private readonly IMemberBusiness _memberBusiness;

        public ApiMembership(IMemberBusiness memberBusiness)
        {
            _memberBusiness = memberBusiness;
        }

        public ApiBaseResponse Login(ApiLoginRequest request, out HttpStatusCode httpStatusCode)
        {
            var response = new ApiBaseResponse();
            httpStatusCode = HttpStatusCode.OK;

            #region Validate input

            if (!ApiHelper.IsModelStateValid(request))
            {
                response.Code = ApiStatusCode.InvalidInput;
                response.Message = response.Code.Text();
                //httpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var validateResult = SessionManager.ValidateLoginSign(request, request.Username, request.Password);
            if (validateResult != ApiStatusCode.Success)
            {
                response.Code = validateResult;
                response.Message = response.Code.Text();
                //httpStatusCode = HttpStatusCode.Unauthorized;
                return response;
            }

            #endregion

            #region Action

            var result = _memberBusiness.Login(request.Username, request.Password);

            if (result.Result != MembershipCode.Success)
            {
                response.Code = ApiStatusCode.Failed;
                response.Message = response.Code.Text();
                //httpStatusCode = HttpStatusCode.NoContent;
                return response;
            }

            //response.Data = new
            //{
            //    UserId = result.User.ID,
            //    UserName = result.User.FullName,
            //    Email = result.User.Email,
            //    Token = result.User.TokenID,
            //    LastLogin = result.User.LastLoginDate,

            //};

            

            #endregion

            response.Code = ApiStatusCode.Success;
            response.Message = response.Code.Text();
            httpStatusCode = HttpStatusCode.OK;
            return response;
        }

        public ApiBaseResponse UpdatePassword(ApiUpdatePasswordRequest request, out HttpStatusCode httpStatusCode)
        {
            var response = new ApiBaseResponse();
            httpStatusCode = HttpStatusCode.OK;

            #region Validate input and authorize

            if (!ApiHelper.IsModelStateValid(request))
            {
                response.Code = ApiStatusCode.InvalidInput;
                response.Message = response.Code.Text();
                //httpStatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var validateResult = SessionManager.ValidateSign(request, request.UserId, request.OldPassword,
                request.NewPassword);
            if (validateResult != ApiStatusCode.Success)
            {
                response.Code = ApiStatusCode.InvalidSign;
                response.Message = response.Code.Text();
                //httpStatusCode = HttpStatusCode.Unauthorized;
                return response;
            }

            #endregion

            #region Action

            //var result = _memberBusiness.UpdatePassword(request.UserId, request.OldPassword, request.NewPassword);

            //switch (result.Result)
            //{
            //    case MembershipCode.InvalidOldPassword:
            //        response.Code = ApiStatusCode.InvalidOldPassword;
            //        //httpStatusCode = HttpStatusCode.NoContent;
            //        break;
            //    case MembershipCode.Success:
            //        response.Code = ApiStatusCode.Success;
            //        //httpStatusCode = HttpStatusCode.OK;
            //        break;
            //    default:
            //        response.Code = ApiStatusCode.Failed;
            //        //httpStatusCode = HttpStatusCode.NoContent;
            //        break;
            //}

            #endregion

            response.Message = response.Code.Text();
            return response;
        }
    }
}