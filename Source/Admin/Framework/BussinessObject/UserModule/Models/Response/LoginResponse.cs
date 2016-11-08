using BussinessObject.UserModule.Enums;

namespace BussinessObject.UserModule.Models.Response
{
    /// <summary>
    /// Định nghĩa các thông số trả về khi đăng nhập
    /// </summary>
    /// <history>
    /// 12/01/2016 PhatVT: Tạo mới
    /// </history>
    public class LoginResponse
    {
        /// <summary>
        /// Đối tượng thông tin user, nếu đăng nhập thành công
        /// </summary>
        public UserInfo Account { get; set; }

        /// <summary>
        /// Trạng thái trả về của method
        /// </summary>
        public LoginCodeEnum CodeEnum { get; set; }

        public LoginResponse()
        {
            CodeEnum = LoginCodeEnum.SystemError;
        }
    }
}
