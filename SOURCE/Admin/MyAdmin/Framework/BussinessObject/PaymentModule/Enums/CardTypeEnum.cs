using System.ComponentModel;

namespace BussinessObject.PaymentModule.Enums
{
    public enum CardTypeEnum
    {
        [Description("Mobifone")]
        Mobifone = 1,

        [Description("Viettel")]
        Viettel = 2,

        [Description("Vinaphone")]
        Vinaphone = 3,

        [Description("Gate")]
        Gate = 5,

        [Description("Bit")]
        Bit = 4,
    }
}
