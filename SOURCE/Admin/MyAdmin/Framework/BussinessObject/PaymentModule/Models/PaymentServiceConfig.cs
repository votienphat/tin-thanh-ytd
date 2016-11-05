using Newtonsoft.Json;

namespace BussinessObject.PaymentModule.Models
{
    public class PaymentServiceConfig
    {
        public bool IsTest { get; set; }
        public string Key { get; set; }
        public string TestServiceString { get; set; }

    }

    public class InAppPurchaseAppleReceiptModel
    {
        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("purchase-info")]
        public string PurchaseInfo { get; set; }

        [JsonProperty("environment")]
        public string Environment { get; set; }

        [JsonProperty("pod")]
        public string Pod { get; set; }

        [JsonProperty("signing-status")]
        public string SigningStatus { get; set; }
    }
}