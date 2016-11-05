using System.Collections.Generic;
using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.LogManagementEntities;

namespace DataAccess.Contract.PaymentModule
{
    public interface IPaymentLogRepository : IDaoRepository<PaymentLog>
    {
        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 22/12/2015</para>
        /// <para>lịch sử nạp gold của user</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        ///  <param name="totalRow"></param>
        /// <returns></returns>
        List<Out_PaymentLog_GetHistoryPagingByUserId_Result> PaymentLog_GetHistoryPagingByUserId(int userId,
            int pageNumber, int pageSize, out int totalRow);

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 22/12/2015</para>
        /// <para>lưu lịch sử nạp tiền</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="paymentId"></param>
        /// <param name="platformId"></param>
        /// <param name="imei"></param>
        /// <param name="harwareid"></param>
        /// <param name="ipaddress"></param>
        /// <param name="status"></param>
        /// <param name="objId"></param>
        /// <param name="paymentAmount"></param>
        /// <param name="itemType"></param>
        /// <returns></returns>
        int PaymentLog_InsertData(int userId, int paymentId, int platformId, string imei, string harwareid,
            string ipaddress, int status, int objId,decimal paymentAmount, int itemType);

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 22/12/2015</para>
        /// <para>cập nhật log nạp gold</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        int PaymentLog_UpdateData(int id, int status);
    }
}
