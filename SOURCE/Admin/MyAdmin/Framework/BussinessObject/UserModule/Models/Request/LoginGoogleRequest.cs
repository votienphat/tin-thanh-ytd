namespace BussinessObject.UserModule.Models.Request
{
    /// <summary>
    /// Thông tin cần để xác thực login
    /// </summary>
    /// <history>
    /// 12/01/2016 PhatVT: Tạo mới
    /// </history>
    public class LoginGoogleRequest
    {
        public string ConfirmLink { get; set; }
        public string AccessToken { get; set; }
    }
}
