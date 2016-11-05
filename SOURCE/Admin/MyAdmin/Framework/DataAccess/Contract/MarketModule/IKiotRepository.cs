using System.Collections.Generic;
using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Contract.MarketModule
{
    public interface IKiotRepository : IDaoRepository<Kiot>
    {        
        List<Out_Kiot_GetKiotUser_Result> GetKiotUser(int intUserID);

        List<Out_Kiot_GetAllKiot_V3_Result> GetAllKiot(int userId);

        List<Out_Kiot_GetAllItemUser_Result> GetAllItemUser(int intUserID);

        int SaveItemGameUser(int intUserID, int intKiotID, int intItemID, int intQuantity, decimal decAmount,
            decimal decPricePromotion, int intReasonID);

        Out_Kiot_BuyItem_Result BuyItemInMarket(int userId, int salerId, int kiotId, int itemId, float tax);

        int RemoveItemGameUser(int intUserID, int intKiotID, int intItemID, int intReasonID);

        int UpdatePromotion(int intUserID, int intKiotID, int intItemID, int intReasonID);

        List<Out_Kiot_GetItemSold_Result> GetItemSold(int intUserID, int intItemID);

        List<Out_Kiot_GetRandomItemOfFriend_Result> GetRandomItemOfFriend(int intUserID, int intTopHasPromotion, int intTopNoPromotion, string strKiotUserID);

        int BuyKiot(int userId, int kiotId);

        int SendMessage(int senderId, int receiverId, string messageContent, int messageType, int status,
            string ipAddress, int platformId, string hardwareId, bool isSystem, int parentId);
    }
}
