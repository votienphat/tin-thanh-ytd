using System;
using System.Security.Cryptography;
using System.Text;
using MyUtility;

namespace BusinessObject.Helper
{
    public class BoCommon
    {
        #region Paging

        /// <summary>
        /// Calculate total page
        /// </summary>
        /// <param name="totalItem"></param>
        /// <param name="pageLength">Default is 0</param>
        /// <returns>If page size is 0, return 1 if total item is greater than 0</returns>
        public static int GetTotalPage(int totalItem, int pageLength = 0)
        {
            if (pageLength <= 0)
            {
                return totalItem > 0 ? 1 : 0;
            }
            return (int)Math.Ceiling((decimal)totalItem / pageLength);
        }

        /// <summary>
        /// Get next page
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageLength"></param>
        /// <param name="totalRows"></param>
        /// <returns></returns>
        public static bool HasNextPage(int startIndex, int pageLength, int totalRows)
        {
            return startIndex + pageLength > totalRows;
        }

        #endregion

        #region Membership

        /// <summary>
        /// mã hóa mật khẩu user nhập
        /// </summary>
        /// <param name="passwordInput"></param>
        /// <param name="keySalt"></param>
        /// <returns></returns>
        /// <history>
        /// 18/12/2014 Create By TrungLD
        /// </history>
        public static string EncodePassword(string passwordInput, string keySalt)
        {
            return Common.sha256_hash(passwordInput + keySalt);
        }

        // ReSharper disable once InconsistentNaming
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(Encoding.ASCII.GetBytes(text));

            //get hash result after compute it
            var result = md5.Hash;

            var strBuilder = new StringBuilder();
            foreach (var t in result)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(t.ToString("x2"));
            }

            return strBuilder.ToString();
        }

        #endregion
    }
}