using System;

namespace EntitiesObject.Models.ReportModel
{
    public class ReportNewDeviceModel
    {
        public int ChannelId { get; set; }

        public int CampaignId { get; set; }

        public int All { get; set; }

        public int IOS { get; set; }

        public int Android { get; set; }

        public int DateType { get; set; }

        public DateTime DateReport { get; set; }

        public ReportNewDeviceModel()
        {
            ChannelId = 0;
            CampaignId = 0;
            All = 0;
            IOS = 0;
            Android = 0;
            DateType = 0;
        }

        public ReportNewDeviceModel(DateTime date)
        {
            ChannelId = 0;
            CampaignId = 0;
            All = 0;
            IOS = 0;
            Android = 0;
            DateType = 0;
            DateReport = date;
        }
    }
}
