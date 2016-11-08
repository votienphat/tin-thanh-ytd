using BussinessObject.UserModule.Enums;

namespace BussinessObject.UserModule.Models.Request
{
    /// <summary>
    /// Định nghĩa các thông số cần để đăng nhập.
    /// Tùy vào loại đăng nhập thì có những thông số khác nhau
    /// </summary>
    /// <history>
    /// 12/01/2016 PhatVT: Tạo mới
    /// </history>
    public class LoginRequest
    {
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string Ip { get; set; }

        public LoginFacebookRequest FacebookRequest { get; set; }
        public LoginGoogleRequest GoogleRequest { get; set; }

        public LoginTypeEnum LoginFrom { get; set; }
        public OpenProviderIdEnum OpenProviderId { get; set; }

        public LoginRequest()
        {
            // Mặc định là login từ web
            LoginFrom = LoginTypeEnum.Web;

            // Mặc định là login thông thư
            OpenProviderId = OpenProviderIdEnum.Normal;
        }
    }
}
