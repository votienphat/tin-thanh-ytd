using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Contract.Membership;
using DataAccess.Entity;
using DataAccess.Repositories.Infrastructure;
using EntitiesObject.Entities.MetroMembershipEntities;

namespace DataAccess.Repositories.Membership
{
    public class LogActionAdminRepository : DaoRepository<MetroMembershipEntities, LogActionAdmin>, ILogActionAdminRepository
    {
        /// <summary>
        /// <para>Author: TrungTT</para>
        /// <para>Date: 2016-01-21</para>
        /// <para>Description: Luu log thao tac cua admin</para>
        /// </summary>
        /// <returns></returns>
        public int InsertLog(int adminId, int actionId, string actionName, string objectId, string description,
            string beforeConfig, string ipAddress, string userAgent)
        {
            var rs = Uow.Context.Ins_LogActionAdmin_InsertLog(adminId, actionId, actionName, objectId, description, beforeConfig,ipAddress, userAgent).FirstOrDefault();
            return rs.GetValueOrDefault(0);
        }
        public IEnumerable<Ins_LogActionAdmin_GetByAdminName_Result> GetListActionAdminByName(string adminName, int actionId, int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow)
        {
            totalRow = 0;
            var outTotalRow = new ObjectParameter("TotalRow", totalRow);
            var result = Uow.Context.Ins_LogActionAdmin_GetByAdminName(adminName, actionId, rowStart, rowEnd, orderBy, isDescending, outTotalRow).ToList();
            totalRow = outTotalRow.Value == null ? 0 : int.Parse(outTotalRow.Value.ToString());

            return result;
        }
    }
}
