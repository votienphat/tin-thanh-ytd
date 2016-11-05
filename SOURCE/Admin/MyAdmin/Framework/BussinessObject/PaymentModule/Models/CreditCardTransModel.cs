using System.Collections.Generic;
using BussinessObject.Enums;
using BussinessObject.PaymentModule.Enums;
using BussinessObject.UserModule.Enums;

namespace BussinessObject.PaymentModule.Models
{
    /*
    class CreditCardTransModel
    {
    }
    */

    public class DepositLogGoogleModel
    {
        public int UserId { get; set; }

        public string PinCard { get; set; }

        public string TransId { get; set; }

        public string CardTypeString { get; set; }

        public decimal CardAmount { get; set; }

        public string Description { get; set; }

        public decimal CurrentCoin { get; set; }

        public decimal CoinTrans { get; set; }

        public string Token { get; set; }

        public string TargetIp { get; set; }

        public CreditCardTypeEum CardType { get; set; }

        public ChargeGoldResultEnum Result { get; set; }

        public DepositGoogleModel GoogleModel { get; set; }
        public int ChannelID { get; set; }
        public string VersionGame { get; set; }
        public PlatformIdEnum PlatformId { get; set; }
    }

    public class DepositGoogleModel
    {
        public string PackageName { get; set; }

        public string ProductId { get; set; }

        public string Token { get; set; }
    }

    #region IAP moi

    /// <summary>
    /// Model dung cho IAP
    /// </summary>
    public class DepositInAppPurchaseModel
    {
        public int UserId { get; set; }

        public string PackageName { get; set; }

        public string ProductId { get; set; }

        public string Token { get; set; }

        public bool IsTest { get; set; }

        public CreditCardTypeEum InAppPurchaseType { get; set; }

        public string IpClient { get; set; }

        public LoginTypeEnum ClientTarget { get; set; }

        /// <summary>
        /// So tien se cong cho user
        /// </summary>
        public decimal ProductValue { get; set; }

        /// <summary>
        /// So tien se cong cho user
        /// </summary>
        public decimal ProductAmount { get; set; }

        /// <summary>
        /// Platform thiet bi
        /// </summary>
        public PlatformIdEnum PlatformId { get; set; }

        /// <summary>
        /// Imei thiet bi
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// Ma phan cung
        /// </summary>
        public string HardwareId { get; set; }

        /// <summary>
        /// Kênh
        /// </summary>
        public int ChannelID { get; set; }

        /// <summary>
        /// Version Game
        /// </summary>
        public string VersionGame { get; set; }

        #region google

        public string GooglePinCard
        {
            get { return UserId + "|" + PackageName + "|" + ProductId + "|" + Token; }
        }

        /// <summary>
        /// Link goi de kiem tra giao dich
        /// </summary>
        public string UrlRequest { get; set; }

        public string ClientId { get; set; }

        /// <summary>
        /// Url refresh token
        /// </summary>
        public string UrlRefreshToken { get; set; }

        public string RefreshToken { get; set; }

        public string ClientSecret { get; set; }

        #endregion
    }

    public class DepositInAppPurchaseResponseModel
    {
        public ChargeGoldResultEnum StatusCode { get; set; }

        /// <summary>
        /// Gold hien tai cua user
        /// </summary>
        public decimal CoinUser { get; set; }

        /// <summary>
        /// Gold nap them
        /// </summary>
        public decimal CoinDeposit { get; set; }

        public string ProductId { get; set; }
    }

    /// <summary>
    /// Model refresh token cua google IAP
    /// </summary>
    public class GoogleApiAccessToken
    {
        public string access_token { get; set; }

        public string token_type { get; set; }

        public int expires_in { get; set; }
    }

    /// <summary>
    /// Model ket qua kiem tra giao dich IAP cua google
    /// </summary>
    public class DepositGoogleResultModel
    {
        public string Kind { get; set; }

        public long PurchaseTimeMillis { get; set; }

        public int PurchaseState { get; set; }

        public int ConsumptionState { get; set; }

        public string DeveloperPayload { get; set; }

        public string product_id { get; set; }
    }


    public class DepositAppleResultModel
    {
        /// <summary>
        /// status code
        /// Either 0 if the receipt is valid, or one of the error codes
        /// For iOS 6 style transaction receipts, the status code reflects the status of the specific transaction’s receipt.
        /// For iOS 7 style app receipts, the status code is reflects the status of the app receipt as a whole.
        /// For example, if you send a valid app receipt that contains an expired subscription, the response is 0 because the receipt as a whole is valid.
        /// <link>https://developer.apple.com/library/ios/releasenotes/General/ValidateAppStoreReceipt/Chapters/ValidateRemotely.html#//apple_ref/doc/uid/TP40010573-CH104-SW5</link>
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// chuỗi JSON
        /// A JSON representation of the receipt that was sent for verification. For information about keys found in a receipt
        /// <link>https://developer.apple.com/library/ios/releasenotes/General/ValidateAppStoreReceipt/Chapters/ReceiptFields.html#//apple_ref/doc/uid/TP40010573-CH106-SW1</link>
        /// </summary>
        public ReceiptAppleDetailModel receipt { get; set; }

        /// <summary>
        /// Only returned for iOS 6 style transaction receipts for auto-renewable subscriptions. The base-64 encoded transaction receipt for the most recent renewal.
        /// </summary>
        public string latest_receipt { get; set; }

        /// <summary>
        /// Only returned for iOS 6 style transaction receipts for auto-renewable subscriptions. The JSON representation of the receipt for the most recent renewal.
        /// </summary>
        public string latest_receipt_info { get; set; }
    }

    public class ReceiptAppleDetailModel
    {
        /*
            "original_purchase_date_pst":"2014-11-10 23:42:09 America/Los_Angeles",
			"purchase_date_ms":"1415691729847",
			"unique_identifier":"b8ed269c71fce1b4c3407fd2f50457445b2f0a07",
			"original_transaction_id":"1000000131178371",
			"bvrs":"1.1.0",
			"transaction_id":"1000000131178371",
			"quantity":"1",
			"unique_vendor_identifier":"66186060-B752-4F25-B308-16E35646C425",
			"item_id":"939933308",
			"product_id":"com.hoiquan.hoiquan52.buytest1",
			"purchase_date":"2014-11-11 07:42:09 Etc/GMT",
			"original_purchase_date":"2014-11-11 07:42:09 Etc/GMT",
			"purchase_date_pst":"2014-11-10 23:42:09 America/Los_Angeles",
			"bid":"com.hoiquan.hoiquan52",
			"original_purchase_date_ms":"1415691729847"
         */

        public string original_purchase_date_pst { get; set; }
        public string purchase_date_ms { get; set; }
        public string unique_identifier { get; set; }

        public string original_transaction_id { get; set; }
        public string bvrs { get; set; }
        public string transaction_id { get; set; }

        public string quantity { get; set; }
        public string unique_vendor_identifier { get; set; }
        public string item_id { get; set; }

        public string product_id { get; set; }
        public string purchase_date { get; set; }
        public string original_purchase_date { get; set; }

        public string purchase_date_pst { get; set; }
        public string bid { get; set; }
        public string original_purchase_date_ms { get; set; }
    }
    #endregion
}
