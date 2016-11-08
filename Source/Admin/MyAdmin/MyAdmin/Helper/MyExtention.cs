using System;
using System.Globalization;
using System.Web.Mvc;
using MyConfig;

namespace MyAdmin.Helper
{
    public static class MyExtention
    {
        public static double ConvertTimestamp(DateTime date)
        {
            var span = (date - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            return Math.Round((double)span.TotalSeconds, 0);
        }
        public static DateTime ToDateTimeParseExact(this string text, string format = "dd/MM/yyyy HH:mm:ss")
        {
            return DateTime.ParseExact(text, format, CultureInfo.InvariantCulture);
        }

        public static string ToString(this DateTime? date, string format, string _default = "")
        {
            if (date.HasValue)
                return date.Value.ToString(format);
            else return _default;
        }
  
        public static DateTime? ToDateTimeParseExact(this string text, DateTime? defaultError, string format = "dd/MM/yyyy HH:mm:ss")
        {
            try
            {
                return DateTime.ParseExact(text, format, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return defaultError;
            }
        }

        public static string FullImagePath(this string path) 
        {
            if (!string.IsNullOrEmpty(path) && (path.Contains("http://") || path.Contains("https://")))
            {
                return path;
            }
            return MyConfig.MyConfiguration.Default.ImageHost + path;
        }

        /// <summary>
        /// 1234 => 1,234
        /// </summary>
        /// <param name="number"></param>
        /// <param name="isDec">Có hiển thị số thập phân không</param>
        /// <returns></returns>
        public static string ToFormat(this int number, bool isDec = false)
        {
            if (isDec)
            {
                return number == 0 ? "0" : string.Format("{0:#,#0.#}", number);
            }
            return number == 0 ? "0" : string.Format("{0:#,#0}", number);
        }
        public static string ToFormat(this double number, bool isDec = false)
        {
            if (isDec)
            {
                return number == 0 ? "0" : string.Format("{0:#,#0.#}", number);
            }
            return number == 0 ? "0" : string.Format("{0:#,#0}", number);
        }

      

       

    
        public static string ToFormatMoney(this decimal? money)
        {
            if (money.HasValue)
                return string.Format("{0:#,0#}", money);
            return "0";
        }
        public static string ToFormatMoney(this decimal money)
        {
            return string.Format("{0:#,0#}", money);
        }
        public static string ToFormatMoney(this int? money)
        {
            if (money.HasValue)
                return string.Format("{0:#,0#}", money);
            return "0";
        }

        public static string FormatGold(this decimal pCoin)
        {
            var lCoin = (long)pCoin;
            string strRet = String.Format(new CultureInfo("vi-VN"), "{0:0,0}", lCoin);
            if (strRet == "00") { strRet = "0"; }
            return strRet;
        }

        public static string FormatCoin(this int? pCoin)
        {
            var lCoin = (long)pCoin;
            string strRet = String.Format(new CultureInfo("vi-VN"), "{0:0,0}", lCoin);
            if (strRet == "00") { strRet = "0"; }
            return strRet;
        }

        public static decimal ToDecimal(this string str, decimal _default = 0)
        {
            try
            {
                return decimal.Parse(str);
            }
            catch (Exception)
            {
                return _default; 
            }
        }
        #region AtTheMomentDate

        /// <summary>
        /// Author: QuangPN
        /// <para>Date: 7-1-2015</para>
        /// <para>(DateTime.Now - value) > 1h 35</para>
        /// </summary>
        public static string AtTheMomentDate(this DateTime date, string hh = "h", string mm = "")
        {
            TimeSpan time = DateTime.Now - date;
            if (time.TotalHours < 24)
            {
                return string.Format("{0}{1} {2}{3}", time.Hours, hh, time.ToString("mm"), mm); // time.ToString(@"hhhmm");
            }
            return date.ToString("dd/MM/yyyy");
        }

        public static string AtTheMomentDate(this DateTime? date, string hh = "h", string mm = "")
        {
            if (!date.HasValue)
                return "";

            return AtTheMomentDate(date.Value, hh, mm);
        }


        
        #endregion


        public static string FullAvatarPath(this string imgPath)
        {
            if (imgPath.Contains("http://") || imgPath.Contains("https://"))
            {
                return imgPath;
            }
            return MyConfiguration.Default.ImageHost + imgPath;
        }

        public static string FullImgPath(this string imgPath)
        {
            //if (string.IsNullOrEmpty(imgPath))
            //    return MyConfiguration.Default.ImageHost + MyConfiguration.Default.DefaultAvatar;
            if (imgPath.Contains("http://") || imgPath.Contains("https://"))
            {
                return imgPath;
            }
            return MyConfiguration.Default.ApiFullDomain + imgPath;
        }
    }
}