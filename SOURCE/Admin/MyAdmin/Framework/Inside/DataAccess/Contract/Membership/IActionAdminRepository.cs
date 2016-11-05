using System.Collections.Generic;
using DataAccess.Repositories.Infrastructure.Contract;
using EntitiesObject.Entities.MetroMembershipEntities;

namespace DataAccess.Contract.Membership
{

    public interface IActionAdminRepository : IDaoRepository<ActionAdmin>
    {
        IEnumerable<Ins_ActionAdmin_GetAll_Result> GetAll();
    }
}