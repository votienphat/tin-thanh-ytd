using System;
using System.Collections.Generic;

namespace BussinessObject.EventModule.Models
{
    public class LuckyEverydayModel : LuckyEverydayMiniModel
    {
        public DateTime EventDate { get; set; }
        public int PeriodOfTime { get; set; }
    }
    public class LuckyEverydayMiniModel
    {
        public long RowNumber { get; set; }
        public string DisplayName { get; set; }
        public string PeriodOfTimeValue { get; set; }
        public decimal Value { get; set; }
        public string Present { get; set; }
        public int TotalRow { get; set; }
    }
    public class LuckyEverydayListMiniModel
    {
        public List<LuckyEverydayMiniModel> LuckyEverydayMiniModel { get; set; }
     
    }

}