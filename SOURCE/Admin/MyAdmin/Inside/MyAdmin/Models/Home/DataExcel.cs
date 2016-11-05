using System;
using System.Collections.Generic;

namespace MyAdmin.Models.DataExcel
{
    public class DataExcel
    {
        public List<ExcelModel> ImportDataExcel { get; set; }
    }
  
    public class ExcelModel
    {
        public int No { get; set; }
        public string Project { get; set; }
        public string PoNo { get; set; }
        public string ItemCategory { get; set; }
        public string Diameter { get; set; }
        public int Leght { get; set; }
        public int Quantity { get; set; }
        public string Weight { get; set; }

        public string DiameterFist { get; set; }
        public int LeghtFist { get; set; }
        public int QuantityFist { get; set; }
        public int WeightFist { get; set; }

    }
}