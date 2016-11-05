using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesObject.Entities.MetroMembershipEntities;
using DataAccess.Repositories.Infrastructure.Contract;

namespace DataAccess.Contract.Membership
{
    public interface ILogActionAdminRepository : IDaoRepository<LogActionAdmin>
    {
        int InsertLog(int adminId, int actionId, string actionName, string objectId, string description,
            string beforeConfig, string ipAddress, string userAgent);

        IEnumerable<Ins_LogActionAdmin_GetByAdminName_Result> GetListActionAdminByName(string adminName, int actionId, int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow);
    }
}
