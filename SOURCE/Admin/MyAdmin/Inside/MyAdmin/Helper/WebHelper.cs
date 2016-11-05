using System;
using System.IO;
using System.Linq;
using System.Web;
using BusinessObject.Helper;
using Logger;
using MyAdmin.FileUploadSvc;
using MyConfig;
using MyUtility;
using MyUtility.Extensions;
using System.Web.Http;

namespace MyAdmin.Helper
{
    public class WebHelper
    {
        public static UploadImageResult UploadImage(string path, byte[] byteArray)
        {
            try
            {
                CommonLogger.DefaultLogger.Debug("UploadImage - Newname: " + path);

                if (!FileExtension.IsValidImage(byteArray))
                {
                    Logger.CommonLogger.DefaultLogger.Debug("UploadImage - Hinh khong hop le - path: " + path);
                    return new UploadImageResult { IsSuccess = false, Message = "Hình không hợp lệ" };
                }
                string _imageDir = "~/Content/File/ImageArtcle";
                var imgpath = _imageDir + "/" + path;
                var lastPos = path.LastIndexOf("/", System.StringComparison.Ordinal);
                var fileName =  path;

                // Kiem tra extension file
                if (!FileExtension.IsValidExtension(fileName))
                {
                    //File.WriteAllText(Server.MapPath("~\\exception.txt"), "UploadImage - TypeImageUploadEnum: " + type + " - Tên hình không hợp lệ - Newname: " + fileName);
                    Logger.CommonLogger.DefaultLogger.Debug("UploadImage - Ten hinh khong hop le - TypeImageUploadEnum: " + " - path: " + path + " - fileName: " + fileName);
                    return new UploadImageResult { IsSuccess = false, Message = "Hình không hợp lệ" };
                }

                var serverpath = MyConfiguration.Article.MapPath + path;
                //HttpContext.Current.Server.MapPath(imgpath)
                //var dir = Path.GetDirectoryName(serverpath);
                //if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                //{
                //    Directory.CreateDirectory(dir);
                //}

                // Optimize file
                byteArray = SimplePaletteQuantizerHelper.OptimizeImage(byteArray, path);

                var fs = new FileStream(serverpath, FileMode.Create, FileAccess.Write, FileShare.None);

                fs.BeginWrite(byteArray, 0, byteArray.Length, null, null);
                fs.Write(byteArray, 0, byteArray.Length);
                fs.Close();
                fs.Dispose();

                var response = new UploadImageResult { ImagePath = imgpath.StartsWith("~/") ? imgpath.Substring(2) : imgpath, IsSuccess = true };
                return response;
            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.Error("UploadImage", ex);
                return new UploadImageResult { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}