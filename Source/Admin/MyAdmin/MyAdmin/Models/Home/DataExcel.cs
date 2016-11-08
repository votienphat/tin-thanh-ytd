using System;
using System.Collections.Generic;
using OfficeOpenXml;

namespace MyAdmin.Models.DataExcel
{
    public class DataExcel
    {
        public List<ExcelModel> ImportDataExcel { get; set; }
    }
  
    public class ExcelModel
    {
        public string No { get; set; }
        public string Project { get; set; }
        public string PoNo { get; set; }
        public string ItemCategory { get; set; }
        public string Diameter { get; set; }
        public string Leght { get; set; }
        public string Quantity { get; set; }
        public string Weight { get; set; }

        public ExcelRange CellNo { get; set; }
        public ExcelRange CellProject { get; set; }
        public ExcelRange CellPoNo { get; set; }
        public ExcelRange CellItemCategory { get; set; }
        public ExcelRange CellDiameter { get; set; }
        public ExcelRange CellLength { get; set; }
        public ExcelRange CellQuantity { get; set; }
        public ExcelRange CellWeight { get; set; }
    }
    public class ExcelExport
    {
        public string No { get; set; }
        public string Project { get; set; }
        public string PoNo { get; set; }
        public string ItemCategory { get; set; }
        public string Diameter { get; set; }
        public string Leght { get; set; }
        public string Quantity { get; set; }
        public string Weight { get; set; }

        public string DiameterFist { get; set; }
        public string LeghtFist { get; set; }
        public string QuantityFist { get; set; }
        public string WeightFist { get; set; }
        public string LeghtFistCut { get; set; }
        public string DiameterSecond { get; set; }
        public string LeghtSecond { get; set; }
        public string QuantitySecond { get; set; }
        public string WeightSecond { get; set; }
        public string ParentRow { get; set; }

    }
    public class ExcelCalExport
    {
        public string DiameterFist { get; set; }
        public string LeghtFist { get; set; }
        public string LeghtFistCut { get; set; }
        public string QuantityFist { get; set; }
        public string WeightFist { get; set; }
        public string DiameterSecond { get; set; }
        public string LeghtSecond { get; set; }
        public string QuantitySecond { get; set; }
        public string WeightSecond { get; set; }
        public string ParentRow { get; set; }

    }
}