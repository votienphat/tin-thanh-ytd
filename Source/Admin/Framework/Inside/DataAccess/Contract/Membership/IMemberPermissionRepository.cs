using System.Collections.Generic;
using DataAccess.Repositories.Infrastructure.Contract;
using EntitiesObject.Entities.MetroMembershipEntities;

namespace DataAccess.Contract.Membership
{
    public interface IMemberPermissionRepository : IDaoRepository<MemberPermission>
    {
        /// <summary>
        /// Lấy danh sách quyền của user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 16/11/2015 PhatVT: Create new
        /// </history>
        List<Ins_MemberPermission_GetPermissionByUser_Result> GetPermissionByUser(int userId);
    }
}
