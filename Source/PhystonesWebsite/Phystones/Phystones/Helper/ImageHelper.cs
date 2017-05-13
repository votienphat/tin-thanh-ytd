using System;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;

namespace Phystones.Helper
{
    public static class ImageHelper
    {
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
        public static Image Base64ToImage(string base64String)
        {
            string mess = "";
            byte[] imageBytes = Base64ToByte(base64String,out mess);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
        public static string GetFileExtension(string base64String)
        {
            string[] words = base64String.Split('/');
            var text = words[1];
            var ext = text.Split(';');
            return ext[0];
        }


    }
}