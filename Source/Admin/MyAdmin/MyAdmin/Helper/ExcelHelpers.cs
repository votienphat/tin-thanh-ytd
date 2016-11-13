using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Logger;
using MyAdmin.Models.Home;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace MyAdmin.Helpers
{
    public class ExcelHelpers
    {
        #region Export 
        public ExcelPackage ExportData(List<ExcelExport> dataExport, string excelTitle, string[] titles, string cols,int RowData)
        {
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = null;
            var datetime = DateTime.Now;
            ws = pck.Workbook.Worksheets.Add("BAOCAO");
            ws = FormatHeader(ws, cols, titles, RowData);

            int row = RowData, roll = RowData -1,indexRow = 1;
            foreach (var data in dataExport)
            {
                ws.Cells["A" + row + ":" + "R" + row].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                ws.Cells["A" + row].Value = data.No;
                ws.Cells["B" + row].Value = data.Project;
                ws.Cells["C" + row].Value = data.PoNo;
                ws.Cells["D" + row].Value = data.ItemCategory;
                ws.Cells["E" + row].Value = data.Diameter;
                ws.Cells["F" + row].Value = data.Length;
                ws.Cells["G" + row].Value = data.Quantity;
                ws.Cells["H" + row].Value = data.Weight;
                ws.Cells["I" + row].Value = data.FirstDiameter;
                ws.Cells["J" + row].Value = data.FirstLength;
                ws.Cells["K" + row].Value = data.FirstCutLength;
                ws.Cells["L" + row].Value = data.FirstQuantity;
                ws.Cells["M" + row].Value = data.FirstWeight;

                ws.Cells["N" + row].Value = data.SecondDiameter;
                ws.Cells["O" + row].Value = data.SecondLength;
                ws.Cells["P" + row].Value = data.SecondQuantity;
                ws.Cells["Q" + row].Value = data.SecondWeight;
                ws.Cells["R" + row].Value = data.ParentRow;
                ws.Cells["S" + row].Value = indexRow;
                row++;
                roll++;
                indexRow++;
            }
            for (int i = 1; i <= cols.Length; i++)
            {
                ws.Column(i).AutoFit(0, 50);
            }
            return pck;

        }
        #endregion
        public DataExcel ImportDataExcel(string filePath,int RowData)
        {
            var existingFile = new FileInfo(filePath);
            int startRow = RowData;
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
                        for (int rowNumber = startRow; rowNumber <= lastRowIndex; rowNumber++)
                        {

                            var newRow = new ExcelModel
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
                            
                            GetModelRequest(newRow);
                            //datanew.No = datanew.CellNo.Value.ToString().TrimStart().TrimEnd();
                            //datanew.Project = datanew.CellProject.Value.ToString().TrimStart().TrimEnd();
                            //datanew.PoNo = datanew.CellPoNo.Value.ToString().TrimStart().TrimEnd();
                            //datanew.ItemCategory = datanew.CellItemCategory.Value.ToString().TrimStart().TrimEnd();
                            //datanew.Diameter = datanew.CellDiameter.Value.ToString().TrimStart().TrimEnd();
                            //datanew.Length = datanew.CellLength.Value.ToString().TrimStart().TrimEnd();
                            //datanew.Quantity = datanew.CellQuantity.Value.ToString().TrimStart().TrimEnd();
                            //datanew.Weight = datanew.CellWeight.Value.ToString().TrimStart().TrimEnd();
                            importResult.ImportDataExcel.Add(newRow);
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

        private ExcelWorksheet FormatHeader(ExcelWorksheet ws, string cols, string[] titles,int RowData)
        {
            int i = 0;
            string START_HEADER = (RowData - 1).ToString();
            foreach (var character in cols)
            {
                ws.Cells[character + START_HEADER].Value = titles[i];
                ws.Cells[character + START_HEADER].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                ws.Cells[character + START_HEADER].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[character + START_HEADER].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                ws.Cells[character + START_HEADER].Style.Font.Bold = true;
                ws.Cells[character + START_HEADER].Style.Font.Size = 10;
                ws.Cells[character + START_HEADER].AutoFitColumns();
                ws.Cells[character + START_HEADER].Style.Font.Color.SetColor(Color.Black);
                i++;
            }
            return ws;
        }


        /// <summary>
        /// <para>Author: TrungTT</para>
        /// <para>Date: 2015-08-27</para>
        /// <para>Author: Convert request data sang object tuong ung</para>
        /// </summary>
        /// <returns></returns>
        public static void GetModelRequest(ExcelModel formData)
        {
            if (formData == null)
                return;

            try
            {
                var type = typeof(ExcelModel);
                var listProperties = type.GetProperties();
                if (listProperties.Length > 0)
                {
                    foreach (var prop in listProperties)
                    {
                        // Tên mấy cái đối tượng ô tỏng Excel luôn phải bắt đầu bằng từ Cell
                        var cellType = typeof(ExcelRange);
                        if (prop.PropertyType == cellType)
                        {
                            continue;
                        }

                        // Nếu không tồn tại cell tương ứng thì không lấy dữ liệu
                        var cellProp = type.GetProperty("Cell" + prop.Name);
                        if (cellProp == null)
                        {
                            continue;
                        }

                        ExcelRange cellValue = cellProp.GetValue(formData) as ExcelRange;
                        if (cellValue == null || cellValue.Value == null)
                        {
                            continue;
                        }

                        string value = cellValue.Value.ToString();

                        // Dựa vào từng loại mà gán dữ liệu
                        if (prop.PropertyType.ToString().Contains("Int32"))
                        {
                            int intValue;
                            if (int.TryParse(value, out intValue))
                            {
                                prop.SetValue(formData, intValue);
                            }
                        }
                        else if (prop.PropertyType.ToString().Contains("Int64"))
                        {
                            long longValue;
                            if (long.TryParse(value, out longValue))
                            {
                                prop.SetValue(formData, longValue);
                            }
                        }
                        else if (prop.PropertyType.ToString().Contains("Decimal"))
                        {
                            decimal decimalValue;
                            if (decimal.TryParse(value, out decimalValue))
                            {
                                prop.SetValue(formData, decimalValue);
                            }
                        }
                        else if (prop.PropertyType.ToString().Contains("Double"))
                        {
                            double doubleValue;
                            if (double.TryParse(value, out doubleValue))
                            {
                                prop.SetValue(formData, doubleValue);
                            }
                        }
                        else if (prop.PropertyType.ToString().Contains("DateTime"))
                        {
                            DateTime dtValue;
                            if (DateTime.TryParse(value, out dtValue))
                            {
                                prop.SetValue(formData, dtValue);
                            }
                        }
                        else if (prop.PropertyType.ToString().Contains("Boolean"))
                        {
                            bool dtValue;
                            if (Boolean.TryParse(value, out dtValue))
                            {
                                prop.SetValue(formData, dtValue);
                            }
                        }
                        else
                        {
                            prop.SetValue(formData, value);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.Error("GetModelRequest", ex);
            }
        }
    }
}