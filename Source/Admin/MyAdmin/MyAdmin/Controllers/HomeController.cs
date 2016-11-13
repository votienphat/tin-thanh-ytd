using System.Web.Mvc;
using MyAdmin.ActionFilter;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Linq;
using System;
using MyAdmin.Helpers;
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
        public ActionResult ImportExcel(IEnumerable<HttpPostedFileBase> files)
        {
            DataExcel importResult = null;
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
                importResult = excelHelpers.ImportDataExcel(path);
                var listdataOut = new ExcelCalExport();
                var listExport = new List<ExcelExport>();
                var datalist = importResult.ImportDataExcel;
                for (int i = 0; i < datalist.Count(); i++)
                 {
                    var rowExport = new ExcelExport();
                    var RowCurent = CalcuRow(listExport, datalist, datalist[i].No, out listdataOut);
                    if (listdataOut.FirstQuantity > 0)
                    {
                        var rowExportOut = new ExcelExport();
                        var RowFor = new ExcelModel();
                        int maxRow = datalist.Max(x => x.No) + 1;
                        rowExportOut.No = maxRow;
                        rowExportOut.PoNo = datalist[i].PoNo;
                        rowExportOut.Project = datalist[i].Project;
                        rowExportOut.ItemCategory = datalist[i].ItemCategory;
                        rowExportOut.Diameter = datalist[i].Diameter;
                        rowExportOut.Length = datalist[i].Length;
                        rowExportOut.Quantity = listdataOut.SecondQuantity.GetValueOrDefault();
                        listExport.Add(rowExportOut);

                        RowFor.No = maxRow;
                        RowFor.Project = datalist[i].Project;
                        RowFor.PoNo = datalist[i].PoNo;
                        RowFor.ItemCategory = datalist[i].ItemCategory;
                        RowFor.Diameter = datalist[i].Diameter;
                        RowFor.Length = datalist[i].Length;
                        RowFor.Quantity = listdataOut.SecondQuantity.GetValueOrDefault();
                        RowFor.Weight = datalist[i].Weight;
                        datalist.Add(RowFor);

                        rowExport.Quantity = listdataOut.FirstQuantity.GetValueOrDefault();

                    }
                    else
                    {
                        rowExport.Quantity = datalist[i].Quantity;
                    }
                    rowExport.No = datalist[i].No;
                    rowExport.PoNo = datalist[i].PoNo;
                    rowExport.Project = datalist[i].Project;
                    rowExport.ItemCategory = datalist[i].ItemCategory;
                    rowExport.Diameter = datalist[i].Diameter;
                    rowExport.Length = datalist[i].Length;
                    rowExport.Weight = datalist[i].Weight;
                    rowExport.FirstDiameter = RowCurent.FirstDiameter;
                    rowExport.FirstLength = RowCurent.FirstLength;
                    rowExport.FirstCutLength = RowCurent.FirstCutLength;
                    rowExport.FirstQuantity = RowCurent.FirstQuantity;
                    rowExport.FirstWeight = RowCurent.FirstWeight;

                    rowExport.SecondDiameter = RowCurent.SecondDiameter;
                    rowExport.SecondLength = RowCurent.SecondLength;
                    rowExport.SecondQuantity = RowCurent.SecondQuantity;
                    rowExport.SecondWeight = RowCurent.SecondWeight;
                    rowExport.ParentRow = RowCurent.ParentRow;


                    listExport.Add(rowExport);
                }

                if (importResult.ImportDataExcel.Count > 0)
                {
                    return ExportData(listExport);
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false, message = "" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ExportData(List<ExcelExport> dataExport)
        {
            ExcelHelpers exHelpers = new ExcelHelpers();
            var ImportPath = "~/App_Data/Excel/";
            if (!Directory.Exists(Server.MapPath(ImportPath)))
                Directory.CreateDirectory(Server.MapPath(ImportPath));
            var detailName = "Report.xlsx";
            var path = Path.Combine(Server.MapPath(ImportPath), detailName);
            exHelpers.ExportData(dataExport, "Danh Sách", new string[] { "No", "Project", "Po No", "Item Category", "Diameter mm", "Length m", "Qty nos", "Weight kg", "Diameter mm", "Length m", "FirstCutLength m", "Qty nos", "Weight kg", "Diameter mm", "Length m", "Qty nos", "Weight kg", "Parent" }, "ABCDEFGHIJKLMNOPQR")
                .SaveAs(new FileInfo(path));

            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = "Report.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public static ExcelCalExport CalcuRow(List<ExcelExport> listdata, List<ExcelModel> Data, int noRow, out ExcelCalExport listdataOut)
        {
            listdataOut = new ExcelCalExport();
            var returnData = new ExcelCalExport();
            var minTon = listdata.Where(x => x.FirstLength > 0).OrderBy(x => x.FirstLength).ToList();
            var CurentRow = Data.FirstOrDefault(x => x.No == noRow);
            var requireLeght = CurentRow.Quantity * CurentRow.Length;
            //kiểm tra có dư hay ko nếu không dư thì lấy thanh mặt định
            if (minTon.Any())
            {
                foreach (var item in minTon)
                {
                    // nếu có kho dư
                    // gán parent cho thanh sữ dụng
                    if (item.FirstLength >= requireLeght)
                    {
                        returnData.ParentRow = item.No;
                        returnData.SecondLength = CurentRow.Length;
                        returnData.SecondDiameter = CurentRow.Diameter;
                        returnData.SecondQuantity = CurentRow.Quantity;
                        int index = listdata.FindIndex(x => x.No == item.No);
                        listdata[index].FirstLength = item.FirstLength - CurentRow.Length;
                        return returnData;
                    }
                }
                // uu tiên không tách dòng trc nên for lại 1 lần nua
                // thực hiện tách dòng trong kho
                foreach (var item in minTon)
                {
                    if (item.FirstLength >= CurentRow.Length)
                    {
                        // kiểm tra số lượng và dòng cần tách
                        int Quantity = CurentRow.Quantity;
                        var checkQuantity = 1;
                        for (int i = 1; i <= Quantity; i++)
                        {
                            if (CurentRow.Length * i > item.FirstLength)
                            {
                                listdataOut.FirstQuantity = i;
                                checkQuantity = i - 1;
                                break;
                            }
                        }
                        returnData.ParentRow = item.No;
                        returnData.SecondLength = CurentRow.Length;
                        returnData.SecondDiameter = CurentRow.Diameter;
                        returnData.SecondQuantity = checkQuantity;
                        int index = listdata.FindIndex(x => x.No == item.No);
                        listdata[index].FirstLength = item.FirstLength - CurentRow.Length;
                        return returnData;
                    }
                }
                if (requireLeght <= LeghtDefaut)
                {
                    returnData.FirstLength = LeghtDefaut - CurentRow.Length;
                    returnData.FirstCutLength = LeghtDefaut - CurentRow.Length;
                    returnData.FirstDiameter = CurentRow.Diameter;
                    returnData.FirstQuantity = CurentRow.Quantity;
                    return returnData;
                }
                returnData.FirstLength = LeghtDefaut - CurentRow.Length;
                returnData.FirstCutLength = LeghtDefaut - CurentRow.Length;
                returnData.FirstDiameter = CurentRow.Diameter;
                returnData.FirstQuantity = CurentRow.Quantity;
                return returnData;

            }
            else
            {

                returnData.FirstLength = LeghtDefaut - CurentRow.Length;
                returnData.FirstCutLength = LeghtDefaut - CurentRow.Length;
                returnData.FirstDiameter = CurentRow.Diameter;
                returnData.FirstQuantity = CurentRow.Quantity;
                return returnData;

            }
            return returnData;
        }
    }
}