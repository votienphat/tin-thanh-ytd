using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Contract.UserModule
{
    public interface IWalletStarRepository : IDaoRepository<WalletStar>
    {
        Out_WalletStar_ExchangeCheckEnoughGold_Result WalletStarCheckEnoughGold(int userId, int cardType, decimal cardAmount);

        int UpdateStar(int userId, decimal goldTrans, int reasonCoinId);
    }
}