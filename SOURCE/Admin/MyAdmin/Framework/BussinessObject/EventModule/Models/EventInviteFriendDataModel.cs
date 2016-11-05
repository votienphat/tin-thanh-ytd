
using System.Collections.Generic;
namespace BussinessObject.EventModule.Models
{
    public class KhuyenMaiNapTienLanDau
    {
        //public int DayRegister { get; set; }
        public int NumberCardPromotion { get; set; }
        public int Percent { get; set; }
        public int MinAmount { get; set; }
        public int MaxAmount { get; set; }
    }

    public class EventInviteFriendDataModel
    {
        public decimal Gold { get; set; }
    }

    public class ConfigEventInviteFriendModel
    {
        public ConfigEventInviteFriendModel ()
        {
            Gold = "0";
            Status = false;
        }
        public bool Status { get; set; }
        public string Gold { get; set; }
        public string Title { get; set; }

        public string DateBegin { get; set; }
        public string DateEnd { get; set; }
    }

    public class ItemAwardValueCard
    {
        public int CardAmount { get; set; }
        public double PercentValue { get; set; }
    }

    public class ConfigValueCard
    {
        public List<ItemAwardValueCard> MenhGiaModels { get; set; }
    }
}
