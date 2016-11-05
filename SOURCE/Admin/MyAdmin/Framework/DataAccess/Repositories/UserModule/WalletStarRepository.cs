using System.Linq;
using DataAccess.Contract.UserModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.UserModule
{
    public class WalletStarRepository : DaoRepository<UserEntities, WalletStar>, IWalletStarRepository
    {

        public Out_WalletStar_ExchangeCheckEnoughGold_Result WalletStarCheckEnoughGold(int userId, int cardType, decimal cardAmount)
        {
            return Uow.Context.Out_WalletStar_ExchangeCheckEnoughGold(userId, cardType, cardAmount).FirstOrDefault();
        }


        public int UpdateStar(int userId, decimal goldTrans, int reasonCoinId)
        {
            return Uow.Context.Out_WalletStar_UpdateGold(userId, goldTrans, reasonCoinId).FirstOrDefault().GetValueOrDefault();
        }
    }
}