using System.Web.Mvc;
using MyAdmin.ActionFilter;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Linq;
using System;
using Logger;
using MyAdmin.Helper;
using MyAdmin.Models.Home;
using MyUtility.Extensions;

namespace MyAdmin.Controllers
{
    public class HomeController : BaseController
    {
        public static int LenghtDefaut = 12000;
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
        public ActionResult ImportExcel2(IEnumerable<HttpPostedFileBase> files, int rowData, int offset)
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
                var importResult = excelHelpers.ImportDataExcel(path, rowData);

                var listExport = new List<ExcelExport>();
                var listImport = importResult.ImportDataExcel;

                for (int i = 0; i < listImport.Count; i++)
                {
                    var rowExport = new ExcelExport();
                    ExcelCalExport newData;

                    var currentRow = CalcuRow(listExport, listImport, listImport[i].No, out newData, offset);
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

                    if (newData.FirstQuantity > 0)
                    {
                        int maxRow = listImport.Max(x => x.No) + 1;
                        //var rowExportOut = new ExcelExport
                        //{
                        //    No = maxRow,
                        //    PoNo = listImport[i].PoNo,
                        //    Project = listImport[i].Project,
                        //    ItemCategory = listImport[i].ItemCategory,
                        //    Diameter = listImport[i].Diameter,
                        //    Length = listImport[i].Length,
                        //    Quantity = newData.SecondQuantity.GetValueOrDefault(),

                        //    FormatNo = currentRow.FormatNo,
                        //    FormatPoNo = currentRow.FormatPoNo,
                        //    FormatProject = currentRow.FormatProject,
                        //    FormatItemCategory = currentRow.FormatItemCategory,
                        //    FormatDiameter = currentRow.FormatDiameter,
                        //    FormatLength = currentRow.FormatLength,
                        //    FormatQuantity = currentRow.FormatQuantity,
                        //    FormatWeight = currentRow.FormatWeight,

                        //    FormatFirstDiameter = currentRow.FormatDiameter,
                        //    FormatFirstLength = currentRow.FormatLength,
                        //    FormatFirstQuantity = currentRow.FormatQuantity,
                        //    FormatFirstWeight = currentRow.FormatWeight,
                        //    FormatFirstCutLength = currentRow.FormatLength,

                        //    FormatSecondDiameter = currentRow.FormatDiameter,
                        //    FormatSecondLength = currentRow.FormatLength,
                        //    FormatSecondQuantity = currentRow.FormatQuantity,
                        //    FormatSecondWeight = currentRow.FormatWeight
                        //};
                        //listExport.Add(rowExportOut);

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
                }

                if (importResult.ImportDataExcel.Count > 0)
                {
                    return ExportData(listExport, rowData);
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false, message = "" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ExportData(List<ExcelExport> dataExport, int rowData)
        {
            ExcelHelpers exHelpers = new ExcelHelpers();
            var ImportPath = "~/App_Data/Excel/";
            if (!Directory.Exists(Server.MapPath(ImportPath)))
                Directory.CreateDirectory(Server.MapPath(ImportPath));
            var detailName = "Report.xlsx";
            var path = Path.Combine(Server.MapPath(ImportPath), detailName);
            exHelpers.ExportData(dataExport, "Danh Sách", new string[] { "No", "Project", "Po No", "Item Category", "Diameter mm", "Length m", "Qty nos", "Weight kg", "Diameter mm", "Length m", "FirstCutLength m", "Qty nos", "Weight kg", "Diameter mm", "Length m", "Qty nos", "Weight kg", "Parent","STT" }, "ABCDEFGHIJKLMNOPQRS", rowData)
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
                                listdataOut.SecondQuantity = checkQuantity;
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
                if (requireLength <= LenghtDefaut)
                {
                    returnData.FirstLength = LenghtDefaut - currentRow.Length;
                    returnData.FirstCutLength = LenghtDefaut - currentRow.Length;
                    returnData.FirstDiameter = currentRow.Diameter;
                    returnData.FirstQuantity = currentRow.Quantity;
                    return returnData;
                }
                returnData.FirstLength = LenghtDefaut - currentRow.Length;
                returnData.FirstCutLength = LenghtDefaut - currentRow.Length;
                returnData.FirstDiameter = currentRow.Diameter;
                returnData.FirstQuantity = currentRow.Quantity;
                return returnData;

            }
            else
            {

                returnData.FirstLength = LenghtDefaut - currentRow.Length;
                returnData.FirstCutLength = LenghtDefaut - currentRow.Length;
                returnData.FirstDiameter = currentRow.Diameter;
                returnData.FirstQuantity = currentRow.Quantity;
                return returnData;

            }
        }

        #region New

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ImportExcel(IEnumerable<HttpPostedFileBase> files, int rowData = 8, int offset = 0, string exclude = "")
        {
            var ImportPath = "~/App_Data/Excel/";
            try
            {
                if (!Directory.Exists(Server.MapPath(ImportPath)))
                    Directory.CreateDirectory(Server.MapPath(ImportPath));
                var httpPostedFileBases = files as HttpPostedFileBase[] ?? files.ToArray();

                var uploadFileName =
                    Path.GetFileName(DateTime.Now.ToString("ddMMyyhhmm") + "_" + httpPostedFileBases.First().FileName);
                var downloadFileName = "Export_" + uploadFileName;
                var path = Path.Combine(Server.MapPath(ImportPath), uploadFileName);
                httpPostedFileBases.First().SaveAs(path);
                ExcelHelpers excelHelpers = new ExcelHelpers();
                var importResult = excelHelpers.ImportDataExcel(path, rowData);

                var listImport = importResult.ImportDataExcel;
                // Lấy danh sách Diameter bỏ qua
                var tmpExcludeDiameters = exclude.Split(',');
                var excludeDiameters = new List<int>();
                foreach (var tmpExcludeDiameter in tmpExcludeDiameters)
                {
                    int diameter;
                    if (int.TryParse(tmpExcludeDiameter.Trim(), out diameter))
                    {
                        if (!excludeDiameters.Exists(x=>x == diameter))
                        {
                            excludeDiameters.Add(diameter);
                        }
                    }
                }

                int length = listImport.Count;
                for (int i = 0; i < length; i++)
                {
                    var item = listImport[i];
                    if (excludeDiameters.Exists(x=>x== item.Diameter))
                    {
                        continue;
                    }

                    var count = Calculate(listImport[i], listImport, i, excludeDiameters, offset);
                    length += count;
                    i += count;
                }

                if (listImport.Count > 0)
                {
                    return ExportData2(listImport, rowData, downloadFileName);
                }
            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.Error("ImportExcel", ex);
            }
            return Json(new { status = false, message = "" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Tính toán và trả về số dòng cộng thêm
        /// </summary>
        /// <param name="targetRow"></param>
        /// <param name="importList"></param>
        /// <param name="targetIndex"></param>
        /// <param name="excludeDiameters"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        private int Calculate(ExcelModel targetRow, List<ExcelModel> importList, int targetIndex, 
            List<int> excludeDiameters, int offset = 0)
        {
            int result = 0;

            if (targetRow == null || targetRow.Quantity <= 0 || offset < 0)
                return result;

            // Xử lý từng cây
            ExcelModel sourceRow = null;
            bool isRecycle = false;

            // Tìm dòng dữ liệu để tái sử dụng
            int sourceIndex = 0;
            for (int i = 0; i < targetIndex; i++)
            {
                var row = importList[i];

                if (excludeDiameters.Exists(x => x == row.Diameter))
                {
                    continue;
                }

                // Dữ liệu thỏa điều kiện: Chiều dài còn lại phải lớn hơn chiều dài cần sử dụng
                if (row.Diameter == targetRow.Diameter && row.FirstCutLength >= targetRow.Length + offset)
                {
                    // Nếu trước đó đã tìm ra dữ liệu phù hợp nhưng chiều dài nó lại chưa tối ưu
                    // thì lấy cái có chiều dài tối ưu hơn, tức là nhỏ nhất
                    if (sourceRow == null || sourceRow.FirstCutLength > row.FirstCutLength)
                    {
                        sourceRow = row;
                        isRecycle = true;
                        sourceIndex = i;
                    }
                }

                // Nếu tới dòng hiện tại thì dừng, không tìm nữa
                if (row.Id == targetRow.Id)
                {
                    break;
                }
            }

            if (isRecycle)
            {
                // Nếu là tái sử dụng thì lưu bên bộ Second

                // Ở đây có 3 case
                // Case 1: Quantity của source và quantity target bằng nhau
                //         Sau khi xử lý là dừng vòng lặp quantity
                // Case 2: Quantity của source lớn hơn target - Tách cái source ra thành 2 dòng khác nhau
                //         Sau khi xử lý là dừng vòng lặp quantity
                // Case 3: Quantity của source nhỏ hơn target - Lấy hết số source và tách target
                if (sourceRow.Quantity == targetRow.Quantity)
                {
                    targetRow.SecondDiameter = targetRow.Diameter;
                    targetRow.SecondQuantity = targetRow.Quantity;
                    targetRow.SecondWeight = targetRow.Weight;
                    targetRow.SecondLength = targetRow.Length;
                    targetRow.ParentPoNo = sourceRow.PoNo;
                    targetRow.ParentId = sourceRow.Id;

                    sourceRow.FirstCutLength -= targetRow.Length;
                }
                else if (sourceRow.Quantity > targetRow.Quantity)
                {
                    targetRow.SecondDiameter = targetRow.Diameter;
                    targetRow.SecondQuantity = targetRow.Quantity;
                    targetRow.SecondWeight = targetRow.Weight;
                    targetRow.SecondLength = targetRow.Length;
                    targetRow.ParentPoNo = sourceRow.PoNo;
                    targetRow.ParentId = sourceRow.Id;

                    var newSource = new ExcelModel(sourceRow);
                    newSource.Quantity = sourceRow.Quantity - targetRow.Quantity;
                    sourceRow.Quantity = targetRow.Quantity;
                    sourceRow.FirstCutLength -= targetRow.Length;
                    importList.Insert(sourceIndex + 1, newSource);

                    result++;
                }
                else
                {
                    var newTarget = new ExcelModel(targetRow);
                    newTarget.Quantity = targetRow.Quantity - sourceRow.Quantity;
                    importList.Insert(targetIndex + 1, newTarget);

                    targetRow.SecondDiameter = targetRow.Diameter;
                    targetRow.SecondQuantity = targetRow.Quantity;
                    targetRow.SecondWeight = targetRow.Weight;
                    targetRow.SecondLength = targetRow.Length;
                    targetRow.Quantity = sourceRow.Quantity;
                    targetRow.ParentPoNo = sourceRow.PoNo;
                    targetRow.ParentId = sourceRow.Id;

                    sourceRow.FirstCutLength -= targetRow.Length;
                }
            }
            else
            {
                // Nếu là mới thì lưu bên bộ first
                targetRow.FirstDiameter = targetRow.Diameter;
                targetRow.FirstQuantity = targetRow.Quantity;
                targetRow.FirstWeight = targetRow.Weight;
                targetRow.FirstLength = targetRow.Length;
                targetRow.FirstCutLength = LenghtDefaut - targetRow.Length;
            }

            return result;
        }

        public ActionResult ExportData2(List<ExcelModel> dataExport, int rowData, string fileName)
        {
            ExcelHelpers exHelpers = new ExcelHelpers();
            var ImportPath = "~/App_Data/Excel/";
            if (!Directory.Exists(Server.MapPath(ImportPath)))
                Directory.CreateDirectory(Server.MapPath(ImportPath));
            var detailName = fileName;
            var path = Path.Combine(Server.MapPath(ImportPath), detailName);
            exHelpers.ExportData2(dataExport, "Danh Sách", new[] { "No", "Project", "Po No", "Item Category", "Diameter mm", "Length m", "Qty nos", "Weight kg", "Diameter mm", "Length m", "FirstCutLength m", "Qty nos", "Weight kg", "Diameter mm", "Length m", "Qty nos", "Weight kg", "Parent", "STT" }, "ABCDEFGHIJKLMNOPQRS", rowData)
                .SaveAs(new FileInfo(path));

            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        #endregion
    }
}