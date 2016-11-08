using System;
using System.Collections.Generic;
using BusinessObject.MembershipModule.Enums;
using BusinessObject.MembershipModule.Models.Request;
using EntitiesObject.Entities.MetroMembershipEntities;

namespace BusinessObject.MembershipModule.Contract
{
    public interface ILogActionAdminBusiness
    {
        /// <summary>
        /// Ghi log thao tac cua admin
        /// </summary>
        /// <history>
        /// 2016-01-20 Create By TrungTT
        /// </history>
        LogActionAdminStatus InsertLog(LogActionAdminRequestModel model);
        /// <summary>
        /// Lấy danh sách adtion của Admin
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 17-2-2016 Create By MinhT
        /// </history>
        IEnumerable<Ins_ActionAdmin_GetAll_Result> GetListActionAdmin();

        /// <summary>
        /// Lấy danh sách ghi log action của Admin by AdminName
        /// </summary>
        /// <param name="adminName"></param>
        /// <param name="actionId"></param>
        /// <param name="rowStart"></param>
        /// <param name="rowEnd"></param>
        /// <param name="orderBy"></param>
        /// <param name="isDescending"></param>
        /// <param name="totalRow"></param>
        /// <returns></returns>
        /// <history>
        /// 17-2-2016 Create By MinhT
        /// </history>
        IEnumerable<Ins_LogActionAdmin_GetByAdminName_Result> GetListActionAdminByName(string adminName, int actionId, int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow);
    }
}
