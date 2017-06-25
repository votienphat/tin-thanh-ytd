using System.ComponentModel;

namespace VanTaiSystem.Modules.Base.Resources
{
    public class BaseResource
    {
        public class ApiStatusCode
        {
            public const string Failed = "Thất bại";
            public const string Success = "Thành công";
            public const string InvalidInput = "Thông tin không hợp lệ";
            public const string InvalidInputMaxMin = "Chiều dài ký tự không hợp lệ";

            public const string NotLogin = "Chưa đăng nhập";
            public const string Unauthorized = "Không có quyền";
            public const string UnexistedApi = "API không tồn tại";
            public const string InvalidSign = "Chữ ký không hợp lệ";
            public const string TokenExpire = "Token hết hạn";
            public const string SystemError = "Hệ thống bảo trì";
        }
    }

}