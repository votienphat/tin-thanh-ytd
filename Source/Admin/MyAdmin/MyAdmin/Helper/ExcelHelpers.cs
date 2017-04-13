using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Logger;
using MyAdmin.Models.Home;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace MyAdmin.Helper
{
    public class ExcelHelpers
    {
        #region Export 

        private void CopyData(ExcelRange cell, object value, string format = "General")
        {
            cell.Value = value;
            cell.Style.Numberformat.Format = format;
        }

        public ExcelPackage ExportData(List<ExcelExport> dataExport, string excelTitle, string[] titles, string cols,int rowData)
        {
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = null;
            ws = pck.Workbook.Worksheets.Add("BAOCAO");
            ws = FormatHeader(ws, cols, titles, rowData);

            int row = rowData, roll = rowData -1,rowindex = 1;
            foreach (var data in dataExport)
            {
                ws.Cells["A" + row + ":" + "S" + row].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                CopyData(ws.Cells["A" + row], data.No, data.FormatNo.Format);
                CopyData(ws.Cells["B" + row], data.Project, data.FormatProject.Format);
                CopyData(ws.Cells["C" + row], data.PoNo, data.FormatPoNo.Format);
                CopyData(ws.Cells["D" + row], data.ItemCategory, data.FormatItemCategory.Format);
                CopyData(ws.Cells["E" + row], data.Diameter, data.FormatDiameter.Format);
                CopyData(ws.Cells["F" + row], data.Length, data.FormatLength.Format);
                CopyData(ws.Cells["G" + row], data.Quantity, data.FormatQuantity.Format);
                CopyData(ws.Cells["H" + row], data.Weight, data.FormatWeight.Format);
                CopyData(ws.Cells["I" + row], data.FirstDiameter, data.FormatFirstDiameter.Format);
                CopyData(ws.Cells["J" + row], data.FirstLength, data.FormatFirstLength.Format);
                CopyData(ws.Cells["K" + row], data.FirstCutLength, data.FormatFirstCutLength.Format);
                CopyData(ws.Cells["L" + row], data.FirstQuantity, data.FormatFirstQuantity.Format);
                CopyData(ws.Cells["M" + row], data.FirstWeight, data.FormatFirstWeight.Format);
                CopyData(ws.Cells["N" + row], data.SecondDiameter, data.FormatSecondDiameter.Format);
                CopyData(ws.Cells["O" + row], data.SecondLength, data.FormatSecondLength.Format);
                CopyData(ws.Cells["P" + row], data.SecondQuantity, data.FormatSecondQuantity.Format);
                CopyData(ws.Cells["Q" + row], data.SecondWeight, data.FormatSecondWeight.Format);
                CopyData(ws.Cells["R" + row], data.ParentRow);
                CopyData(ws.Cells["S" + row], rowindex);
                row++;
                roll++;
                rowindex++;
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
                                CellWeight = currentWorksheet.Cells[rowNumber, 8],
                            };

                            newRow.FormatNo = newRow.CellNo.Style.Numberformat;
                            newRow.FormatProject = newRow.CellProject.Style.Numberformat;
                            newRow.FormatPoNo = newRow.CellPoNo.Style.Numberformat;
                            newRow.FormatItemCategory = newRow.CellItemCategory.Style.Numberformat;
                            newRow.FormatDiameter = newRow.CellDiameter.Style.Numberformat;
                            newRow.FormatLength = newRow.CellLength.Style.Numberformat;
                            newRow.FormatQuantity = newRow.CellQuantity.Style.Numberformat;
                            newRow.FormatWeight = newRow.CellWeight.Style.Numberformat;
                            
                            GetModelRequest(newRow);
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