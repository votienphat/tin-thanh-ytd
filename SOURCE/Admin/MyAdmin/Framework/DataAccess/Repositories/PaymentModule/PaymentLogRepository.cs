using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using DataAccess.Contract.PaymentModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.LogManagementEntities;

namespace DataAccess.Repositories.PaymentModule
{
    public class PaymentLogRepository : DaoRepository<LogManagementEntities, PaymentLog>, IPaymentLogRepository
    {
        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 22/12/2015</para>
        /// <para>lịch sử nạp gold của user</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRow"></param>
        /// <returns></returns>
        public List<Out_PaymentLog_GetHistoryPagingByUserId_Result> PaymentLog_GetHistoryPagingByUserId(int userId,
            int pageNumber, int pageSize, out int totalRow)
        {
            totalRow = 0;
            var outTotalRow = new ObjectParameter("TotalRow", totalRow);
            var result = Uow.Context.Out_PaymentLog_GetHistoryPagingByUserId(userId, pageNumber, pageSize, outTotalRow).ToList();
            totalRow = outTotalRow.Value == null ? 0 : Int32.Parse(outTotalRow.Value.ToString());
            return result;
        }

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
        public int PaymentLog_InsertData(int userId, int paymentId, int platformId, string imei, string harwareid,
            string ipaddress, int status, int objId, 
            decimal paymentAmount, int itemType)
        {
            return Uow.Context.Out_PaymentLog_InsertData(userId, paymentId, platformId, imei, harwareid, ipaddress,
                status, objId,  paymentAmount, itemType).FirstOrDefault().GetValueOrDefault(0);
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 22/12/2015</para>
        /// <para>cập nhật log nạp gold</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int PaymentLog_UpdateData(int id, int status)
        {
            return Uow.Context.Out_PaymentLog_UpdateData(id, status);
        }

    }
}
