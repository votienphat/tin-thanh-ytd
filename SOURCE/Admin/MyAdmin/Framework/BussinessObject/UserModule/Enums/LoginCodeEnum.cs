using System.ComponentModel;

namespace BussinessObject.UserModule.Enums
{
    /// <summary>
    /// Định nghĩa kết quả login
    /// <para>Author: PhatVT</para>
    /// <para>Created Date: 17/12/2014</para>
    /// </summary>
    public enum LoginCodeEnum
    {
        /// <summary>
        /// Thành công
        /// </summary>
        [Description("Thành công")]
        Success = 0,

        /// <summary>
        /// Email hoặc mật khẩu không chính xác
        /// </summary>
        [Description("Username hoặc mật khẩu không chính xác")]
        PassOrEmailError = 1,

        /// <summary>
        /// Tài khoản đã bị khóa
        /// </summary>
        [Description("Tài khoản đã bị khóa")]
        BanNick = 2,

        /// <summary>
        /// Tài khoản chưa được kích hoạt
        /// </summary>
        [Description("Tài khoản chưa được kích hoạt")]
        UserIsNotActive = 4,

        /// <summary>
        /// Tài khoản không tồn tại
        /// </summary>
        [Description("Tài khoản không tồn tại")]
        NonExistsAccount = 5,

        /// <summary>
        /// Lỗi hệ thống
        /// </summary>
        [Description("Lỗi hệ thống")]
        SystemError = 9999
    }
}
