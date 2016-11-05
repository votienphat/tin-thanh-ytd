using System.Data.Entity.Core.Objects;
using System.Linq;
using DataAccess.Contract.PaymentModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.PaymentModule
{
    public class WalletRepository : DaoRepository<UserEntities, Wallet>, IWalletRepository
    {

        public decimal UpdateCoin(int userId, int walletType, decimal coin)
        {
            var result = Uow.Context.Out_Wallet_UpdateCoin(userId, walletType, coin).FirstOrDefault();
            return result.GetValueOrDefault(0);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 21/12/2015</para>
        /// <para>lay coin hien tai cua user</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public decimal GetCoin(int userId)
        {
            return Uow.Context.Out_Account_GetCoin(userId).FirstOrDefault().GetValueOrDefault(0);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 21/12/2015</para>
        /// <para>cập nhật gold cho user và ghi log</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="goldTrans"></param>
        /// <param name="description"></param>
        /// <param name="actionId"></param>
        /// <param name="isMobile"></param>
        /// <param name="loginType"></param>
        /// <param name="clientTarget"></param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        public int Wallet_AddOrSubtractGoldUser(int userId, decimal goldTrans, int reasonId, string description, int actionId,
            bool isMobile, int loginType, int clientTarget, int adminId = 0)
        {
            return
                Uow.Context.Out_Wallet_AddOrSubtractGoldUser(userId, adminId, goldTrans, reasonId, description, actionId,
                    isMobile, loginType, clientTarget).FirstOrDefault().GetValueOrDefault(0);
        }


        public Out_Wallet_ExchangeCoin_ToCard_Result ExchangeToCard(int userId, int cardTypeId, int paymentType, decimal cardAmount, int walletType)
        {
            var result = Uow.Context.Out_Wallet_ExchangeCoin_ToCard(userId, cardTypeId, paymentType, cardAmount, walletType).FirstOrDefault();
            return result;
        }


        public void DepositOrWithdrawCoffer(int userId, decimal goldTransfer, int actionType, string ipAddress, int platformId, string hardwareId, out int result, out decimal walletNow, out decimal cofferNow)
        {
            result = 0;
            var outResult = new ObjectParameter("Result", result);
            walletNow = 0;
            var outWalletNow = new ObjectParameter("WalletNow", walletNow);
            cofferNow = 0;
            var outCofferNow = new ObjectParameter("CofferNow", cofferNow);
            var tem = Uow.Context.Out_Wallet_DebositOrWithdrawCoffer(userId, goldTransfer, actionType, ipAddress, platformId, hardwareId, outResult, outWalletNow, outCofferNow).FirstOrDefault();

            result = outResult.Value == null ? 0 : int.Parse(outResult.Value.ToString());
            walletNow = outWalletNow.Value == null ? 0 : decimal.Parse(outWalletNow.Value.ToString());
            cofferNow = outCofferNow.Value == null ? 0 : decimal.Parse(outCofferNow.Value.ToString());
        }


        public Out_Wallet_ExchangeCheckEnoughGold_Result CheckEnoughGold(int userId, int cardType, decimal amount)
        {
            return Uow.Context.Out_Wallet_ExchangeCheckEnoughGold(userId, cardType, amount).FirstOrDefault();
        }

        public Out_Wallet_SubtractGoldExchangeCard_Result SubtractGoldExchangeCard(int userId, decimal goldTrans)
        {

            return Uow.Context.Out_Wallet_SubtractGoldExchangeCard(userId, goldTrans).FirstOrDefault();
        }
    }
}
