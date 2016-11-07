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
        [HeaderAuthorizeFilter(IsCheckPermission = false)]
        public ActionResult Index()
        {

            return View();
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
        public JsonResult ImportExcel(IEnumerable<HttpPostedFileBase> files)
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
                    var RowCurent = CalcuRow(listExport, importResult.ImportDataExcel,item.No);
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
                ExportData(listExport);
                if (importResult.ImportDataExcel.Count > 0)
                {
                   
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
            exHelpers.ExportData(dataExport, "Danh Sách", new string[] { "No", "Project", "Po No", "Item Category", "Diameter", "Leght", "Qty", "Weight", "Diameter", "Leght", "LeghtFistCut", "Qty", "Weight", "Diameter", "Leght", "Qty", "Weight","Parent" }, "ABCDEFGHIJKLMNOPQR")
                .SaveAs(new FileInfo(path));
            return File(path, System.Net.Mime.MediaTypeNames.Application.Octet, detailName);
        }
        public static ExcelCalExport CalcuRow(List<ExcelExport> listdata,List<ExcelModel> Data,string noRow)
        {
            var returnData = new ExcelCalExport();
            var minTon = listdata.Where(x=>x.LeghtFist != null).OrderBy(x=>x.LeghtFist).ToList();
            var CurentRow = Data.FirstOrDefault(x => x.No == noRow);
            //kiểm tra có dư hay ko nếu không dư thì lấy thanh mặt định
             if (minTon.Any())
            {
                foreach (var item in minTon)
                {
                    // nếu có kho dư
                    // gán parent cho thanh sữ dụng
                    if (int.Parse(item.LeghtFist) > int.Parse( CurentRow.Leght))
                    {

                        //kiểm tra số lượng tương ứng
                        if (int.Parse(CurentRow.Quantity) > 1)
                        {
                            var requireLeght = int.Parse(CurentRow.Quantity) * int.Parse(CurentRow.Leght);
                            // nếu tổng chiều dài vượt mứt 
                            // 1 tìm trong kho dư xem có thanh nào phù hơp
                            // 2 lấy thanh mặc định xem có phù hợp ko
                            // 3 tiến hành chia nhỏ từng dòng
                            if(requireLeght > int.Parse(item.LeghtFist))
                            {
                                foreach (var itemTon in minTon)
                                {
                                    if (int.Parse(itemTon.LeghtFist) > requireLeght)
                                    {
                                        returnData.ParentRow = item.No;
                                        returnData.LeghtSecond = CurentRow.Leght;
                                        returnData.DiameterSecond = CurentRow.Diameter;
                                        returnData.QuantitySecond = CurentRow.Quantity;
                                        int indexTon = listdata.FindIndex(x => x.No == itemTon.No);
                                        listdata[indexTon].LeghtFist = (int.Parse(item.LeghtFist) - int.Parse(CurentRow.Leght)).ToString();
                                        break;
                                    }
                                }
                            }

                        }

                        returnData.ParentRow = item.No;
                        returnData.LeghtSecond = CurentRow.Leght;
                        returnData.DiameterSecond = CurentRow.Diameter;
                        returnData.QuantitySecond = CurentRow.Quantity;

                        int index = listdata.FindIndex(x => x.No == item.No);
                        listdata[index].LeghtFist = (int.Parse(item.LeghtFist) - int.Parse(CurentRow.Leght)).ToString();
                        break;

                    }
                    //có dư nhưng không tìm thấy thanh phù hợp thì lấy mặt định
                    else {
                        returnData.LeghtFist = (LeghtDefaut- int.Parse(CurentRow.Leght)).ToString();
                        returnData.LeghtFistCut = (LeghtDefaut - int.Parse(CurentRow.Leght)).ToString();
                        returnData.DiameterFist = CurentRow.Diameter;
                        returnData.QuantityFist = CurentRow.Quantity;
                    }
                }

            } else {
                returnData.LeghtFist = (LeghtDefaut - int.Parse(CurentRow.Leght)).ToString();
                returnData.LeghtFistCut = (LeghtDefaut - int.Parse(CurentRow.Leght)).ToString();
                returnData.DiameterFist = CurentRow.Diameter;
                returnData.QuantityFist = CurentRow.Quantity;

            }
            return returnData;
        }
    }
}