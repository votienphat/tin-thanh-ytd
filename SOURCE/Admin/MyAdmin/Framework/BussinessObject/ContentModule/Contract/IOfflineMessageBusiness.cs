using System.Collections.Generic;
using BussinessObject.PaymentModule.Models;

namespace BussinessObject.ContentModule.Contract
{
    public interface IOfflineMessageBusiness
    {
        /// <summary>
        /// Lấy thông tin message
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 28/12/2015: Created by MinhT
        /// </history>

        IEnumerable<MessageModel> GetMessages(int userId,int pageNumber, int pageSize, int messageType, out int totalrow);

        MessageModel GetMessageDetails(int userId, int messageId, out int result);
        List<int> DeleteMessage(int userId, string messageId, out int result);

        int SendMessage(int senderId, int receiverId, string messageContent, int messageType, int status, string ipAddress, int platformId, string hardwareId, bool isSystem, int parentId);
    }
}
