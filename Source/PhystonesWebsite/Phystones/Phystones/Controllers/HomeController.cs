using System.IO;
using System.Web.Mvc;
using BusinessObject.WebModule.Contract;
using Phystones.Models.Enum;
using MyUtility.Extensions;
using System.Linq;
using Microsoft.Office.Interop.Word;
using Phystones.Models.Article;
using Newtonsoft.Json;
using Phystones.Enums;


namespace Phystones.Controllers
{
    public class HomeController : WebBaseController
    {
        #region Variables

        private IWebBusiness _webBusiness;
        public HomeController(IWebBusiness webBusiness)
        {
            _webBusiness = webBusiness;
        }

        #endregion

        public ActionResult Index()
        {
            ////Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            ////Microsoft.Office.Interop.Word.Document doc = app.Documents.Open(Server.MapPath("~/Content/Upload/DigitalSignature/template.docx"));
            ////object missing = System.Reflection.Missing.Value;
            ////doc.Content.Text += "bbbbbbb";
            ////doc.Save();

            //Application app = new Application();
            ////app.Visible = false;
            ////app.ScreenUpdating = false;
            //object oMissing = System.Reflection.Missing.Value;

            //Document doc = app.Documents.Open(Server.MapPath("~/Content/Upload/DigitalSignature/xxxx.docx"),
            //    ref oMissing, ref oMissing,
            //    ref oMissing, ref oMissing, ref oMissing, ref oMissing,
            //    ref oMissing, ref oMissing, ref oMissing, ref oMissing,
            //    ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            //doc.Activate();


            //object outputFileName = doc.FullName.Replace(".doc", ".pdf").Replace(".docx", ".pdf");
            //object fileFormat = WdSaveFormat.wdFormatPDF;

            //// Save document into PDF Format
            //doc.SaveAs(ref outputFileName,
            //    ref fileFormat, ref oMissing, ref oMissing,
            //    ref oMissing, ref oMissing, ref oMissing, ref oMissing,
            //    ref oMissing, ref oMissing, ref oMissing, ref oMissing,
            //    ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            //object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
            //((_Document)doc).Close(ref saveChanges, ref oMissing, ref oMissing);

            ////Word2Pdf objWorPdf = new Word2Pdf();
            ////string backfolder1 = Server.MapPath("~/Content/Upload/DigitalSignature/");
            ////string strFileName = "template.docx";
            ////object FromLocation = Path.Combine(backfolder1, strFileName);
            ////string FileExtension = Path.GetExtension(strFileName);
            ////string ChangeExtension = strFileName.Replace(FileExtension, ".pdf");
            ////if (FileExtension == ".doc" || FileExtension == ".docx")
            ////{
            ////    object ToLocation = Path.Combine(backfolder1, ChangeExtension);
            ////    objWorPdf.InputLocation = FromLocation;
            ////    objWorPdf.OutputLocation = ToLocation;
            ////    objWorPdf.Word2PdfCOnversion();
            ////}


            var plain = _webBusiness.GetByCategoryId(CategoryArticleEnum.Plain.Value());
            var resultplain = plain.Select(x => new ArticleViewModel
            {
                Title = x.Title,
                Image = x.Image,
                Link = x.TextId,
                Id = x.Id,
            }).ToList();
            var slogan = _webBusiness.SloganGet();
            ViewBag.Plain = resultplain;
            ViewBag.Slogan = slogan;
            return View();
        }
    }
}