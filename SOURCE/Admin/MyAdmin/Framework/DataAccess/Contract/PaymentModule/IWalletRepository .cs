using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Contract.PaymentModule
{
    public interface IWalletRepository : IDaoRepository<Wallet>
    {
        decimal UpdateCoin(int userId, int walletType, decimal coin);

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 21/12/2015</para>
        /// <para>lay coin hien tai cua user</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        decimal GetCoin(int userId);

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 21/12/2015</para>
        /// <para>cập nhật gold cho user và ghi log</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="goldTrans"></param>
        /// <param name="reasonId"></param>
        /// <param name="description"></param>
        /// <param name="actionId"></param>
        /// <param name="isMobile"></param>
        /// <param name="loginType"></param>
        /// <param name="clientTarget"></param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        int Wallet_AddOrSubtractGoldUser(int userId, decimal goldTrans, int reasonId, string description,
            int actionId,
            bool isMobile, int loginType, int clientTarget, int adminId = 0);

        Out_Wallet_ExchangeCoin_ToCard_Result ExchangeToCard(int userId, int cardTypeId, int paymentType, decimal cardAmount, int walletType);

        void DepositOrWithdrawCoffer(int userId, decimal goldTransfer, int actionType,  string ipAddress,
            int platformId, string hardwareId, out int result, out decimal walletNow, out decimal cofferNow);

        Out_Wallet_ExchangeCheckEnoughGold_Result CheckEnoughGold(int userId, int cardType, decimal amount);

        Out_Wallet_SubtractGoldExchangeCard_Result SubtractGoldExchangeCard(int userId, decimal goldTrans);


    }
}
