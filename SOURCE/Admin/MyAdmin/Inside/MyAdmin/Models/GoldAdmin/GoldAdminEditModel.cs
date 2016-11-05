
using System.ComponentModel.DataAnnotations;

namespace MyAdmin.Models.GoldAdmin
{
    public class GoldAdminEditModel
    {
        public int UserId1 { get; set; }

        [Required(ErrorMessage = "Phải chọn lý do.")]
        [Range(1, 10000000, ErrorMessage = "Phải chọn lý do.")]
        public int ReasonId1 { get; set; }

        public string ReasonName1 { get; set; }

        [Range(0, 10000000, ErrorMessage = "Số xu phải < 10.000.000")]
        [Required(ErrorMessage = "Phải nhập số xu.")]
        public decimal Gold1 { get; set; }

        public string Note1 { get; set; }

        public string Message1 { get; set; }

        public bool IsAddToWallet1 { get; set; }

        public string DisplayName1 { get; set; }

        public string Avatar1 { get; set; }

        public GoldAdminEditModel()
        {
            UserId1 = 0;
            ReasonName1 = string.Empty;
            ReasonId1 = 0;
            Gold1 = 0;
            Note1 = string.Empty;
            Message1 = string.Empty;
            IsAddToWallet1 = false;
            DisplayName1 = string.Empty;
            Avatar1 = string.Empty;
        }
    }
}