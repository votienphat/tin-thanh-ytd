using System;
using System.Collections.Generic;

namespace BussinessObject.EventModule.Models
{
    public class LuckyEverydayEventDate
    {
        public DateTime EventDate { get; set; }
        public List<PeriodOfTime> PeriodOfTimes { get; set; }
    }
    public class PeriodOfTime
    {
        public int PeriodTime { get; set; }
        public string PeriodTimeString { get; set; }
        public string PeriodTimeValue { get; set; }
    }
}