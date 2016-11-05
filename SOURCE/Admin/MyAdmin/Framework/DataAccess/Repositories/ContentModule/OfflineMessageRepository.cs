using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using DataAccess.Contract.ContentModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.ContentModule
{
    public class OfflineMessageRepository : DaoRepository<UserEntities, OfflineMessage>, IOfflineMessageRepository
    {
        public IEnumerable<Out_OfflineMessage_Get_Result> GetMessages(int userId,int rowStart, int rowEnd, int messageType, out int totalrow)
        {
            totalrow = 0;
            var outTotalRow = new ObjectParameter("TotalRow", totalrow);
            var result = Uow.Context.Out_OfflineMessage_Get(userId,rowStart, rowEnd, messageType, outTotalRow).ToList();
            totalrow = outTotalRow.Value == null ? 0 : int.Parse(outTotalRow.Value.ToString());
            return result;
        }

        public Out_OfflineMessage_GetDetails_Result GetMessageDetail(int userId, int messageId)
        {
            return Uow.Context.Out_OfflineMessage_GetDetails(userId, messageId).FirstOrDefault();
        }

        public List<int?> DeleteMessage(int userId,string listId, string seperator, out int result)
        {
            result = 0;
            var outResult = new ObjectParameter("Result", result);
            List<int?> value= Uow.Context.Out_OfflineMessage_Delete(userId, listId, seperator, outResult).ToList();
            result = outResult.Value == null ? 0 : int.Parse(outResult.Value.ToString());
            return value;
        }

        public int SendMessage(int senderId, int receiverId, string messageContent, int messageType, int status, string ipAddress, int platformId, string hardwareId, bool isSystem, int parentId)
        {
            return Uow.Context.Out_OfflineMessage_Send(senderId, receiverId, messageContent, messageType, status,
                ipAddress, platformId, hardwareId, isSystem, parentId);
        }
    }
}
