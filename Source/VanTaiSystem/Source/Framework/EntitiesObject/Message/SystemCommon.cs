using System;

namespace EntitiesObject.Message
{
    public class SystemCommon
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

        /// <summary>
        /// Lấy index cuối
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageLength"></param>
        /// <returns></returns>
        public static int GetEndIndex(int startIndex, int pageLength)
        {
            return startIndex + pageLength - 1;
        }

        #endregion
    }
}