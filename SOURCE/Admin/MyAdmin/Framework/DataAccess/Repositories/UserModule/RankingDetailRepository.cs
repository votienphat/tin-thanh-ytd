using System.Collections.Generic;
using System.Linq;
using DataAccess.Contract.UserModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.UserModule
{
    public class RankingDetailRepository : DaoRepository<UserEntities, RankingDetail>, IRankingDetailRepository
    {
        public List<Out_RankingDetail_GetTopChargeCard_Result> GetTopChargeCard(int rowStart, int rowEnd)
        {
            return Uow.Context.Out_RankingDetail_GetTopChargeCard(rowStart, rowEnd).ToList();
        }


        public List<Out_RankingDetail_GetTopExChangeCard_Result> GetTopExChangeCard(int rowStart, int rowEnd)
        {
            return Uow.Context.Out_RankingDetail_GetTopExChangeCard(rowStart, rowEnd).ToList();
        }
    }
}
