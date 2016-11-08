using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessRedis.Constants
{
    /// <summary>
    /// Dinh nghia redis key bang 3 chu cai dau tien (GetUsersOnline = "guo")
    /// </summary>
    public class RedisKeyConstants
    {
        #region Account
        /// <summary>
        /// guo:top:userID
        /// </summary>
        public const string GetUsersOnline = "guo";
        #endregion

        #region Event
        /// <summary>
        /// gtl:top:userID
        /// </summary>
        public const string GetTopLevel = "gtl";

        /// <summary>
        /// gtr:top:userID
        /// </summary>
        public const string GetTopRich = "gtr";
        #endregion

        #region Account
        /// <summary>
        /// gtlf:top:pageNumber
        /// </summary>
        public const string GetTopLevelFriend = "gtlf";

        /// <summary>
        /// gtrf:top:pageNumber
        /// </summary>
        public const string GetTopRickFriend = "gtrf";
        #endregion

        #region Payment

        public const string GetTopChargeCard = "tcc";

        public const string GetTopExChangeCard = "texc";

        public const string GetExchangeCardHistory = "exch";

        #endregion

        #region may mawns hang ngay
        public const string LuckyEveryday = "le";
        #endregion

    }
}