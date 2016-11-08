using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using MyAdmin.Models.DataExcel;

namespace MyAdmin.Helpers
{
    public class ExcelHelpers
    {
        const string START_HEADER = "1";

        const int START_ROW = 1;

        #region Export 
        public ExcelPackage ExportData(List<ExcelExport> dataExport, string excelTitle, string[] titles, string cols)
        {
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = null;
            var datetime = DateTime.Now;
            ws = pck.Workbook.Worksheets.Add("BAOCAO");
            ws = FormatHeader(ws, cols, titles);

            int row = 2, roll = 1;
            foreach (var data in dataExport)
            {

                ws.Cells["A" + row + ":" + "D" + row].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                ws.Cells["A" + row].Value = data.No;

                ws.Cells["B" + row].Value = data.Project;

                ws.Cells["C" + row].Value = data.PoNo;

                ws.Cells["D" + row].Value = data.ItemCategory;
                ws.Cells["E" + row].Value = data.Diameter;
                ws.Cells["F" + row].Value = data.Leght;
                ws.Cells["G" + row].Value = data.Quantity;
                ws.Cells["H" + row].Value = data.Weight;
                ws.Cells["I" + row].Value = data.DiameterFist;
                ws.Cells["J" + row].Value = data.LeghtFist;
                ws.Cells["K" + row].Value = data.LeghtFistCut;
                ws.Cells["L" + row].Value = data.QuantityFist;
                ws.Cells["M" + row].Value = data.WeightFist;

                ws.Cells["N" + row].Value = data.DiameterSecond;
                ws.Cells["O" + row].Value = data.LeghtSecond;
                ws.Cells["P" + row].Value = data.QuantitySecond;
                ws.Cells["Q" + row].Value = data.WeightSecond;
                ws.Cells["R" + row].Value = data.ParentRow;
                row++;
                roll++;
            }
            for (int i = 1; i <= cols.Length; i++)
            {
                ws.Column(i).AutoFit(0, 50);
            }
            return pck;

        }
        #endregion

        public DataExcel ImportDataExcel(string filePath)
        {
            var existingFile = new FileInfo(filePath);
            int startRow = START_ROW;
            DataExcel importResult = new DataExcel
            {
                ImportDataExcel = new List<ExcelModel>()
            };
            using (var package = new ExcelPackage(existingFile))
            {
                ExcelWorkbook workBook = package.Workbook;
                if (workBook != null)
                {
                    if (workBook.Worksheets.Count > 0)
                    {
                        ExcelWorksheet currentWorksheet = workBook.Worksheets.First();
                        var lastRowIndex = GetLastUsedRow(currentWorksheet);
                        for (int rowNumber = startRow + 1; rowNumber < lastRowIndex; rowNumber++)
                        {

                            var datanew = new ExcelModel
                            {
                                CellNo = currentWorksheet.Cells[rowNumber, 1],
                                CellProject = currentWorksheet.Cells[rowNumber, 2],
                                CellPoNo = currentWorksheet.Cells[rowNumber, 3],
                                CellItemCategory = currentWorksheet.Cells[rowNumber, 4],
                                CellDiameter = currentWorksheet.Cells[rowNumber, 5],
                                CellLength = currentWorksheet.Cells[rowNumber, 6],
                                CellQuantity = currentWorksheet.Cells[rowNumber, 7],
                                CellWeight = currentWorksheet.Cells[rowNumber, 8]
                            };

                            datanew.No = datanew.CellNo.Value.ToString().TrimStart().TrimEnd();
                            datanew.Project = datanew.CellProject.Value.ToString().TrimStart().TrimEnd();
                            datanew.PoNo = datanew.CellPoNo.Value.ToString().TrimStart().TrimEnd();
                            datanew.ItemCategory = datanew.CellItemCategory.Value.ToString().TrimStart().TrimEnd();
                            datanew.Diameter = datanew.CellDiameter.Value.ToString().TrimStart().TrimEnd();
                            datanew.Leght = datanew.CellLength.Value.ToString().TrimStart().TrimEnd();
                            datanew.Quantity = datanew.CellQuantity.Value.ToString().TrimStart().TrimEnd();
                            datanew.Weight = datanew.CellWeight.Value.ToString().TrimStart().TrimEnd();
                            importResult.ImportDataExcel.Add(datanew);
                        }
                    }
                }
            }
            return importResult;
        }

        int GetLastUsedRow(ExcelWorksheet sheet)
        {
            var row = sheet.Dimension.End.Row;
            while (row >= 1)
            {
                var range = sheet.Cells[row, 1, row, sheet.Dimension.End.Column];
                if (range.Any(c => !string.IsNullOrEmpty(c.Text)))
                {
                    break;
                }
                row--;
            }
            return row;
        }

        private ExcelWorksheet FormatHeader(ExcelWorksheet ws, string cols, string[] titles)
        {
            int i = 0;
            foreach (var character in cols)
            {
                ws.Cells[character + START_HEADER].Value = titles[i];
                ws.Cells[character + START_HEADER].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                ws.Cells[character + START_HEADER].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[character + START_HEADER].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                ws.Cells[character + START_HEADER].Style.Font.Bold = true;
                ws.Cells[character + START_HEADER].Style.Font.Size = 14;
                ws.Cells[character + START_HEADER].AutoFitColumns();
                ws.Cells[character + START_HEADER].Style.Font.Color.SetColor(Color.Black);
                i++;
            }
            return ws;
        }

        public DataExcel ImportDataExcel2(string filePath)
        {
            var existingFile = new FileInfo(filePath);
            int startRow = START_ROW;
            DataExcel importResult = new DataExcel
            {
                ImportDataExcel = new List<ExcelModel>()
            };
            using (var package = new ExcelPackage(existingFile))
            {
                ExcelWorkbook workBook = package.Workbook;
                if (workBook != null)
                {
                    if (workBook.Worksheets.Count > 0)
                    {
                        ExcelWorksheet currentWorksheet = workBook.Worksheets.First();
                        var lastRowIndex = GetLastUsedRow(currentWorksheet);
                        for (int rowNumber = startRow + 1; rowNumber <= lastRowIndex; rowNumber++)
                        {
                            var datanew = new ExcelModel();
                            datanew.No = currentWorksheet.Cells[rowNumber, 1].Value.ToString().TrimStart().TrimEnd();
                            datanew.Project = currentWorksheet.Cells[rowNumber, 2].Value.ToString().TrimStart().TrimEnd();
                            datanew.PoNo = currentWorksheet.Cells[rowNumber, 3].Value.ToString().TrimStart().TrimEnd();
                            datanew.ItemCategory = currentWorksheet.Cells[rowNumber, 4].Value.ToString().TrimStart().TrimEnd();
                            datanew.Diameter = currentWorksheet.Cells[rowNumber, 5].Value.ToString().TrimStart().TrimEnd();
                            datanew.Leght = currentWorksheet.Cells[rowNumber, 6].Value.ToString().TrimStart().TrimEnd();
                            datanew.Quantity = currentWorksheet.Cells[rowNumber, 7].Value.ToString().TrimStart().TrimEnd();
                            datanew.Weight = currentWorksheet.Cells[rowNumber, 8].Value.ToString().TrimStart().TrimEnd();
                            importResult.ImportDataExcel.Add(datanew);
                        }
                    }
                }
            }
            return importResult;
        }

    }
}