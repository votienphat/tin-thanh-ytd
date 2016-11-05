
using System.ComponentModel.DataAnnotations;

namespace MyAdmin.Models.GoldAdmin
{
    public class GoldAdminModel
    {
        public int Index { get; set; }

        [Required(ErrorMessage = "Phải nhập UserId.")]
        [StringLength(400, ErrorMessage = "Danh sách UserId tối đa 400 ký tự.")]
        public string UserIds { get; set; }

        [Required(ErrorMessage = "Phải nhập UserId.")]
        public int UserId { get; set; }

        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Phải chọn lý do.")]
        [Range(1, 10000000, ErrorMessage = "Phải chọn lý do.")]
        public int ReasonId { get; set; }

        public string ReasonName { get; set; }

        [Range(0, 10000000, ErrorMessage = "Số xu phải < 10.000.000")]
        [Required(ErrorMessage = "Phải nhập số xu.")]
        public decimal Gold { get; set; }

        public string Note { get; set; }

        public string Message { get; set; }

        public bool IsAdd { get; set; }

        public bool IsAddToWallet { get; set; }

        public decimal CoinVi { get; set; }

        public decimal CoinRuong { get; set; }

        public bool IsError { get; set; }

        public string AmountGoldStr { get; set; }

        public string Avatar { get; set; }

        public GoldAdminModel()
        {
            Index = 0;
            UserIds = string.Empty;
            UserId = 0;
            DisplayName = string.Empty;
            ReasonId = 0;
            Gold = 0;
            Note = string.Empty;
            Message = string.Empty;
            IsAdd = false;
            IsAddToWallet = false;
            CoinVi = 0;
            CoinRuong = 0;

            IsError = false;
            AmountGoldStr = string.Empty;
            Avatar = string.Empty;
        }
    }
}