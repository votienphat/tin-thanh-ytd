using System.Collections.Generic;
using OfficeOpenXml;

namespace MyAdmin.Models.Home
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
        public int Diameter { get; set; }
        public long Length { get; set; }
        public int Quantity { get; set; }
        public decimal Weight { get; set; }

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
        public int No { get; set; }
        public string Project { get; set; }
        public string PoNo { get; set; }
        public string ItemCategory { get; set; }
        public int Diameter { get; set; }
        public long Length { get; set; }
        public int Quantity { get; set; }
        public decimal Weight { get; set; }

        public int? FirstDiameter { get; set; }
        public long? FirstLength { get; set; }
        public int? FirstQuantity { get; set; }
        public decimal? FirstWeight { get; set; }
        public long? FirstCutLength { get; set; }
        public int? SecondDiameter { get; set; }
        public long? SecondLength { get; set; }
        public int? SecondQuantity { get; set; }
        public decimal? SecondWeight { get; set; }
        public int? ParentRow { get; set; }
       
    }
    public class ExcelCalExport
    {
        public int? FirstDiameter { get; set; }
        public long? FirstLength { get; set; }
        public long? FirstCutLength { get; set; }
        public int? FirstQuantity { get; set; }
        public decimal? FirstWeight { get; set; }
        public int? SecondDiameter { get; set; }
        public long? SecondLength { get; set; }
        public int? SecondQuantity { get; set; }
        public decimal? SecondWeight { get; set; }
        public int? ParentRow { get; set; }

    }
}