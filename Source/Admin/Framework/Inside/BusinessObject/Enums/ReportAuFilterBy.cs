using System.ComponentModel;

namespace BusinessObject.Enums
{
    public enum ReportAuFilterBy
    {
        [Description("All")]
        All,
        [Description("OpenProviderID")]
        OpenProviderID,
        [Description("PlatformID")]
        PlatformID,
        [Description("ChannelID")]
        ChannelID
    }
}