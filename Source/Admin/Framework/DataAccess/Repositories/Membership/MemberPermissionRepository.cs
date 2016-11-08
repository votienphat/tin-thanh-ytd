using System.Collections.Generic;
using System.Linq;
using DataAccess.Contract.Membership;
using DataAccess.Entity;
using DataAccess.Repositories.Infrastructure;
using EntitiesObject.Entities.MetroMembershipEntities;

namespace DataAccess.Repositories.Membership
{
    public class MemberPermissionRepository : DaoRepository<MetroMembershipEntities, MemberPermission>, IMemberPermissionRepository
    {
        /// <summary>
        /// Lấy danh sách quyền của user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 16/11/2015 PhatVT: Create new
        /// </history>
        public List<Ins_MemberPermission_GetPermissionByUser_Result> GetPermissionByUser(int userId)
        {
            return Uow.Context.Ins_MemberPermission_GetPermissionByUser(userId).ToList();
        }
    }
}