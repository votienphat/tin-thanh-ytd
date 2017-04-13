using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Text;
using System;
using MyConfig;

namespace MyAdmin.Helper
{
    public static class Common
    {
        public static int GetFristSortColumns(List<int> listCol)
        {
            if (listCol != null && listCol.Count > 0)
            {
                return listCol[0];
            }
            return 0;
        }
        public static string GetFristSortDir(List<string> listDir)
        {
            if (listDir != null && listDir.Count > 0)
            {
                return listDir[0];
            }
            return "asc";
        }

        public static string GetTextID(string title, int maxLength)
        {
            title = UnicodeToAscii(title);
            if (title.Length > maxLength)
                title = title.Substring(0, maxLength).Trim();

            if (title.EndsWith("-"))
                title = title.Substring(0, title.Length - 1);

            return title;
        }

        public static String UnicodeToAscii(String strUnicode)
        {
            var strB = new StringBuilder(strUnicode);

            string[] unicodeChar = {
                "\u1EF9", "\u1EF8", "\u1EF7", "\u1EF6", "\u1EF5", "\u1EF4",
				"\u1EF3", "\u1EF2", "\u1EF1", "\u1EF0", "\u1EEF", "\u1EEE", "\u1EED", "\u1EEC", "\u1EEB",
				"\u1EEA", "\u1EE9", "\u1EE8", "\u1EE7", "\u1EE6", "\u1EE5", "\u1EE4", "\u1EE3", "\u1EE2",
				"\u1EE1", "\u1EE0", "\u1EDF", "\u1EDE", "\u1EDD", "\u1EDC", "\u1EDB", "\u1EDA", "\u1ED9",
				"\u1ED8", "\u1ED7", "\u1ED6", "\u1ED5", "\u1ED4", "\u1ED3", "\u1ED2", "\u1ED1", "\u1ED0",
				"\u1ECF", "\u1ECE", "\u1ECD", "\u1ECC", "\u1ECB", "\u1ECA", "\u1EC9", "\u1EC8", "\u1EC7",
				"\u1EC6", "\u1EC5", "\u1EC4", "\u1EC3", "\u1EC2", "\u1EC1", "\u1EC0", "\u1EBF", "\u1EBE",
				"\u1EBD", "\u1EBC", "\u1EBB", "\u1EBA", "\u1EB9", "\u1EB8", "\u1EB7", "\u1EB6", "\u1EB5",
				"\u1EB4", "\u1EB3", "\u1EB2", "\u1EB1", "\u1EB0", "\u1EAF", "\u1EAE", "\u1EAD", "\u1EAC",
				"\u1EAB", "\u1EAA", "\u1EA9", "\u1EA8", "\u1EA7", "\u1EA6", "\u1EA5", "\u1EA4", "\u1EA3",
				"\u1EA2", "\u1EA1", "\u1EA0", "\u01B0", "\u01AF", "\u01A1", "\u01A0", "\u0169", "\u0168",
				"\u0129", "\u0128", "\u0111", "\u0103", "\u0102", "\u00FD", "\u00FA", "\u00F9", "\u00F5",
				"\u00F4", "\u00F3", "\u00F2", "\u00ED", "\u00EC", "\u00EA", "\u00E9", "\u00E8", "\u00E3",
				"\u00E2", "\u00E1", "\u00E0", "\u00DD", "\u00DA", "\u00D9", "\u00D5", "\u00D4", "\u00D3",
				"\u00D2", "\u0110", "\u00CD", "\u00CC", "\u00CA", "\u00C9", "\u00C8", "\u00C3", "\u00C2",
				"\u00C1", "\u00C0"
        };

            string[] asciiChar = {
                "y", "Y", "y", "Y", "y", "Y", "y", "Y", "u", "U", "u",
                "U", "u", "U", "u", "U", "u", "U", "u", "U", "u", "U", "o", "O",
                "o", "O", "o", "O", "o", "O", "o", "O", "o", "O", "o", "O", "o",
                "O", "o", "O", "o", "O", "o", "O", "o", "O", "i", "I", "i", "I", "e",
                "E", "e", "E", "e", "E", "e", "E", "e", "E", "e", "E", "e", "E", "e",
                "E", "a", "A", "a", "A", "a", "A", "a", "A", "a", "A", "a", "A",
                "a", "A", "a", "A", "a", "A", "a", "A", "a", "A", "a", "A", "u", "U",
                "o", "O", "u", "U", "i", "I", "d", "a", "A", "y", "u", "u", "o", "o", "o",
                "o", "i", "i", "e", "e", "e", "a", "a", "a", "a", "Y", "U", "U", "O", "O",
                "O", "O", "D", "I", "I", "E", "E", "E", "A", "A", "A", "A"
        };

            for (int i = 0; i < asciiChar.Length; i++)
                strB.Replace(unicodeChar[i], asciiChar[i]);

            string strInput = strB.ToString().ToLower();
            strB = new StringBuilder();

            foreach (char c in strInput)
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || (c == '-'))
                    strB.Append(c);
                else
                    strB.Append("-");

            return Regex.Replace(strB.ToString(), @"-+", "-");
        }

        /// <summary>
        /// Author: ThongNT
        /// <para>Date: 23-12-2014</para>
        /// <para>Get duong dan public cua file</para>
        /// </summary>
        /// <param name="urlHelper"></param>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public static string ContentVersion(this UrlHelper urlHelper, string relativePath)
        {
            return urlHelper.Content(relativePath) + "?v=" + MyConfiguration.Default.ContentVersion;
        }
    }
}
