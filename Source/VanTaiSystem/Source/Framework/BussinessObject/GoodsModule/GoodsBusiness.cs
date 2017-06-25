using System;
using System.Collections.Generic;
using BussinessObject.GoodsModule.Contract;
using DataAccess.Contract.Goods;
using EntitiesObject.Entities.VanTaiEntities;

namespace BussinessObject.GoodsModule
{
    public class GoodsBusiness : IGoodsBusiness
    {
        #region Varriables

        private readonly IPORepository _poRepo;

        #endregion

        #region Constructor

        public GoodsBusiness(IPORepository poRepo)
        {
            _poRepo = poRepo;
        }

        #endregion

        public List<PO_Search_Result> POSearch(out int totalRow, string keyword = "", DateTime? startTime = null, DateTime? endTime = null,
            int startIndex = 0, int endIndex = 20)
        {
            return _poRepo.Search(out totalRow, keyword, startTime, endTime, startIndex, endIndex);
        }
    }
}