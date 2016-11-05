using System.ComponentModel;

namespace BusinessObject.Enums
{
    public enum MyConfigKey
    {
        [Description("PaymentCard")]
        PaymentCard,

        [Description("ExchangeCard")]
        ExchangeCard,

        [Description("PaymentGoogleAPI")]
        // ReSharper disable once InconsistentNaming
        PaymentGoogleIAP,

        [Description("ConfigAPI")]
        ConfigApi,

        [Description("AutoExchange")]
        AutoExchange,

    }

    public enum ConfigTypeCallApi
    {
        [Description("POST")]
        POST,

        [Description("GET")]
        GET,
    }
}
