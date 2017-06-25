using System;
using System.Collections.Generic;
using EntitiesObject.Entities.VanTaiEntities;

namespace BussinessObject.GoodsModule.Contract
{
    public interface IGoodsBusiness
    {
        List<PO_Search_Result> POSearch(out int totalRow, string keyword = "", DateTime? startTime = null,
            DateTime? endTime = null, int startIndex = 0, int endIndex = 20);
    }
}