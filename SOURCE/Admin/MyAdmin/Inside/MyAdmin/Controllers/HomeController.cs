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
    }
}