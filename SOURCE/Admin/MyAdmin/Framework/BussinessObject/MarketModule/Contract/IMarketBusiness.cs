using System.Collections.Generic;
using EntitiesObject.Entities.UserEntities;

namespace BussinessObject.MarketModule.Contract
{
    public interface IMarketBusiness
    {
        List<Out_Kiot_GetKiotUser_Result> GetKiotUser(int intUserID);

        List<Out_Kiot_GetAllKiot_V3_Result> GetAllKiot(int userId);

        List<Out_Kiot_GetAllItemUser_Result> GetAllItemUser(int intUserID);

        int SaveItemGameUser(int intUserID, int intKiotID, int intItemID, int intQuantity, decimal decAmount,
            decimal decPricePromotion, int intReasonID);

        Out_Kiot_BuyItem_Result BuyItemInMarket(int userId, int salerId, int kiotId, int itemId, float tax);

        int RemoveItemGameUser(int intUserID, int intKiotID, int intItemID, int intReasonID);

        int UpdatePromotion(int intUserID, int intKiotID, int intItemID, int intReasonID);

        List<Out_Kiot_GetItemSold_Result> GetItemSold(int intUserID = 0, int intItemID = 0);

        List<Out_Kiot_GetRandomItemOfFriend_Result> GetRandomItemOfFriend(int intUserID = 0, int intTopHasPromotion = 0, int intTopNoPromotion = 0, string strKiotUserID = "");

        int BuyKiot(int userId, int kiotId);

        int SendMessage(int senderId, int receiverId, string messageContent, int messageType, int status,
            string ipAddress, int platformId, string hardwareId, bool isSystem, int parentId);
    }
}
