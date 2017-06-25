using System;
using System.Collections.Generic;
using DataAccess.Repositories.Infrastructure.Contract;
using EntitiesObject.Entities.VanTaiEntities;

namespace DataAccess.Contract.Goods
{
    public interface IPORepository : IDaoRepository<PO>
    {
        List<PO_Search_Result> Search(out int totalRow, string keyword = "", DateTime? startTime = null,
            DateTime? endTime = null, int startIndex = 0, int endIndex = 20);
    }
}
