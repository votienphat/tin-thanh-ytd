using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;
using MyAdmin.FileUploadSvc;
using MyAdmin.Models;
using MyAdmin.Helper;
using MyAdmin.Models.Content;
using MyConfig;


namespace MyAdmin.Helper
{
    /// <summary>
    /// QuangPN
    /// </summary>
    public class HtmlFillter
    {

        #region Code củ move bên website củ qua
        #region config

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private static Regex _validAttributeOrTagNameRegEx = new Regex(@"^\w+$",
                                    RegexOptions.Compiled | RegexOptions.IgnoreCase);

        // ReSharper disable once InconsistentNaming
        private const string STR_RemoveHtmlAttributeRegexQuote =
                                        @"(?<=<)([^/>]+)(\s{0}=[""][\s\S\(\)]*?[""])([^/>]*)(?=/?>|\s)";

        // ReSharper disable once InconsistentNaming
        private const string STR_RemoveHtmlAttributeRegexSingle =
                                        @"(?<=<)([^/>]+)(\s{0}=['][\s\S\(\)]*?['])([^/>]*)(?=/?>|\s)";

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once InconsistentNaming
        private static string[] ARR_ReplaceHtmlEvent = { "javascript", "onabort", "onblur", "onchange",
														"onclick", "ondblclick", "onerror", "onfocus",
														"onkeydown", "onkeypress", "onkeyup", "onload",
														"onmousedown", "onmousemove", "onmouseout",
														"onmouseup", "onmouseup", "onreset", "onresize",
														"onselect", "onsubmit", "onunload" };

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once InconsistentNaming
        private static string[] ARR_ReplaceHtmlTag = { "script", "object", "embed" };

        // ReSharper disable once InconsistentNaming
        private const string Suffix_ReplaceHtmlTag = "safe";

        // ReSharper disable once InconsistentNaming
        public const string BRTag = "<br />";




        #endregion

        #region public method

