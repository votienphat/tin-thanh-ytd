using System;

namespace EntitiesObject.Models.ReportModel
{
    public class CCUModel
    {

        public DateTime DateReport { get; set; }

        public int MaxCCU { get; set; }

        public int MinCCU { get; set; }

        public double AvgCCU { get; set; }
    }
}
