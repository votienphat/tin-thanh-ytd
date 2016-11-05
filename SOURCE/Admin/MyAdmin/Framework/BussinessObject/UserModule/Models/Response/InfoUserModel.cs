using System;

namespace BussinessObject.UserModule.Models.Response
{
    public class InfoUserModel
    {
        //Thông tin: hiện popup thông tin của người chơi đó: Level, ID, Điểm kinh nghiệm, Tên hiển thị, Số Xu trong ví. Lưu ý: không show số CMND và số điện thoại của người kia.
        public int UserId { get; set; }
        public string DisplayName { get; set; }
        public string LevelUser { get; set; }
        public string ExpUser { get; set; }
        public string GoldUser { get; set; }
        public string ExperienceTarget { get; set; }
    }
}
