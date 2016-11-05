using System.ComponentModel;

namespace BussinessObject.Enums
{
    public enum HqCardTypeEnum
    {
        [Description("VMS")]
        Mobi = 1,
        [Description("VNP")]
        Vina = 3,
        [Description("VTT")]
        Viettel = 2,
        [Description("BIT")]
        Bit = 4
    }
}