using System.Web.Mvc;
using MyAdmin.ActionFilter;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Linq;
using System;
using MyAdmin.Helper;
using MyAdmin.Models.Home;

namespace MyAdmin.Controllers
{
    public class HomeController : BaseController
    {
        public static int LeghtDefaut = 12000;
        public HomeController()
        {
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectToAction("TableData");
            //return View();
        }

        [AllowAnonymous]
        public ActionResult FormData()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult TableData()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ImportExcel(IEnumerable<HttpPostedFileBase> files, int RowData, int Offset)
        {
            var ImportPath = "~/App_Data/Excel/";
            try
            {
                if (!Directory.Exists(Server.MapPath(ImportPath)))
                    Directory.CreateDirectory(Server.MapPath(ImportPath));
                var httpPostedFileBases = files as HttpPostedFileBase[] ?? files.ToArray();
                var path = Path.Combine(Server.MapPath(ImportPath),
                Path.GetFileName(DateTime.Now.ToString("ddMMyy") + "_" + httpPostedFileBases.First().FileName));
                httpPostedFileBases.First().SaveAs(path);
                ExcelHelpers excelHelpers = new ExcelHelpers();
                var importResult = excelHelpers.ImportDataExcel(path, RowData);

                var listExport = new List<ExcelExport>();
                var listImport = importResult.ImportDataExcel;

                for (int i = 0; i < listImport.Count; i++)
                {
                    var rowExport = new ExcelExport();
                    ExcelCalExport newData;

                    var currentRow = CalcuRow(listExport, listImport, listImport[i].No, out newData, Offset);

                    if (newData.FirstQuantity > 0)
                    {
                        int maxRow = listImport.Max(x => x.No) + 1;
                        var rowExportOut = new ExcelExport
                        {
                            No = maxRow,
                            PoNo = listImport[i].PoNo,
                            Project = listImport[i].Project,
                            ItemCategory = listImport[i].ItemCategory,
                            Diameter = listImport[i].Diameter,
                            Length = listImport[i].Length,
                            Quantity = newData.SecondQuantity.GetValueOrDefault(),

                            FormatNo = currentRow.FormatNo,
                            FormatPoNo = currentRow.FormatPoNo,
                            FormatProject = currentRow.FormatProject,
                            FormatItemCategory = currentRow.FormatItemCategory,
                            FormatDiameter = currentRow.FormatDiameter,
                            FormatLength = currentRow.FormatLength,
                            FormatQuantity = currentRow.FormatQuantity,
                            FormatWeight = currentRow.FormatWeight,

                            FormatFirstDiameter = currentRow.FormatDiameter,
                            FormatFirstLength = currentRow.FormatLength,
                            FormatFirstQuantity = currentRow.FormatQuantity,
                            FormatFirstWeight = currentRow.FormatWeight,
                            FormatFirstCutLength = currentRow.FormatLength,

                            FormatSecondDiameter = currentRow.FormatDiameter,
                            FormatSecondLength = currentRow.FormatLength,
                            FormatSecondQuantity = currentRow.FormatQuantity,
                            FormatSecondWeight = currentRow.FormatWeight
                        };
                        listExport.Add(rowExportOut);

                        var rowFor = new ExcelModel
                        {
                            No = maxRow,
                            Project = listImport[i].Project,
                            PoNo = listImport[i].PoNo,
                            ItemCategory = listImport[i].ItemCategory,
                            Diameter = listImport[i].Diameter,
                            Length = listImport[i].Length,
                            Quantity = newData.SecondQuantity.GetValueOrDefault(),
                            Weight = listImport[i].Weight,

                            FormatNo = currentRow.FormatNo,
                            FormatPoNo = currentRow.FormatPoNo,
                            FormatProject = currentRow.FormatProject,
                            FormatItemCategory = currentRow.FormatItemCategory,
                            FormatDiameter = currentRow.FormatDiameter,
                            FormatLength = currentRow.FormatLength,
                            FormatQuantity = currentRow.FormatQuantity,
                            FormatWeight = currentRow.FormatWeight
                        };
                        listImport.Add(rowFor);

                        rowExport.Quantity = newData.FirstQuantity.GetValueOrDefault();

                    }
                    else
                    {
                        rowExport.Quantity = listImport[i].Quantity;
                    }

                    rowExport.No = listImport[i].No;
                    rowExport.PoNo = listImport[i].PoNo;
                    rowExport.Project = listImport[i].Project;
                    rowExport.ItemCategory = listImport[i].ItemCategory;
                    rowExport.Diameter = listImport[i].Diameter;
                    rowExport.Length = listImport[i].Length;
                    rowExport.Weight = listImport[i].Weight;
                    rowExport.FirstDiameter = currentRow.FirstDiameter;
                    rowExport.FirstLength = currentRow.FirstLength;
                    rowExport.FirstCutLength = currentRow.FirstCutLength;
                    rowExport.FirstQuantity = currentRow.FirstQuantity;
                    rowExport.FirstWeight = currentRow.FirstWeight;

                    rowExport.SecondDiameter = currentRow.SecondDiameter;
                    rowExport.SecondLength = currentRow.SecondLength;
                    rowExport.SecondQuantity = currentRow.SecondQuantity;
                    rowExport.SecondWeight = currentRow.SecondWeight;
                    rowExport.ParentRow = currentRow.ParentRow;

                    rowExport.FormatNo = currentRow.FormatNo;
                    rowExport.FormatPoNo = currentRow.FormatPoNo;
                    rowExport.FormatProject = currentRow.FormatProject;
                    rowExport.FormatItemCategory = currentRow.FormatItemCategory;
                    rowExport.FormatDiameter = currentRow.FormatDiameter;
                    rowExport.FormatLength = currentRow.FormatLength;
                    rowExport.FormatQuantity = currentRow.FormatQuantity;
                    rowExport.FormatWeight = currentRow.FormatWeight;

                    rowExport.FormatFirstDiameter = currentRow.FormatDiameter;
                    rowExport.FormatFirstLength = currentRow.FormatLength;
                    rowExport.FormatFirstQuantity = currentRow.FormatQuantity;
                    rowExport.FormatFirstWeight = currentRow.FormatWeight;
                    rowExport.FormatFirstCutLength = currentRow.FormatLength;

                    rowExport.FormatSecondDiameter = currentRow.FormatDiameter;
                    rowExport.FormatSecondLength = currentRow.FormatLength;
                    rowExport.FormatSecondQuantity = currentRow.FormatQuantity;
                    rowExport.FormatSecondWeight = currentRow.FormatWeight;


                    listExport.Add(rowExport);
                }

                if (importResult.ImportDataExcel.Count > 0)
                {
                    return ExportData(listExport, RowData);
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false, message = "" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ExportData(List<ExcelExport> dataExport, int RowData)
        {
            ExcelHelpers exHelpers = new ExcelHelpers();
            var ImportPath = "~/App_Data/Excel/";
            if (!Directory.Exists(Server.MapPath(ImportPath)))
                Directory.CreateDirectory(Server.MapPath(ImportPath));
            var detailName = "Report.xlsx";
            var path = Path.Combine(Server.MapPath(ImportPath), detailName);
            exHelpers.ExportData(dataExport, "Danh Sách", new string[] { "No", "Project", "Po No", "Item Category", "Diameter mm", "Length m", "Qty nos", "Weight kg", "Diameter mm", "Length m", "FirstCutLength m", "Qty nos", "Weight kg", "Diameter mm", "Length m", "Qty nos", "Weight kg", "Parent" }, "ABCDEFGHIJKLMNOPQR", RowData)
                .SaveAs(new FileInfo(path));

            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = "Report.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public static ExcelCalExport CalcuRow(List<ExcelExport> listExport, List<ExcelModel> listImport, int noRow, out ExcelCalExport listdataOut, int offset = 0)
        {
            listdataOut = new ExcelCalExport();
            var minTon = listExport.Where(x => x.FirstLength > 0).OrderBy(x => x.FirstLength).ToList();
            var currentRow = listImport.FirstOrDefault(x => x.No == noRow);
            var requireLength = currentRow.Quantity * (currentRow.Length + offset);

            var returnData = new ExcelCalExport
            {
                FormatNo = currentRow.FormatNo,
                FormatPoNo = currentRow.FormatPoNo,
                FormatProject = currentRow.FormatProject,
                FormatItemCategory = currentRow.FormatItemCategory,
                FormatDiameter = currentRow.FormatDiameter,
                FormatLength = currentRow.FormatLength,
                FormatQuantity = currentRow.FormatQuantity,
                FormatWeight = currentRow.FormatWeight
            };
            //kiểm tra có dư hay ko nếu không dư thì lấy thanh mặt định
            if (minTon.Any())
            {
                foreach (var item in minTon)
                {
                    // nếu có kho dư
                    // gán parent cho thanh sữ dụng
                    if (item.FirstLength >= requireLength)
                    {
                        returnData.ParentRow = item.PoNo;
                        returnData.SecondLength = currentRow.Length + offset;
                        returnData.SecondDiameter = currentRow.Diameter;
                        returnData.SecondQuantity = currentRow.Quantity;

                        int index = listExport.FindIndex(x => x.No == item.No);
                        listExport[index].FirstLength = item.FirstLength - (currentRow.Length + offset);
                        return returnData;
                    }
                }
                // uu tiên không tách dòng trc nên for lại 1 lần nua
                // thực hiện tách dòng trong kho
                foreach (var item in minTon)
                {
                    if (item.FirstLength >= currentRow.Length + offset)
                    {
                        // kiểm tra số lượng và dòng cần tách
                        int quantity = currentRow.Quantity;
                        var checkQuantity = 1;
                        for (int i = 1; i <= quantity; i++)
                        {
                            if ((currentRow.Length + offset) * i > item.FirstLength)
                            {
                                listdataOut.FirstQuantity = i;
                                checkQuantity = i - 1;
                                break;
                            }
                        }
                        returnData.ParentRow = item.PoNo;
                        returnData.SecondLength = currentRow.Length;
                        returnData.SecondDiameter = currentRow.Diameter;
                        returnData.SecondQuantity = checkQuantity;
                        int index = listExport.FindIndex(x => x.No == item.No);
                        listExport[index].FirstLength = item.FirstLength - currentRow.Length;
                        return returnData;
                    }
                }
                if (requireLength <= LeghtDefaut)
                {
                    returnData.FirstLength = LeghtDefaut - currentRow.Length;
                    returnData.FirstCutLength = LeghtDefaut - currentRow.Length;
                    returnData.FirstDiameter = currentRow.Diameter;
                    returnData.FirstQuantity = currentRow.Quantity;
                    return returnData;
                }
                returnData.FirstLength = LeghtDefaut - currentRow.Length;
                returnData.FirstCutLength = LeghtDefaut - currentRow.Length;
                returnData.FirstDiameter = currentRow.Diameter;
                returnData.FirstQuantity = currentRow.Quantity;
                return returnData;

            }
            else
            {

                returnData.FirstLength = LeghtDefaut - currentRow.Length;
                returnData.FirstCutLength = LeghtDefaut - currentRow.Length;
                returnData.FirstDiameter = currentRow.Diameter;
                returnData.FirstQuantity = currentRow.Quantity;
                return returnData;

            }
        }
    }
}