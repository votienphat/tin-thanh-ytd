﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BussinessObject.UserModule.Models;
using BussinessObject.UserModule.Models.Request;
using BussinessObject.UserModule.Models.Response;
using EntitiesObject.Entities.UserEntities;

namespace BussinessObject.UserModule.Contract
{
    public interface IAccountBusiness
    {
        /// <summary>
        /// Lấy thông tin account theo user ID
        /// <para>Author: PhatVT</para>
        /// <para>Created Date: 18/12/2014</para>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        UserInfo GetInfo(int userId);

        string GetLinkAvatar(int userId);

        List<Out_Account_GetUsersOnline_Result> GetUsersOnline(int position, int pageSize);

        /// <summary>
        /// Lấy danh sách user online + cờ xác định là bạn
        /// </summary>
        /// <param name="position"></param>
        /// <param name="pageSize"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 17/3/2016 Create By TaiNM
        /// </history>
        List<Out_Account_GetUsersOnline_V2_Result> GetUsersOnline_V2(int position, int pageSize, int userId);

        List<Out_Account_GetUsersOnline_V2_Result> GetUsersOnline_V2(int position, int pageSize, int userId, DateTime dtmClearCacheRedis);

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <history>
        /// 12/01/2016 PhatVT: Tạo mới
        /// </history>
        LoginResponse Login(LoginRequest request);

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
        bool CallApiKickOrUpdateGold(int userId, int wsPacket);

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

        Task<int> UpdateGoldUser(int intUserID, int intType, decimal decGold);
    }
}