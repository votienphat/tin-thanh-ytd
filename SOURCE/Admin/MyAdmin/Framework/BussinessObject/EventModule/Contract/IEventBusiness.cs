using System;
using System.Collections.Generic;
using BussinessObject.EventModule.Enum;
using BussinessObject.EventModule.Models;
using EntitiesObject.Entities.EventManagement;
using EntitiesObject.Entities.LogManagementEntities;
using EntitiesObject.Entities.UserEntities;

namespace BussinessObject.EventModule.Contract
{
    public interface IEventBusiness
    {

        /// <summary>
        /// <para>Author:PhatVT</para>
        /// <para>DateCreated: 06/01/2016</para>
        /// <para>Chạy các khuyến mãi</para>
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 18/3/2016 Updated TaiNM
        /// Param promotionRequestModel
        /// 22/3/2016 Updated TaiNM
        /// return list EventPromotionResposeModel
        /// </history>
        List<EventPromotionResposeModel> RunPromotion(PromotionRequestModel request);

        /// <summary>
        /// Lấy danh sách loại thẻ đổi
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 21/12/2015: Created by PhatVT
        /// </history>

        /// <summary>
        /// Lấy danh sách bài viết hiển thị trên game
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 18/2/2016 Create By MinhT
        /// </history>
        List<ArticleResponseModel> GetArticle(int cate, int starindex, int pagelenght, out int totalrow);


        /// <summary>
        /// Lấy top danh sách bài viết hiển thị trên game mobile
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        List<ArticleResponseModel> GetTopArticle(int top);

        /// <summary>
        /// Lấy danh sách bài viết hiển thị trên game Theo ID
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        /// <history>
        /// 18/2/2016 Create By MinhT
        /// </history>
        ArticleResponseModel GetLinkArticleDetail(int articleId);
        ArticleResponseModel GetArticleDetailBySeo(string textid);

        /// <summary>
        /// lấy top level user
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 5/3/2016 Create By MinhT
        /// </history>
        List<Out_LevelGameLog_GetLevelGame_Result> GetTopLevel(int top, int userId);

        /// <summary>
        /// lấy top level user - Cache data on Redis
        /// Duynd - 16/05/2016
        /// </summary>
        /// <param name="top"></param>
        /// <param name="userId"></param>
        /// <param name="dtmClearCacheRedis"></param>
        /// <returns></returns>
        List<Out_LevelGameLog_GetLevelGame_Result> GetTopLevel(int top, int userId, DateTime dtmClearCacheRedis);

        /// <summary>
        /// lấy top kill boss
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 5/3/2016 Create By TrungLD
        /// </history>
        List<Out_TopKillBoss_GetTop_Result> GetTopKillBoss(int top);

        /// <summary>
        /// Lấy danh sách top thắng lớn
        /// </summary>
        /// <param name="reportDate"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        /// <history>
        /// 11/3/2016 Create By MinhT
        /// </history>
        List<Out_E1602TopUserWin_GetByDate_Result> GetTopUserWin(DateTime reportDate, int top);

        /// <summary>
        /// Danh sach sát thương cá boss theo event
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 16/03/2016</para>
        List<Out_E1604_TopAttackBoss_GetDataTop_Result> E1604_TopAttackBoss_GetDataTop(int top);

        /// <summary>
        /// Lấy top hoàn thành nhiệm vụ trong ngày
        /// </summary>
        /// <param name="top"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        /// <history>
        /// 18/3/2016 Create By MinhT
        /// </history>
        List<Out_TMission_Top_GetTop_Result> TMission_GetTopFinish(int top, DateTime date);

        /// <summary>
        /// Lấy top user hoàn thành bộ sưu tập cá trong ngày
        /// </summary>
        /// <param name="top"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        /// <history>
        /// 29/3/2016 Create By ThoaiND
        /// </history>
        List<Out_Report_E1606_UserShootFish_GetDate_Result> TopUserFishCollection(int top, DateTime date);

        /// <summary>
        /// Lấy top user hoàn thành bộ sưu tập cá trong ngày
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        /// <history>
        /// 30/3/2016 Create By ThoaiND
        /// </history>
        List<Ins_FishCollection_GetTop_Result> GetTopFishCollection(int? top = 10);

        /// <summary>
        /// Lấy bảng thành tích user
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        /// <history>
        /// 30/3/2016 Create By ThoaiND
        /// </history>
        List<Out_FishCollection_AcchiveUser_Result> GetFishCollection_AcchiveUser(int userid);

