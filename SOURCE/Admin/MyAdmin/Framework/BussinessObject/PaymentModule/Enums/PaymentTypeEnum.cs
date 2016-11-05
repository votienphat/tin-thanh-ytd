using System.ComponentModel;

namespace BussinessObject.PaymentModule.Enums
{
    public enum PaymentTypeEnum
    {
        [Description("Card")]
        Card = 1,

        [Description("Google IAP")]
        GoogleIap = 2,
        [Description("Apple IAP")]
        IosIap = 3,

        [Description("WindowsPhone IAP")]
        WdpIap = 4,
    }

    public enum ItemPaymentTypeEnum
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

        [Description("IAP")]
        GoogleIap = 6,
    }
}
