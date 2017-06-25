
using System.ComponentModel;
using VanTaiSystem.Modules.Base.Resources;
using VanTaiSystem.Modules.Goods.Resources;

namespace VanTaiSystem.Modules.Base.Enums
{
    public enum ApiStatusCode
    {
        #region Base Code

        /// <summary>
        /// Thất bại
        /// </summary>
        [Description(BaseResource.ApiStatusCode.Failed)]
        Failed = 0,

        /// <summary>
        /// Thành công
        /// </summary>
        [Description(BaseResource.ApiStatusCode.Success)]
        Success = 1,

        /// <summary>
        /// Lỗi input
        /// </summary>
        [Description(BaseResource.ApiStatusCode.InvalidInput)]
        InvalidInput = 2,

        /// <summary>
        /// Chưa đăng nhập
        /// </summary>
        [Description(BaseResource.ApiStatusCode.NotLogin)]
        NotLogin = 1000,

        /// <summary>
        /// Không có quyền
        /// </summary>
        [Description(BaseResource.ApiStatusCode.Unauthorized)]
        Unauthorized = 1001,

        /// <summary>
        /// API không tồn tại
        /// </summary>
        [Description(BaseResource.ApiStatusCode.UnexistedApi)]
        UnexistedApi = 1002,

        /// <summary>
        /// Chữ ký không hợp lệ
        /// </summary>
        [Description(BaseResource.ApiStatusCode.InvalidSign)]
        InvalidSign = 1003,

        /// <summary>
        /// Token hết hạn
        /// </summary>
        [Description(BaseResource.ApiStatusCode.TokenExpire)]
        TokenExpire = 1004,

        /// <summary>
        /// Lỗi hệ thống
        /// </summary>
        [Description(BaseResource.ApiStatusCode.SystemError)]
        SystemError = 9999,

        #endregion

        #region Membership Code | 10 => 99

        ///// <summary>
        ///// Mật khẩu cũ không hợp lệ
        ///// </summary>
        //[Description(GoodsResources.ApiStatusCode.InvalidOldPassword)]
        //InvalidOldPassword = 10,

        #endregion
    }
}
