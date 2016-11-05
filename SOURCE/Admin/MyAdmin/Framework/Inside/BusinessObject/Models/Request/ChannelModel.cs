namespace BusinessObject.Models.Request
{
    //public class ChannelModel
    //{
    //    public int ChannelId
    //    {
    //        get;
    //        set;
    //    }

    //    public string ChannelName
    //    {
    //        get;
    //        set;
    //    }
    //}

    public class PlatformModel
    {
        public int PlatformId { get; set; }
        public string PlatformName { get; set; }
    }
    public class VersionModel
    {
        public string VersionId { get; set; }
    }

    /// <summary>
    /// TanPVD: 2015-03-27
    /// </summary>
    public class CampaignModel
    {
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }
    }
}
