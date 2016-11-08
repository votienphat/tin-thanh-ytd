using System.Web.Mvc;
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

                var listExport = new List<ExcelExport>();
                foreach (var item in importResult.ImportDataExcel.ToList())
                {
                    var rowExport = new ExcelExport();
                    var RowCurent = CalcuRow(listExport, importResult.ImportDataExcel, item.No,out List<ExcelCalExport> listdataOut);
                    if (listdataOut.Any())
                    {
                        rowExport.No = listExport.Count + 1;
                        rowExport.PoNo = item.PoNo;
                        rowExport.Project = item.Project;
                        rowExport.ItemCategory = item.ItemCategory;
                        rowExport.Diameter = item.Diameter;
                        rowExport.Leght = item.Leght;
                        rowExport.Quantity = item.QuantityFist;
                        listExport.Add(rowExport);
                    }
                    rowExport.No = item.No;
                    rowExport.PoNo = item.PoNo;
                    rowExport.Project = item.Project;
                    rowExport.ItemCategory = item.ItemCategory;
                    rowExport.Diameter = item.Diameter;
                    rowExport.Leght = item.Leght;
                    rowExport.Quantity = item.Quantity;
                    rowExport.Weight = item.Weight;
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

        public static ExcelCalExport CalcuRow(List<ExcelExport> listdata, List<ExcelModel> Data, string noRow, out List<ExcelCalExport> listdataOut)
        {
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
                        if (int.Parse(item.LeghtFist) > CurentRow.Leght)
                        {
                            // kiểm tra số lượng và dòng cần tách
                            int Quantity = CurentRow.Quantity;
                            for (int i = 1; i <= Quantity; i++)
			                {
			                    if(CurentRow.Leght * i > int.Parse(item.LeghtFist))
                                {
                                    ObjTachDong.QuantityFist = i;
                                    listdataOut.Add(ObjTachDong);
                                }
			                }

                            returnData.ParentRow = item.No;
                            returnData.LeghtSecond = CurentRow.Leght;
                            returnData.DiameterSecond = CurentRow.Diameter;
                            returnData.QuantitySecond = CurentRow.Quantity;
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