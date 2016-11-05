using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using DataAccess.Contract.MarketModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.MarketModule
{
    public class KiotRepository : DaoRepository<UserEntities, Kiot>, IKiotRepository
    {
        public List<Out_Kiot_GetKiotUser_Result> GetKiotUser(int intUserID)
        {
            return Uow.Context.Out_Kiot_GetKiotUser(intUserID).ToList();
        }

        public List<Out_Kiot_GetAllKiot_V3_Result> GetAllKiot(int userId)
        {
            return Uow.Context.Out_Kiot_GetAllKiot_V3(userId).ToList();
        }

        public List<Out_Kiot_GetAllItemUser_Result> GetAllItemUser(int intUserID)
        {
            return Uow.Context.Out_Kiot_GetAllItemUser(intUserID).ToList();
        }

        public int SaveItemGameUser(int intUserID, int intKiotID, int intItemID, int intQuantity, decimal decAmount,
            decimal decPricePromotion, int intReasonID)
        {
            int myValue = 0;
            var obj = new ObjectParameter("Result", myValue);
            Uow.Context.Out_Kiot_SaveItemGameUser(intUserID, intKiotID, intItemID, intQuantity, decAmount,
                decPricePromotion, intReasonID, obj);
            myValue = obj.Value != null ? int.Parse(obj.Value.ToString()) : 0;
            return myValue;
        }

        public Out_Kiot_BuyItem_Result BuyItemInMarket(int userId, int salerId, int kiotId, int itemId, float tax)
        {
            return Uow.Context.Out_Kiot_BuyItem(userId, salerId, kiotId, itemId, tax).FirstOrDefault();
        }

        public int RemoveItemGameUser(int intUserID, int intKiotID, int intItemID, int intReasonID)
        {
            int myValue = 0;
            var objResult = new ObjectParameter("Result", myValue);
            Uow.Context.Out_Kiot_RemoveItemGameUser(intUserID, intKiotID, intItemID, intReasonID, objResult);
            myValue = objResult.Value != null ? int.Parse(objResult.Value.ToString()) : 0;
            return myValue;
        }

        public int UpdatePromotion(int intUserID, int intKiotID, int intItemID, int intReasonID)
        {
            int myValue = 0;
            var objResult = new ObjectParameter("Result", myValue);
            Uow.Context.Out_Kiot_UpdatePromotion(intUserID, intKiotID, intItemID, intReasonID, objResult);
            myValue = objResult.Value != null ? int.Parse(objResult.Value.ToString()) : 0;
            return myValue;

        }

        public List<Out_Kiot_GetItemSold_Result> GetItemSold(int intUserID, int intItemID)
        {
            return Uow.Context.Out_Kiot_GetItemSold(intUserID, intItemID).ToList();
        }

        public List<Out_Kiot_GetRandomItemOfFriend_Result> GetRandomItemOfFriend(int intUserID, int intTopHasPromotion, int intTopNoPromotion, string strKiotUserID)
        {
            return Uow.Context.Out_Kiot_GetRandomItemOfFriend(intUserID, intTopHasPromotion, intTopNoPromotion, strKiotUserID).ToList();
        }

        public int BuyKiot(int userId, int kiotId)
        {
            return Uow.Context.Out_Kiot_BuyKiot(userId, kiotId).FirstOrDefault().GetValueOrDefault(0);
        }

        public int SendMessage(int senderId, int receiverId, string messageContent, int messageType, int status, string ipAddress, int platformId, string hardwareId, bool isSystem, int parentId)
        {
            return Uow.Context.Out_OfflineMessage_Send(senderId, receiverId, messageContent, messageType, status,
                ipAddress, platformId, hardwareId, isSystem, parentId);
        }
    }
}
