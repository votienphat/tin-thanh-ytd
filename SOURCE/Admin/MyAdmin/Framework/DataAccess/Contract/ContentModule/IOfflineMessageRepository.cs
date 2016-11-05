using System.Collections.Generic;
using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Contract.ContentModule
{
    public interface IOfflineMessageRepository : IDaoRepository<OfflineMessage>
    {
        IEnumerable<Out_OfflineMessage_Get_Result> GetMessages(int userId, int rowStart, int rowEnd, int messageType, out int totalrow);
        
        Out_OfflineMessage_GetDetails_Result GetMessageDetail(int userId, int messageId);

        List<int?> DeleteMessage(int userId, string listId, string seperator, out int result);

        int SendMessage(int senderId,int receiverId,string messageContent, int messageType,int status,string ipAddress,int platformId,string hardwareId,bool isSystem,int parentId);
    }
}