        public static string RemoveHtmlAttribute(string input, string attributeName)
        {
            var replaceInput = input;

            #region remove cac event javascript

            if (_validAttributeOrTagNameRegEx.IsMatch(attributeName))
            {
                attributeName = "on.*?";

                var regOnEventQuote = new Regex(string.Format(STR_RemoveHtmlAttributeRegexQuote, attributeName),
                                                    RegexOptions.Compiled | RegexOptions.IgnoreCase |
                                                    RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
                var regOnEventSingle = new Regex(string.Format(STR_RemoveHtmlAttributeRegexSingle, attributeName),
                                                    RegexOptions.Compiled | RegexOptions.IgnoreCase |
                                                    RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);

                regOnEventQuote.Split(input);
                regOnEventSingle.Split(input);

                replaceInput = regOnEventQuote.Replace(replaceInput, ConcatText);
                replaceInput = regOnEventSingle.Replace(replaceInput, ConcatText);

                //return replaceInput;
            }
            else
            {
                throw new ArgumentException("Not a valid HTML attribute name", "attributeName");
            }

            #endregion

            var strRet = replaceInput;

            #region remove cac evtnt javascript theo ten chinh xac

            Regex regQuote = null, regSingle = null;
            var replaceTo = string.Empty;

            foreach (string attr in ARR_ReplaceHtmlEvent)
            {
                if (attr == "javascript")
                {
                    regQuote = new Regex(@"[""]" + attr + @":[\s\S]*?[""]\s*?");
                    regSingle = new Regex(@"[']" + attr + @":[\s\S]*?[']\s*?");
                    replaceTo = "\"#\"";
                }
                else if (attr.Substring(0, 2).ToLower() == "on")
                {
                    regQuote = new Regex(@"\s" + attr + @"=[""][\s\S\(\)]*?[""]");
                    regSingle = new Regex(@"\s" + attr + @"=['][\s\S\(\)]*?[']");
                    replaceTo = " ";
                }

                if (regQuote != null)
                {
                    strRet = regQuote.Replace(strRet, replaceTo);
                }
                if (regSingle != null)
                {
                    strRet = regSingle.Replace(strRet, replaceTo);
                }
            }

            #endregion

            return strRet;
        }

        public static string RemoveHtmlAttribute(string input)
        {
            return RemoveHtmlAttribute(input, "style");
        }

        public static string ReplaceHtmlAttribute(string input)
        {
            string strReplace = input;

            // replace open & close tag name
            foreach (var s in ARR_ReplaceHtmlTag)
            {
                strReplace = Regex.Replace(strReplace, @"<" + s,
                                "<" + s.Substring(0, 2) + Suffix_ReplaceHtmlTag + s.Substring(3), RegexOptions.IgnoreCase);
                strReplace = Regex.Replace(strReplace, @"</" + s,
                                "</" + s.Substring(0, 2) + Suffix_ReplaceHtmlTag + s.Substring(3), RegexOptions.IgnoreCase);
            }

            // replace event

            return ARR_ReplaceHtmlEvent.Aggregate(strReplace, (current, s) => Regex.Replace(current, s, s.Substring(0, 2) + Suffix_ReplaceHtmlTag + s.Substring(3), RegexOptions.IgnoreCase));
        }

        public static string RemoveAndReplaceAttribute(string input)
        {
            return RemoveAndReplaceAttribute(input, false);
        }

        public static string RemoveAndReplaceAttribute(string input, bool isReplaceBrTag)
        {
            string strRet = input;

            if (isReplaceBrTag)
            {
                strRet = strRet.Replace(BRTag + "\r\n", BRTag).Replace("\r\n", BRTag).Replace("\r", BRTag).Replace("\n", BRTag);
            }

            //strRet = RegulateHtml(strRet); // chuan hoa html

            strRet = ReplaceHtmlTag(strRet); // xoa cac the nguy hiem

            strRet = RemoveHtmlAttribute(strRet); // xoa cac event cua javascript
            strRet = ReplaceHtmlAttribute(strRet); // doi ten cac event hoac the chua replace duoc

            //strRet = ReplaceBadWord(strRet); // xoa cac tu xau trong comment

            return strRet;
        }

        #endregion

        #region private method

        private static string ConcatText(Match m)
        {
            return m.Groups[1].Value + m.Groups[3].Value + " ";
        }


        #region chuan hoa the html
        /// <summary>
        /// chuan hoa the html
        /// </summary>
        /// <returns></returns>
        //private static string RegulateHtml(string input)
        //{
        //    string strRet = input;

        //    HtmlAgilityPack.HtmlDocument htmldoc = new HtmlAgilityPack.HtmlDocument();
        //    htmldoc.LoadHtml(strRet);
        //    htmldoc.OptionAutoCloseOnEnd = true;

        //    strRet = htmldoc.DocumentNode.InnerHtml;
        //    return strRet;
        //}
        #endregion

        /// <summary>
        /// xoa cac the html khong cho phep va chan ky tu xau
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        // ReSharper disable once FunctionRecursiveOnAllPaths
        private static string ReplaceHtmlTag(string input)
        {
            string strRet = input;

            strRet = CleanUpWordHtml(strRet.Trim());

            // replace the html
            strRet = ReplaceHtmlTag(strRet);
            if (strRet == string.Empty)
                return string.Empty;

            return strRet;
        }

        #region comment

        /// <summary>
        /// xoa cac tu xau trong comment
        /// </summary>
        /// <returns></returns>
        //public static string ReplaceBadWord(string input)
        //{
        //    string strRet = input;

        //    // chan cac ky tu xau bang cac ky tu ****
        //    strRet = ReplaceBadChatWord(strRet);

        //    if ((strRet.IndexOf("Cashfiesta") != -1 || strRet.IndexOf("Auto Easy Money") != -1) &&
        //        strRet.IndexOf("kiếm tiền") != -1 && strRet.IndexOf("ngân hàng") != -1 &&
        //        strRet.Length > 500)
        //    {
        //        return string.Empty;
        //    }

        //    return strRet;
        //}
        #endregion

        public static string CleanUpWordHtml(string html)
        {
            html = MakeSafe(html);
            html = FixEntityCharacters(html);

            var nvc = new NameValueCollection
            {
                {@"<!--(\w|\W)+?-->", string.Empty},
                {@"<title>(\w|\W)+?</title>", string.Empty},
                {@"<style>(\w|\W)+?</style>", string.Empty},
                {@"<script\s[^>]*>.+</script>", string.Empty},
                {@"<script (\w|\W)+?\><script>", string.Empty},
                {@"<object.*>.*<object>", string.Empty},
                {@"\[flash\](\w|\W)+?\[/flash\]", string.Empty},
                {@"\[music\](\w|\W)+?\[/music\]", string.Empty},
                {@"\[media\](\w|\W)+?\[/media\]", string.Empty},
                {@"<video>(\w|\W)+?</video>", string.Empty},
                {@"<form>(\w|\W)+?</form>", string.Empty},
                {@"<iframe (\w|\W)+?>(\w|\W)+?</iframe>", string.Empty},
                {@"<input.*/>", string.Empty},
                {@"<input.*></input>", string.Empty},
                {@"<div.*style=.*absolute.*>", "<div"},
                {@"\s?class=\w+", string.Empty},
                {
                    @"(font-family:[^&gt;]*[;'])|(font-size:[^&gt;]*[;'])|(line-height:[^&gt;]*[;'])|(MsoNormal)|(&lt;!--\[if.*?&lt;!\[endif\]--&gt;)",
                    string.Empty
                },
                {@"<(meta|link|/?o:|/?st\d|/?head|/?html|body|/?body|!\[)[^>]*?>", string.Empty},
                {@"(<[^>]+>)+&nbsp;(</\w+>)+", string.Empty},
                {@"\s+v:\w+=""[^""]+""", string.Empty},
                {@"(\n\r){2,}", string.Empty},
                {@"<[/]?(xml|del|ins|[ovwxp]:\w+)[^>]*?>", string.Empty},
                {@"<([^>]*)(?:lang|size|face|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>"}
            };

            //#Begin Hoang them
            //nvc.Add(@"<embed (\w|\W)+?>", string.Empty);
            //nvc.Add(@"<div .*>.*<img (\w|\W)* />", "<div>");
            //nvc.Add(@"<? background", string.Empty);
            //#End Hoang them

            html = nvc.Keys.Cast<string>().Aggregate(html, (current, key) => Regex.Replace(current, key, nvc[key], RegexOptions.None));

            return html.Trim();
        }
        public static string MakeSafe(string value)
        {
            var v = new StringBuilder(value);

            v
                .Replace("&Agrave;", "À")
                .Replace("&Aacute;", "Á")
                .Replace("&Acirc;", "Â")
                .Replace("&Atilde;", "Ã")
                .Replace("&Ccedil;", "Ç")
                .Replace("&Egrave;", "È")
                .Replace("&Eacute;", "É")
                .Replace("&Ecirc;", "Ê")
                .Replace("&Igrave;", "Ì")
                .Replace("&Iacute;", "Í")
                .Replace("&Icirc;", "Î")
                .Replace("&ETH;", "Ð")
                .Replace("&Ograve;", "Ò")
                .Replace("&Oacute;", "Ó")
                .Replace("&Ocirc;", "Ô")
                .Replace("&Otilde;", "Õ")
                .Replace("&Ugrave;", "Ù")
                .Replace("&Uacute;", "Ú")
                .Replace("&Yacute;", "Ý")
                .Replace("&agrave;", "à")
                .Replace("&aacute;", "á")
                .Replace("&acirc;", "â")
                .Replace("&atilde;", "ã")
                .Replace("&aring;", "å")
                .Replace("&ccedil;", "ç")
                .Replace("&egrave;", "è")
                .Replace("&eacute;", "é")
                .Replace("&ecirc;", "ê")
                .Replace("&igrave;", "ì")
                .Replace("&iacute;", "í")
                .Replace("&icirc;", "î")
                .Replace("&ograve;", "ò")
                .Replace("&oacute;", "ó")
                .Replace("&ocirc;", "ô")
                .Replace("&otilde;", "õ")
                .Replace("&ugrave;", "ù")
                .Replace("&uacute;", "ú")
                .Replace("&ucirc;", "û")
                .Replace("&yacute;", "ý");

            return v.ToString();
        }
        private static string FixEntityCharacters(string html)
        {
            var nvc = new NameValueCollection { { "“", "&ldquo;" }, { "”", "&rdquo;" }, { "–", "&mdash;" } };

            return nvc.Keys.Cast<string>().Aggregate(html, (current, key) => current.Replace(key, nvc[key]));
        }

        #region ReplaceBadChatWord
        //public static string ReplaceBadChatWord(string comment)
        //{
        //    UserManager um = new UserManager();
        //    DataSet ds_ChatWord = um.GetAllChatWord();

        //    if (ds_ChatWord.Tables[0].Rows.Count > 0 && ds_ChatWord.Tables.Count > 0 && ds_ChatWord != null)
        //    {
        //        string[] character = new string[12];
        //        character[0] = " ";
        //        character[1] = "<";
        //        character[2] = ">";
        //        character[3] = "$";
        //        character[4] = ";";
        //        character[5] = ".";
        //        character[6] = "_";
        //        character[7] = "!";
        //        character[8] = ",";
        //        character[9] = "\"";
        //        character[10] = "\n";
        //        character[11] = "&nbsp;";

        //        comment = " " + comment + " ";
        //        string StrNewWord = " *** ";
        //        int iLoop = 0;
        //        int maxLoop = 2;
        //        for (int i = 0; i < ds_ChatWord.Tables[0].Rows.Count; i++)
        //        {
        //            string StrBadWord = ds_ChatWord.Tables[0].Rows[i]["TextChat"].ToString();
        //            StrBadWord = StrBadWord.Trim();

        //            iLoop = 0;
        //            while (comment.ToUpper().Contains((StrBadWord + StrBadWord).ToUpper()))
        //            {
        //                comment = comment.Replace("&nbsp;", " ");
        //                comment = comment.Replace("\n", "\n" + " ");
        //                comment = comment.Replace(StrBadWord + StrBadWord, "***");
        //                comment = comment.Replace(StrBadWord.ToUpper() + StrBadWord.ToUpper(), "***");
        //                iLoop++;
        //                if (iLoop > maxLoop) { break; }
        //            }

        //            iLoop = 0;
        //            string commentBeforeReplace = "";

        //            while (comment != commentBeforeReplace && comment.ToUpper().Contains(StrBadWord.ToUpper())) // comment.Contains(StrBadWord))
        //            {
        //                commentBeforeReplace = comment;
        //                //comment = comment.Replace("&nbsp;", " ");
        //                //comment = comment.Replace("\n", "\n"+ " ");
        //                //comment = comment.Replace(">" + StrBadWord + "<", ">" + StrNewWord + "<");
        //                //comment = comment.Replace("\n" + StrBadWord + " ", StrNewWord);
        //                //comment = comment.Replace(">" + StrBadWord + " ", ">" + StrNewWord);
        //                //comment = comment.Replace(" " + StrBadWord + "<", StrNewWord + "<");
        //                //comment = comment.Replace(" " + StrBadWord + " ", StrNewWord);                        

        //                for (int j = 0; j < character.Length - 1; j++)
        //                {
        //                    for (int k = 0; k < character.Length; k++)
        //                    {
        //                        comment = comment.Replace(character[j] + StrBadWord + character[k], character[j] + StrNewWord + character[k]);
        //                        comment = comment.Replace(character[j] + StrBadWord.ToUpper() + character[k], character[j] + StrNewWord + character[k]);
        //                    }
        //                }

        //                iLoop++;
        //                if (iLoop > maxLoop) { break; }
        //            }
        //        }
        //    }
        //    return comment;



        //}
        #endregion

        #endregion

        #endregion

        #region New code

        /// <summary>
        /// Xóa bỏ các thẻ script
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string RemoveScript(string html)
        {
            return RemoveTag(html, "script");
        }

        /// <summary>
        /// Xóa bỏ tất cả thẻ html theo tên
        /// </summary>
        /// <param name="html"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static string RemoveTag(string html, string tag)
        {
            return html;
        }

        /// <summary>
        /// Xóa tất cả html trong text
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string RemoveHtml(string html)
        {
            return html;
        }

        /// <summary>
        /// Xóa 1 thuộc tính trong tất cả các thẻ theo tên
        /// </summary>
        /// <param name="html"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        public static string RemoveAttribute(string html, string attr)
        {
            return html;
        }

        /// <summary>
        /// Lọc bỏ tất cả event trong html
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string RemoveEventInTag(string html)
        {
            return html;
        }

        /// <summary>
        /// Lọc base64 trong html và save vào service
        /// <para>("yyyy/MM/dd/")+textId</para>
        /// </summary>
        /// <param name="html"></param>
        /// <param name="type">TypeImageUploadEnum</param>
        /// <param name="textId"></param>
        /// <returns></returns>
        public static string FillterBase64SaveToService(string html,  string textId)
        {
            if (string.IsNullOrEmpty(html)) html = "";
            if (textId.Length > 50) textId = textId.Substring(0, 50);
            string path=  textId+"-"+DateTime.Now.Ticks+"-";
            html = Regex.Replace(html, "data:image(.*?)\"", m => SaveByteArrayAsImage(path + m.Index, m.Value.Substring(0, m.Value.Length - 1)) + "\"");
            return html;
        }
        /// <summary>
        /// Lọc base64 trong ass comment html và save vào service
        /// <para>("yyyy/MM/dd/")+textId</para>
        /// </summary>
        /// <param name="html"></param>
        /// <param name="type">TypeImageUploadEnum</param>
        /// <returns></returns>
        public static string FillterBase64InComment(TypeImageUploadEnum type, string html)
        {
            string path = "Comment/" + DateTime.Now.ToString("yyyy/MM/dd/") + DateTime.Now.Ticks + "-";
            html = Regex.Replace(html, "data:image(.*?)\"", m => SaveByteArrayAsImage(path + m.Index, m.Value.Substring(0, m.Value.Length - 1)) + "\"");
            return html;
        }
        private static string SaveByteArrayAsImage(string path, string base64String)
        {
            try
            {
                string ex;
                var bytes = ImageHelper.Base64ToByte(base64String, out ex);
                var rs = WebHelper.UploadImage(path + "." + ex,bytes);
                return MyConfiguration.Default.ImageHost + rs.ImagePath;

            }
            catch
            {
                return "";
            }
            return "";
        }


        /// <summary>
        /// Chuẩn hóa html, xóa script,xóa các link có script, xóa các event, xóa các thẻ nguy hiểm
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string ConvertToHtmlStandard(string html)
        {
            return MyUtility.Extensions.StringExtension.CleanUpWordHtml(html);
        }
        #endregion
    }
}