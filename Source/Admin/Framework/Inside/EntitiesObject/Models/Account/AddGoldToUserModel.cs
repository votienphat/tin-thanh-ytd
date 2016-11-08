
namespace EntitiesObject.Models.Account
{
    public class AddGoldToUserModel
    {
        public int UserId { get; set; }

        public string DisplayName { get; set; }

        public int PubUserId { get; set; }

        public decimal CoinVi { get; set; }

        public decimal CoinRuong { get; set; }

        public string Avatar { get; set; }

        public int LevelUser { get; set; }

        public AddGoldToUserModel()
        {
            UserId = 0;
            DisplayName = string.Empty;
            PubUserId = 0;
            CoinVi = 0;
            CoinRuong = 0;
            Avatar = string.Empty;
            LevelUser = 0;
        }
    }
}
