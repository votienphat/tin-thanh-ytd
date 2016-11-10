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
        public string Diameter { get; set; }
        public string Length { get; set; }
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
        public int No { get; set; }
        public string Project { get; set; }
        public string PoNo { get; set; }
        public string ItemCategory { get; set; }
        public string Diameter { get; set; }
        public string Length { get; set; }
        public string Quantity { get; set; }
        public string Weight { get; set; }

        public string FirstDiameter { get; set; }
        public string FirstLength { get; set; }
        public string FirstQuantity { get; set; }
        public string FirstWeight { get; set; }
        public string FirstCutLength { get; set; }
        public string SecondDiameter { get; set; }
        public string SecondLength { get; set; }
        public string SecondQuantity { get; set; }
        public string SecondWeight { get; set; }
        public string ParentRow { get; set; }

    }
    public class ExcelCalExport
    {
        public string FirstDiameter { get; set; }
        public string FirstLength { get; set; }
        public string FirstCutLength { get; set; }
        public string FirstQuantity { get; set; }
        public string FirstWeight { get; set; }
        public string SecondDiameter { get; set; }
        public string SecondLength { get; set; }
        public string SecondQuantity { get; set; }
        public string SecondWeight { get; set; }
        public string ParentRow { get; set; }

    }
}