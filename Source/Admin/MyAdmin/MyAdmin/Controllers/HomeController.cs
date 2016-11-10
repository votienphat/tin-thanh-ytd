﻿using System.Web.Mvc;
using MyAdmin.ActionFilter;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Linq;
using System;
using MyAdmin.Helpers;
using MyAdmin.Models.DataExcel;

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
                    var RowCurent = CalcuRow(listExport, datalist, datalist[i].No,out listdataOut);
                    if (listdataOut.QuantityFist != null)
                    {
                        var rowExportOut = new ExcelExport();
                        var RowFor = new ExcelModel();
                        int maxRow = int.Parse(datalist.Max(x => x.No)) +1;
                        rowExportOut.No = (maxRow).ToString();
                        rowExportOut.PoNo = datalist[i].PoNo;
                        rowExportOut.Project = datalist[i].Project;
                        rowExportOut.ItemCategory = datalist[i].ItemCategory;
                        rowExportOut.Diameter = datalist[i].Diameter;
                        rowExportOut.Leght = datalist[i].Leght;
                        rowExportOut.Quantity = listdataOut.QuantityFist;
                        listExport.Add(rowExportOut);

                        RowFor.No = (maxRow).ToString();
                        RowFor.Project = datalist[i].Project;
                        RowFor.PoNo = datalist[i].PoNo;
                        RowFor.ItemCategory = datalist[i].ItemCategory;
                        RowFor.Diameter = datalist[i].Diameter;
                        RowFor.Leght = datalist[i].Leght;
                        RowFor.Quantity = listdataOut.QuantityFist;
                        RowFor.Weight = datalist[i].Weight;
                        importResult.ImportDataExcel.Add(RowFor);

                        rowExport.Quantity = listdataOut.QuantityFist;

                    }
                    else {
                        rowExport.Quantity = datalist[i].Quantity;
                    }
                    rowExport.No = datalist[i].No;
                    rowExport.PoNo = datalist[i].PoNo;
                    rowExport.Project = datalist[i].Project;
                    rowExport.ItemCategory = datalist[i].ItemCategory;
                    rowExport.Diameter = datalist[i].Diameter;
                    rowExport.Leght = datalist[i].Leght;
                    rowExport.Weight = datalist[i].Weight;
                    rowExport.DiameterFist = RowCurent.DiameterFist;
                    rowExport.LeghtFist = RowCurent.LeghtFist;
                    rowExport.LeghtFistCut = RowCurent.LeghtFistCut;
                    rowExport.QuantityFist = RowCurent.QuantityFist;
                    rowExport.WeightFist = RowCurent.WeightFist;

                    rowExport.DiameterSecond = RowCurent.DiameterSecond;
                    rowExport.LeghtSecond = RowCurent.LeghtSecond;
                    rowExport.QuantitySecond = RowCurent.QuantitySecond;
                    rowExport.WeightSecond = RowCurent.WeightSecond;
                    rowExport.ParentRow = RowCurent.ParentRow;


                    listExport.Add(rowExport);
                }

                if (importResult.ImportDataExcel.Count > 0)
                {
                    return ExportData(listExport);
                    return Json(new { status = true, Data = importResult, message = "", JsonRequestBehavior.AllowGet });
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
            exHelpers.ExportData(dataExport, "Danh Sách", new string[] { "No", "Project", "Po No", "Item Category", "Diameter", "Leght", "Qty", "Weight", "Diameter", "Leght", "LeghtFistCut", "Qty", "Weight", "Diameter", "Leght", "Qty", "Weight", "Parent" }, "ABCDEFGHIJKLMNOPQR")
                .SaveAs(new FileInfo(path));

            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = "Report.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

            return File(path, System.Net.Mime.MediaTypeNames.Application.Octet, detailName);
        }

        public static ExcelCalExport CalcuRow(List<ExcelExport> listdata, List<ExcelModel> Data, string noRow, out ExcelCalExport listdataOut)
        {
            listdataOut = new ExcelCalExport();
            var returnData = new ExcelCalExport();
            var minTon = listdata.Where(x=>x.LeghtFist != null).OrderBy(x=>x.LeghtFist).ToList();
            var CurentRow = Data.FirstOrDefault(x => x.No == noRow);
            var requireLeght = int.Parse(CurentRow.Quantity) * int.Parse(CurentRow.Leght);
            var ObjTachDong  = new ExcelCalExport();
            //kiểm tra có dư hay ko nếu không dư thì lấy thanh mặt định
            if(minTon.Any())
                {
                    foreach (var item in minTon)
                    {
                        // nếu có kho dư
                        // gán parent cho thanh sữ dụng
                        if (int.Parse(item.LeghtFist) > requireLeght)
                        {
                            returnData.ParentRow = item.No;
                            returnData.LeghtSecond = CurentRow.Leght;
                            returnData.DiameterSecond = CurentRow.Diameter;
                            returnData.QuantitySecond = CurentRow.Quantity;
                            int index = listdata.FindIndex(x => x.No == item.No);
                            listdata[index].LeghtFist = (int.Parse(item.LeghtFist) - int.Parse(CurentRow.Leght)).ToString();
                            return returnData;
                        }
                    }
                // uu tiên không tách dòng trc nên for lại 1 lần nua
                // thực hiện tách dòng trong kho
                    foreach (var item in minTon)
                    {
                        if (int.Parse(item.LeghtFist) > int.Parse(CurentRow.Leght))
                        {
                            // kiểm tra số lượng và dòng cần tách
                            int Quantity = int.Parse(CurentRow.Quantity);
                            var checkQuantity = 1;
                            for (int i = 1; i <= Quantity; i++)
			                {
			                    if(int.Parse(CurentRow.Leght) * i > int.Parse(item.LeghtFist))
                                {
                                    listdataOut.QuantityFist = i.ToString();
                                    checkQuantity = i - 1;
                                break;
                                }
			                }
                            returnData.ParentRow = item.No;
                            returnData.LeghtSecond = CurentRow.Leght;
                            returnData.DiameterSecond = CurentRow.Diameter;
                            returnData.QuantitySecond = checkQuantity.ToString();
                            int index = listdata.FindIndex(x => x.No == item.No);
                            listdata[index].LeghtFist = (int.Parse(item.LeghtFist) - int.Parse(CurentRow.Leght)).ToString();
                            return returnData;
                        }
                    }
                    if (requireLeght < LeghtDefaut) {
                        returnData.LeghtFist = (LeghtDefaut - int.Parse(CurentRow.Leght)).ToString();
                        returnData.LeghtFistCut = (LeghtDefaut - int.Parse(CurentRow.Leght)).ToString();
                        returnData.DiameterFist = CurentRow.Diameter;
                        returnData.QuantityFist = CurentRow.Quantity;
                        return returnData;
                    }

            } else {

                returnData.LeghtFist = (LeghtDefaut - int.Parse(CurentRow.Leght)).ToString();
                returnData.LeghtFistCut = (LeghtDefaut - int.Parse(CurentRow.Leght)).ToString();
                returnData.DiameterFist = CurentRow.Diameter;
                returnData.QuantityFist = CurentRow.Quantity;
                return returnData;

            }
            return returnData;
        }
    }
}