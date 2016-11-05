namespace BussinessObject.PaymentModule.Enums
{
    /// <summary>
    /// Enum CardType
    /// </summary>
    public enum CreditCardTypeEum
    {
        GoogleStore = 1000,
        AppleStore = 1001,
        WindowStore = 1003
    }

    /// <summary>
    /// Enum ket qua nap tien
    /// </summary>
    public enum ChargeGoldResultEnum
    {
        Error = 0,
        Success = 1,
        Pending = 2,
        SuccessBefore = 3,
        FailFromIap = 4,
        IapTransactionNotExists = 5,
        IapPending = 6,
        TransactionNotExists = 7,
        SuccessButAddGoldFail = 8,
    }

    /// <summary>
    /// Enum ket qua log giao dich
    /// </summary>
    public enum DepositLogGoogleResultEnum
    {
        SqlError = -2,
        NotExists = -1,
        TransactionError = 0,
        TransactionSuccess = 1,
        TransactionPending = 2
    }

    /// <summary>
    /// Enum kiem tra ket qua nap tien IAP
    /// </summary>
    public enum DepositCreditStatusEnum
    {
        ThanhCong = 1,
        DangChoXuLy = 2,
        ThatBai = 3,
        KhongLayDuocThongTinGiaoDich = 4,
        ThongTinGiaoDichKhongDung = 5,
        KhongTruyCapDuocGoogle = 6,
        KhongTruyCapDuocApple = 7,
    }

    /// <summary>
    /// Enum trang thai nap tien cua IAP
    /// </summary>
    public enum PurchaseStateGoogleEnum
    {
        Purchased = 0,
        Cancelled = 1
    }

    /// <summary>
    /// Enum phan loai san pham
    /// </summary>
    public enum CreditProductTypeEnum
    {
        Unknown = 0,
        Vnpt = 1,
        AndroidStore = 2,
        AppleStore = 3,
        WindowStore = 4
    }
}