using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesObject.Entities.MetroMembershipEntities;
using DataAccess.Repositories.Infrastructure.Contract;

namespace DataAccess.Contract.Membership
{
    public interface IPageFunctionRepository : IDaoRepository<PageFunction>
    {
        /// <summary>
        /// <para>Author: TrungTT</para>
        /// <para>Date: 2016-01-21</para>
        /// <para>Description: Lay thong tin PageFunction theo id action</para>
        /// </summary>
        /// <param name="actionId">Id thao tac thuc hien</param>
        /// <returns></returns>
        Ins_PageFunction_GetPageFunctionById_Result GetPageFunctionById(int actionId);
    }
}
