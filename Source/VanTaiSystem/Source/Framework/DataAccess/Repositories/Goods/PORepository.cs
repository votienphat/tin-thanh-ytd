using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using DataAccess.Contract.Goods;
using DataAccess.EF;
using DataAccess.Repositories.Infrastructure;
using EntitiesObject.Entities.VanTaiEntities;

namespace DataAccess.Repositories.Goods
{
    public class PORepository : DaoRepository<VanTaiEntities, PO>, IPORepository
    {
        public List<PO_Search_Result> Search(out int totalRow, string keyword = "", DateTime? startTime = null,
            DateTime? endTime = null, int startIndex = 0, int endIndex = 20)
        {
            totalRow = 0;
            var outTotalRow = new ObjectParameter("TotalRow", totalRow);

            var result = Uow.Context.PO_Search(keyword, startTime, endTime, startIndex, endIndex, outTotalRow).ToList();
            totalRow = outTotalRow.Value == null ? 0 : int.Parse(outTotalRow.Value.ToString());

            return result;
        }
    }
}