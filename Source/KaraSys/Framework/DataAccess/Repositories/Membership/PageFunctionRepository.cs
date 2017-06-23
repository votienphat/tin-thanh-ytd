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
    public class PageFunctionRepository : DaoRepository<MetroMembershipEntities, PageFunction>, IPageFunctionRepository
    {
        /// <summary>
        /// <para>Author: TrungTT</para>
        /// <para>Date: 2016-01-21</para>
        /// <para>Description: Lay thong tin PageFunction theo id action</para>
        /// </summary>
        /// <param name="actionId">Id thao tac thuc hien</param>
        /// <returns></returns>
        public Ins_PageFunction_GetPageFunctionById_Result GetPageFunctionById(int actionId)
        {
            return Uow.Context.Ins_PageFunction_GetPageFunctionById(actionId).FirstOrDefault();
        }
    }
}
