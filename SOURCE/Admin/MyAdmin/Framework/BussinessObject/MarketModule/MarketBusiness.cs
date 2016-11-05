using System.Collections.Generic;
using BussinessObject.MarketModule.Contract;
using DataAccess.Contract.MarketModule;
using EntitiesObject.Entities.UserEntities;

namespace BussinessObject.MarketModule
{
    public class MarketBusiness : IMarketBusiness
    {
        #region Variables

        private readonly IKiotRepository _kiotRepo;

        #endregion

        public MarketBusiness(IKiotRepository kiotRepo)
        {
            _kiotRepo = kiotRepo;
        }

        /// <summary>
        /// Lay danh sach kiot (Load cau hinh mat hang) - Duynd - 29/03/2016
        /// </summary>
        public List<Out_Kiot_GetKiotUser_Result> GetKiotUser(int intUserID)
        {
            return _kiotRepo.GetKiotUser(intUserID);
        }

        public List<Out_Kiot_GetAllKiot_V3_Result> GetAllKiot(int userId)
        {
            return _kiotRepo.GetAllKiot(userId);
        }

        /// <summary>
        /// Lay toan bo mat hang cua user - Duynd - 29/03/2016
        /// </summary>
        public List<Out_Kiot_GetAllItemUser_Result> GetAllItemUser(int intUserID)
        {
            return _kiotRepo.GetAllItemUser(intUserID);
        }

        /// <summary>
        /// Luu item game user - Duynd - 29/03/2016
        /// </summary>
        public int SaveItemGameUser(int intUserID, int intKiotID, int intItemID, int intQuantity, decimal decAmount,
            decimal decPricePromotion, int intReasonID)
        {
            return _kiotRepo.SaveItemGameUser(intUserID, intKiotID, intItemID, intQuantity, decAmount,
                decPricePromotion, intReasonID);
        }

        public Out_Kiot_BuyItem_Result BuyItemInMarket(int userId, int salerId, int kiotId, int itemId, float tax)
        {
            return _kiotRepo.BuyItemInMarket(userId, salerId, kiotId, itemId, tax);
        }

        /// <summary>
        /// Xoa san pham trong kiot - Duynd - 30/03/2016
        /// </summary>
        public int RemoveItemGameUser(int intUserID, int intKiotID, int intItemID, int intReasonID)
        {
            return _kiotRepo.RemoveItemGameUser(intUserID, intKiotID, intItemID, intReasonID);
        }

        /// <summary>
        /// Cap nhat quang cao trong kiot - Duynd - 30/03/2016
        /// </summary>
        public int UpdatePromotion(int intUserID, int intKiotID, int intItemID, int intReasonID)
        {
            return _kiotRepo.UpdatePromotion(intUserID, intKiotID, intItemID, intReasonID);
        }

        /// <summary>
        /// Lay danh sach san pham da ban - Duynd - 30/03/2016
        /// </summary>
        public List<Out_Kiot_GetItemSold_Result> GetItemSold(int intUserID, int intItemID)
        {
            return _kiotRepo.GetItemSold(intUserID, intItemID);
        }

        /// <summary>
        /// Lay danh sach san pham dang ban cua ban be - Duynd - 30/03/2016
        /// </summary>        
        public List<Out_Kiot_GetRandomItemOfFriend_Result> GetRandomItemOfFriend(int intUserID, int intTopHasPromotion, int intTopNoPromotion, string strKiotUserID)
        {
            return _kiotRepo.GetRandomItemOfFriend(intUserID, intTopHasPromotion, intTopNoPromotion, strKiotUserID);
        }

        public int BuyKiot(int userId, int kiotId)
        {
            return _kiotRepo.BuyKiot(userId, kiotId);
        }

        public int SendMessage(int senderId, int receiverId, string messageContent, int messageType, int status, string ipAddress, int platformId, string hardwareId, bool isSystem, int parentId)
        {
            return _kiotRepo.SendMessage(senderId, receiverId, messageContent, messageType, status,
                ipAddress, platformId, hardwareId, isSystem, parentId);
        }
    }
}
