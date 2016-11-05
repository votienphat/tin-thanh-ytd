using System.Collections.Generic;
using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Contract.UserModule
{
    public interface IRankingDetailRepository : IDaoRepository<RankingDetail>
    {
        List<Out_RankingDetail_GetTopChargeCard_Result> GetTopChargeCard(int rowStart, int rowEnd);
        List<Out_RankingDetail_GetTopExChangeCard_Result> GetTopExChangeCard(int rowStart, int rowEnd);
    }
}
