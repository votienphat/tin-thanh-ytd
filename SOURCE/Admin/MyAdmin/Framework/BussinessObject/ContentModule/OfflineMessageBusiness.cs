using System.Collections.Generic;
using System.Linq;
using BussinessObject.ContentModule.Contract;
using BussinessObject.PaymentModule.Models;
using DataAccess.Contract.ContentModule;
using MyUtility;

namespace BussinessObject.ContentModule
{
    public class OfflineMessageBusiness : IOfflineMessageBusiness
    {
        #region Variables

        private readonly IOfflineMessageRepository _ioffMessage;

        #endregion

        public OfflineMessageBusiness(IOfflineMessageRepository offMessageBusiness)
        {
            _ioffMessage = offMessageBusiness;
        }

        /// <summary>
        /// Create By: MinhT
        /// Date Create: 24/12/2015
        /// Lấy Top nộp card của User
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalrow"></param>
        /// <param name="messageType"></param>
        /// <returns></returns>
        public IEnumerable<MessageModel> GetMessages(int userId,int pageNumber, int pageSize, int messageType, out int totalrow)
        {
            const char myChar = '\n';
            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            var rowStart = (pageNumber - 1) * pageSize;
            var rowEnd = rowStart + pageSize - 1;
            var tem = _ioffMessage.GetMessages(userId, rowStart, rowEnd, messageType, out totalrow);
            return tem.Select(item => new MessageModel
            {
                MessageId = item.MessageId,
                SenderId = item.SenderId,
                SenderName = item.SenderName,
                ReceiverId = item.ReceiverId,
                ReceiverName = item.ReceiverName,
                MessageType = item.MessageType,
                MessageContent = item.MessageContent.LastIndexOf(myChar) >= 2 ? StringCommon.ToSubstring(myChar, item.MessageContent) : item.MessageContent,
                SendDate =item.SendDate.GetValueOrDefault().ToShortTimeString() + " " +item.SendDate.GetVnDateFormat(),
                IsRead = item.IsRead,
                Status = item.Status,
                ParentId = item.ParentId,
                RowNumber = item.RowNumber
            }).ToList();
        }

        /// <summary>
        /// Create by: MinhT
        /// Date Create: 25/12/2015
        /// Lấy chi tiết message
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="userId"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public MessageModel GetMessageDetails(int userId, int messageId, out int result)
        {

            var tem = _ioffMessage.GetMessageDetail(userId, messageId);

            if (tem!=null)
            {
                result = 1;
                return new MessageModel
                {
                    SenderId = tem.SenderId,
                    SenderName = tem.SenderName,
                    ReceiverId = tem.ReceiverId,
                    ReceiverName = tem.ReceiverName,
                    MessageType = tem.MessageType,
                    MessageContent = tem.MessageContent,
                    SendDate = tem.SendDate.GetVnDateFormat(),
                    Status = tem.Status,
                    ParentId = tem.ParentId
                };
            }
            result = 2;
            return new MessageModel();
        }

        /// <summary>
        /// Create by: MinhT
        /// Date Create: 25/12/2015
        /// Xóa 1 danh sách message
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="listId"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public List<int> DeleteMessage(int userId, string listId, out int result)
        {
            const string separetor = ",";
            var tem = _ioffMessage.DeleteMessage(userId, listId, separetor, out result).ToList();
            return tem.Select(item => item.GetValueOrDefault()).ToList();
        }

        public int SendMessage(int senderId, int receiverId, string messageContent, int messageType, int status, string ipAddress, int platformId, string hardwareId, bool isSystem, int parentId)
        {
            return _ioffMessage.SendMessage(senderId, receiverId, messageContent, messageType, status, ipAddress,
                platformId, hardwareId, isSystem, parentId);

        }
    }
}
