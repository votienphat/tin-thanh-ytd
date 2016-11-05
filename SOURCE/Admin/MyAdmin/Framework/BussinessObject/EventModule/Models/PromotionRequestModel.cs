
namespace BussinessObject.EventModule.Models
{
    public class PromotionRequestModel
    {
        public int RunOn { get; set; }

        public int UserId { get; set; }

        public int UserFriendId { get; set; }

        public string Description { get; set; }

        public int LoginType { get; set; }

        public int ClientTarget { get; set; }

        public bool IsMobile { get; set; }

        public int Amount { get; set; }

        public string IpRequest { get; set; }

        public int ChanelId { get; set; }

        public int PlatformId { get; set; }

        public string GameVersion { get; set; }

        public decimal Gold { get; set; }

        public int CardType { get; set; }
    }
}
