using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Contract.UserModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.UserModule
{
    public class AccountRepository : DaoRepository<UserEntities, Account>, IAccountRepository
    {
        public Out_Account_GetByID_Result GetAccountById(int userId)
        {
            return Uow.Context.Out_Account_GetByID(userId).FirstOrDefault();
        }

        public int AddAvatar(int userId, string pathImage)
        {
            return Uow.Context.Out_Account_AddAvatar(userId, pathImage);
        }

        public string GetLinkAvatar(int userId)
        {
            return Uow.Context.Out_Account_GetLinkAvatar(userId).FirstOrDefault();
        }

        public List<Out_Account_GetUsersOnline_Result> GetUsersOnline(int position, int pageSize)
        {
            var mock = new List<Out_Account_GetUsersOnline_Result>();
            
            //for (int i = 0; i < 199; i++)
            //{
            //    mock.Add(new Out_Account_GetUsersOnline_Result
            //    {
            //        UserID = i+12300,
            //        EmotionPath = "test/test/"+i+".jpg",
            //        FullNameInfo = "name"+i,
            //        LevelUser = i,
            //        WalletMoney = i*10000
            //    });
            //}
            //return mock;
            return Uow.Context.Out_Account_GetUsersOnline(position, pageSize).ToList();
        }

        public List<Out_Account_GetUsersOnline_V2_Result> GetUsersOnline_V2(int position, int pageSize, int userId)
        {
            return Uow.Context.Out_Account_GetUsersOnline_V2(position, pageSize, userId).ToList();
        }

        public int GetUserIsPlayingGame(int userId)
        {
            return Uow.Context.Out_Game_CheckIsPlayGame(userId);
        }

        /// <summary>
        /// Lấy tên tài khoản theo userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 22/3/2016 Create By TaiNM
        /// </history>
        public string GetNameByUserId(int userId)
        {
            return Uow.Context.Ins_Account_GetNameByUserId(userId).FirstOrDefault();
        }

        /// <summary>
        /// Gọi Store API để kích hoặc Update gold user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="wsPacket"></param>
        /// <returns></returns>
        /// <history>
        /// 1/4/2016 Create By MinhT
        /// </history>
        public int CallApiKickOrUpdateGold(int userId, int wsPacket)
        {
            return Uow.Context.Ins_Game_KickOrUpdateGold(userId, wsPacket).FirstOrDefault().GetValueOrDefault();
        }

        /// <summary>
        /// Check user update phone
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated : 11/04/2016</para>
        public bool IsUpdatePhoneUser(int userId)
        {
            return Uow.Context.Out_Profile_CheckIsUpdatePhoneNumber(userId).FirstOrDefault().GetValueOrDefault(false);
        }

        /// <summary>
        /// lay thoi gian choi game trong ngay cua user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated : 13/04/2016</para>
        public int ATPPlayerRecord_GetTimePlayToday(int userId)
        {
            return Uow.Context.Out_ATPPlayerRecord_GetTimePlayToday(userId).FirstOrDefault().GetValueOrDefault(0);
        }

        /// <summary>
        /// Cap nhat gold cho user tren DBGame
        /// Created user: Duynd - 05/05/2016        
        /// </summary>
        public int UpdateGoldUser(int intUserID, int intType, decimal decGold)
        {
            return Uow.Context.Api_A_Gold_UpdateForUser(intUserID, intType, Convert.ToInt64(decGold));
        }
    }
}
