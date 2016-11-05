using System;
using System.Collections.Generic;
using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.EventManagement;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Contract.EventModule
{
    public interface IEventRepository : IDaoRepository<EventPromotion>
    {
        List<Out_EventPromotion_GetInfo_Result> GetInfo();

        List<Out_EventPromotion_GetInfo_RunOn_Result> GetInfo_ByRunOn(int runOn);

        Out_EventPromotion_GetInfoDetails_Result GetInfoDetails(int articleId);

        List<Out_E1602TopUserWin_GetByDate_Result> GetTopUserWin(DateTime reportDate, int top);

        List<Out_E1604_TopAttackBoss_GetDataTop_Result> E1604_TopAttackBoss_GetDataTop(int top);

        List<Out_TMission_Top_GetTop_Result> TMission_GetTopFinish(int top, DateTime date);
        List<Out_Report_E1606_UserShootFish_GetDate_Result> GetTopUserFishCollection(int top, DateTime date);
        List<Out_FishCollection_AcchiveUser_Result> GetFishCollection_AcchiveUser(int userid);
        List<Ins_FishCollection_GetTop_Result> FishCollection_GetTop(int? top = null);
        List<Out_E1607_TopFriendWinGold_Archives_Result> GetFriendWinGold_Acchive(int userid, DateTime dateBegin, DateTime dateEnd);
        List<Out_E1607_TopFriendWinGold_GetTop_Result> GetTopFriendWinGold_GetTop(int? top = null);
        Out_EventPromotion_GetEventByCode_Result EventPromotion_GetEventByCode(string keyCode);
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
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 30/05/2016</para>
        /// <para>lây top user bắn thắng</para>
        /// </summary>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        List<Out_E1602TopUserWin_GetTopByDate_Result> E1602TopUserWin_GetTopByDate(int top);

        /// <summary>
        /// <para>Author:ThoaiND</para>
        /// <para>DateCreated: 31/05/2016</para>
        /// <para>Kiem tra khuyen mai nap tien</para>
        /// </summary>
        /// <param name="soluongthe"></param>
        /// <param name="minamount"></param>
        /// <param name="maxamount"></param>
        /// <param name="userid"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        int? Out_CheckKhuyenMaiNapTien_Valid(int soluongthe, int minamount, int maxamount, int userid, int amount);

        /// <summary>
        /// <para>Author:ThoaiND</para>
        /// <para>DateCreated: 2/6/2016</para>
        /// <para>ghi log khuyen mai nap tien</para>
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="createdate"></param>
        /// <param name="chanelid"></param>
        /// <param name="iprequest"></param>
        /// <param name="platformid"></param>
        /// <param name="gameversion"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        int? Out_E1610_KhuyenMaiNapTheLanDau_Insert(int userid, int chanelid, string iprequest, int platformid, string gameversion, int amount);

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

        /// <summary>
        /// <para>Author:ThoaiND</para>
        /// <para>DateCreated: 3/6/2016</para>
        /// <para>ghi log nap tien theo menh gia</para>
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="chanelid"></param>
        /// <param name="platformid"></param>
        /// <param name="gameversion"></param>
        /// <param name="amount"></param>
        /// <param name="cardtype"></param>
        /// <param name="percent"></param>
        /// <param name="iprequest"></param>
        /// <returns></returns>
        int? Out_E1612_KhuyenMaiTheoMenhGia_Insert(int userid, int chanelid, int platformid, string gameversion, int amount, int cardtype, double percent, string iprequest);

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 03/06/2016</para>
        /// <para>lấy event đang chạy theo kiểu event</para>
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        List<Out_EventPromotion_GetEventRunningByType_Result> EventPromotion_GetEventRunningByType(int typeId);

        /// <summary>
        /// Lấy danh sách user trúng thưởng trong event may mắn hằng ngày
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 6/7/2016 CreateBy MinhT 
        /// </history>
        List<Out_LogUserRecieveLuckyEveryday_GetFullDataForAPI_Result> LogUserRecieveLuckyEveryday_GetFullDataForAPI();

        /// <summary>
        /// Lấy danh sách user trúng thưởng trong event may mắn hằng ngày
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 6/7/2016 CreateBy MinhT 
        /// </history>
        List<Out_LogUserRecieveLuckyEveryday_GetFullDataForAPI_ByDate_Result> LogUserRecieveLuckyEveryday_GetFullDataForAPI(DateTime dateReport, int periodOfTime, int rowBegin, int rowEnd);

        /// <summary>
        /// Lấy danh sách  khung thời gian và ngày diễn ra sự kiện may mắn hằng ngày
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 6/7/2016 CreateBy MinhT 
        /// </history>
        List<Out_E1609_LogConfigLuckyEveryday_GetDateEventEveryDay_Result>
            Out_E1609_LogConfigLuckyEveryday_GetDateEventEveryDay();
    }
}
