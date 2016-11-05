using System.Collections.Generic;
using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Contract.UserModule
{
    public interface IAccountRepository : IDaoRepository<Account>
    {
        Out_Account_GetByID_Result GetAccountById(int userId);
        int AddAvatar(int userId, string pathImage);

        string GetLinkAvatar(int userId);

        List<Out_Account_GetUsersOnline_Result> GetUsersOnline(int position, int pageSize);

        List<Out_Account_GetUsersOnline_V2_Result> GetUsersOnline_V2(int position, int pageSize, int userId);

        int GetUserIsPlayingGame(int userId);

        /// <summary>
        /// Lấy tên tài khoản theo userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 22/3/2016 Create By TaiNM
        /// </history>
        string GetNameByUserId(int userId);

        /// <summary>
        /// Gọi Store API để kích hoặc Update gold user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="wsPacket"></param>
        /// <returns></returns>
        /// <history>
        /// 1/4/2016 Create By MinhT
        /// </history>
        int CallApiKickOrUpdateGold(int userId, int wsPacket);

        /// <summary>
        /// Check user update phone
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated : 11/04/2016</para>
        bool IsUpdatePhoneUser(int userId);

        /// <summary>
        /// lay thoi gian choi game trong ngay cua user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated : 13/04/2016</para>
        int ATPPlayerRecord_GetTimePlayToday(int userId);

        int UpdateGoldUser(int intUserID, int intType, decimal decGold);
    }
}
