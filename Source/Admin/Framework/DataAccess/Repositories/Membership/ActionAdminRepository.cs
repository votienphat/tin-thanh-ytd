using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Contract.Membership;
using DataAccess.Entity;
using DataAccess.Repositories.Infrastructure;
using EntitiesObject.Entities.MetroMembershipEntities;

namespace DataAccess.Repositories.Membership
{
    public class ActionAdminRepository : DaoRepository<MetroMembershipEntities, ActionAdmin>, IActionAdminRepository
    {
        public IEnumerable<Ins_ActionAdmin_GetAll_Result> GetAll()
        {
            return Uow.Context.Ins_ActionAdmin_GetAll();
        }
    }
}
