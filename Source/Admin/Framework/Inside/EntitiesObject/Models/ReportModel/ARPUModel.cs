using System;

namespace EntitiesObject.Models.ReportModel
{
    public class ARPUModel
    {
        public int? TotalUser { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? AverageAmount { get; set; }

        public DateTime? ReportDate { get; set; }

        public ARPUModel()
        {
            TotalUser = 0;
            TotalAmount = 0;
            AverageAmount = 0;
        }

        public ARPUModel(DateTime date)
        {
            TotalUser = 0;
            TotalAmount = 0;
            AverageAmount = 0;
            ReportDate = date;
        }
    }
}