        /// <summary>
        /// Lấy top user có tổng bạn có số gold cao nhất
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        /// <history>
        /// 4/4/2016 Create By ThoaiND
        /// </history>
        List<Out_E1607_TopFriendWinGold_Archives_Result> GetFriendWinGold_Acchive(int userid, DateTime dateBegin, DateTime dateEnd);

        /// <summary>
        /// Lấy bảng thành tích user
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        /// <history>
        /// 4/4/2016 Create By ThoaiND
        /// </history>
        List<Out_E1607_TopFriendWinGold_GetTop_Result> GetTopFriendWinGold_GetTop(int? top = null);

        ConfigEventInviteFriendModel GetConfigInviteFriend(string keyCode);
        List<Ins_E1607_TopManyFriend_GetTopFriendFacebook_Result> GetTopFriendFacebook(int intTop);

        List<Ins_E1607_TopManyFriend_GetTopFriendFacebookOfUser_Result> GetTopFriendFacebookOfUser(int intUserID, int intPageSize, int intPosition);

        /// <summary>
        /// Lấy danh sách bạn bè của user mời từ fb có xu cao nhất
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="position"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <history>
        /// 5/4/2016 Create By TaiNM
        /// </history>
        List<Out_FriendRelationship_TopFriendFbGoldMax_Result> GetTopFriendFbGoldMax(int userId,
            int position, int pageSize, out int count);

        /// <summary>
        /// Lấy danh sách user có tổng xu của bạn bè (mời trên fb)  cao nhất
        /// </summary>
        /// <param name="position"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <history>
        /// 5/4/2016 Create By TaiNM
        /// </history>
        List<Out_E1607_TopGoldFriend_GetData_Result> GetTopGoldFriend(int position, int pageSize, out int count);

        Ins_EventPromotion_GetEventById_Result GetEventById(int intEventID);

        /// <summary>
        /// Lấy danh sách đại gia trong game
        /// </summary>
        /// <param name="top"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <history>
        /// 25/4/2016 Create By TaiNM
        /// </history>
        List<Out_RichGameLog_GetData_Result> GetTopRich(int top, int userId);
        
        List<Out_RichGameLog_GetData_Result> GetTopRich(int top, int userId, DateTime dtmClearCacheRedis);

        List<Out_E1602TopUserWin_GetTopByDate_Result> E1602TopUserWin_GetTopByDate(int top = 10);

        /// <summary>
        /// Lấy Top user bắn chết cá theo 1 số cá được cấu hình cho trước
        /// </summary>
        /// <param name="rowBegin"></param>
        /// <param name="rowEnd"></param>
        /// <param name="top"></param>
        /// <param name="totalRow"></param>
        /// <returns></returns>
        /// <history>
        /// 2/6/2016 MinhT Create New
        /// </history>
        List<Out_A_TPlayGame_ListHitFish_ByUser_EventBanCa_Result> Out_A_TPlayGame_ListHitFish_ByUser_EventBanCa_Result(int rowBegin, int rowEnd, int top, out int totalRow);

        Out_EventPromotion_GetEventByCode_Result EventPromotion_GetEventByCode(string keyCode);

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 03/06/2016</para>
        /// <para>lấy event đang chạy theo kiểu event</para>
        /// </summary>
        /// <param name="typeEnum"></param>
        /// <returns></returns>
        List<Out_EventPromotion_GetEventRunningByType_Result> EventPromotion_GetEventRunningByType(
            TypeEventPromotion typeEnum);

        /// <summary>
        /// Lấy danh sách user trúng thưởng trong event may mắn hằng ngày
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 6/7/2016 CreateBy MinhT 
        /// </history>
        List<LuckyEverydayViewModel> LogUserRecieveLuckyEveryday_GetFullDataForAPI();
        
        /// <summary>
        /// Lấy danh sách user trúng thưởng trong event may mắn hằng ngày
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 6/7/2016 CreateBy MinhT 
        /// </history>
        List<LuckyEverydayMiniModel> LogUserRecieveLuckyEveryday_GetFullDataForAPI(DateTime dateReport, int periodOfTime, int pageSize, int pageIndex);

        /// <summary>
        /// Lấy danh sách  khung thời gian và ngày diễn ra sự kiện may mắn hằng ngày
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 6/7/2016 CreateBy MinhT 
        /// </history>
        List<LuckyEverydayEventDate>
            GetLuckyEverydayEventDate();
    }
}
