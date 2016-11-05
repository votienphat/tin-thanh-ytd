using System;
using System.Text.RegularExpressions;
using MyAdmin.Models;
using MyAdmin.Models.Content;
using MyAdmin.UploadFileService;

namespace MyAdmin.Helper
{
    public static class ImageHelper
    {
        /// <summary>
        /// QuangPN
        /// Đổi base 64 thành byte 
        /// <para>data:image/png;base64,iVBORw0KGgoAAAANSUhEUg</para>
        /// <para>ex png|jpg|gif...</para>
        /// </summary>
        /// <returns></returns>
        public static byte[] Base64ToByte(string base64String, out string ex)
        {
            try
            {
                #region Get string

                var base64 = "";
                ex = "";
                if (base64String.IndexOf("base64,", StringComparison.Ordinal) > 0)
                {
                    var pos = base64String.IndexOf("base64,", StringComparison.Ordinal) + 7;
                    base64 = base64String.Substring(pos);
                    var regex = new Regex(@"data:image\/(.*?);base64");
                    var v = regex.Match(base64String);
                    ex = v.Groups[1].ToString().ToLower();
                }
                #endregion

                byte[] bytes = Convert.FromBase64String(base64);
                return bytes;
            }
            catch (Exception)
            {
                ex = null;
                return null;
            }
        }


        /// <summary>
        /// QuangPN
        /// <para></para>
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="path"></param>
        /// <param name="type">TypeImageUploadEnum</param>
        /// <returns></returns>
        public static UploadFileResponse SaveImageToService(FileUploadSvc.TypeImageUploadEnum type, byte[] bytes, string path)
        {
            try
            {
                var upload = new FileUploadSvc.FileUploadSvc();
                var rs = upload.UploadImage(type, path, bytes);
                return rs.IsSuccess ? new UploadFileResponse { Code = UploadFileResponseCode.Success, Path = rs.ImagePath } :
                    new UploadFileResponse { Code = UploadFileResponseCode.Fail, Message = rs.Message };

            }
            catch (Exception e)
            {
                return new UploadFileResponse { Code = UploadFileResponseCode.Fail, Message = e.Message };
            }
        }

      


        /// <summary>
        /// QuangPN
        /// <para>Lưu avatar</para>
        /// </summary>
        /// <param name="path">vd: Avatar/flower.jpg</param>
        /// <returns></returns>
        public static bool DeleteImage(string path)
        {
            var upload = new FileUploadSvc.FileUploadSvc();
            return upload.DeleteImage(path);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="imgPath"></param>
        /// <param name="attr">ex: "class='abc' id='abcd'"</param>
        /// <returns></returns>
        public static string CreateImageOrFlash(this string imgPath, string alt = "", string attr = "")
        {
            //if (imgPath.ToLower().EndsWith(".swf"))
            {
                // ReSharper disable once ConvertToConstant.Local
                string ob = @"  <object data='{0}' {1}>
                            <param value='transparent' name='wmode'>
                            <param name='allowFullScreen' value='true'>
                            <param name='allowScriptAccess' value='always'>
                            <param name='AllowNetworking' value='all'>
                        </object>";
                return string.Format(ob, imgPath, attr);
            }
            var str = string.Format("<img src='{0}'  {1} alt='{2}'  />", imgPath, attr, alt);
            return str;
        }
       

        public static string CreateUploadImageBase64(string name, string imgurl = "", int width = 80, int height = 80)
        {
            const string str = @"<div class='base64img' id='base64img_{0}'>
                                    <img id='img_{0}' src='{3}' width='{1}' height='{2}' alt='noimage' />
                                    <button class='btn btn-primary btn-xs' type='button'>Chọn hình</button>
                                    <span></span>
                                    <input type='file' id='file_{0}' />
                                    <input type='hidden' value='' id='hf_{0}' name='{0}' />
                                </div>";

            return string.Format(str, name, width, height, imgurl);
        }
    }
}