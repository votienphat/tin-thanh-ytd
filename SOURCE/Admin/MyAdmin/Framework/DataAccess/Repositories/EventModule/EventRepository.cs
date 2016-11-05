using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using DataAccess.Contract.EventModule;
using DataAccess.EF;
using DataAccess.Infrastructure;
using EntitiesObject.Entities.EventManagement;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.EventModule
{
    public class EventRepository : DaoRepository<EventManagementEntities, EventPromotion>, IEventRepository
    {
        public List<Out_EventPromotion_GetInfo_Result> GetInfo()
        {
            return Uow.Context.Out_EventPromotion_GetInfo().ToList();
        }

        public List<Out_EventPromotion_GetInfo_RunOn_Result> GetInfo_ByRunOn(int runOn)
        {
            return Uow.Context.Out_EventPromotion_GetInfo_RunOn(runOn).ToList();
        }

        public Out_EventPromotion_GetInfoDetails_Result GetInfoDetails(int articleId)
        {
            return Uow.Context.Out_EventPromotion_GetInfoDetails(articleId).FirstOrDefault();
        }

        public List<Out_E1602TopUserWin_GetByDate_Result> GetTopUserWin(System.DateTime reportDate, int top)
        {
            return Uow.Context.Out_E1602TopUserWin_GetByDate(reportDate, top).ToList();
        }

        public List<Out_E1604_TopAttackBoss_GetDataTop_Result> E1604_TopAttackBoss_GetDataTop(int top)
        {
            return Uow.Context.Out_E1604_TopAttackBoss_GetDataTop(top, null, null).ToList();
        }


        public List<Out_TMission_Top_GetTop_Result> TMission_GetTopFinish(int top, DateTime date)
        {
            return Uow.Context.Out_TMission_Top_GetTop(top, date).ToList();
        }

        public List<Out_Report_E1606_UserShootFish_GetDate_Result> GetTopUserFishCollection(int top, DateTime date)
        {
            return Uow.Context.Out_Report_E1606_UserShootFish_GetDate(top, date).ToList();
        }


        public List<Out_FishCollection_AcchiveUser_Result> GetFishCollection_AcchiveUser(int userid)
        {
            return Uow.Context.Out_FishCollection_AcchiveUser(userid).ToList();
        }


        public List<Ins_FishCollection_GetTop_Result> FishCollection_GetTop(int? top = null)
        {
            return Uow.Context.Ins_FishCollection_GetTop(top).ToList();
        }


        public List<Out_E1607_TopFriendWinGold_Archives_Result> GetFriendWinGold_Acchive(int userid, DateTime dateBegin, DateTime dateEnd)
        {
            return Uow.Context.Out_E1607_TopFriendWinGold_Archives(userid, dateBegin, dateEnd).ToList();
        }

        public List<Out_E1607_TopFriendWinGold_GetTop_Result> GetTopFriendWinGold_GetTop(int? top = null)
        {
            return Uow.Context.Out_E1607_TopFriendWinGold_GetTop().ToList();
        }

        public Out_EventPromotion_GetEventByCode_Result EventPromotion_GetEventByCode(string keyCode)
        {
            return Uow.Context.Out_EventPromotion_GetEventByCode(keyCode).FirstOrDefault();
        }

        public List<Ins_E1607_TopManyFriend_GetTopFriendFacebook_Result> GetTopFriendFacebook(int intTop)
        {
            return Uow.Context.Ins_E1607_TopManyFriend_GetTopFriendFacebook(intTop).ToList();
        }

        public List<Ins_E1607_TopManyFriend_GetTopFriendFacebookOfUser_Result> GetTopFriendFacebookOfUser(int intUserID, int intPageSize, int intPosition)
        {
            return Uow.Context.Ins_E1607_TopManyFriend_GetTopFriendFacebookOfUser(intUserID, intPageSize, intPosition).ToList();
        }

        /// <summary>
        /// Lấy danh sách bạn bè của user mời từ fb có xu cao nhất
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="top"></param>
        /// <param name="position"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <history>
        /// 5/4/2016 Create By TaiNM
        /// </history>
        public List<Out_FriendRelationship_TopFriendFbGoldMax_Result> GetTopFriendFbGoldMax(int userId, int position, int pageSize, out int count)
        {
            count = 0;
            var objParam = new ObjectParameter("TotalRow", count);
            var results = Uow.Context.Out_FriendRelationship_TopFriendFbGoldMax(userId, position, pageSize, objParam).ToList();
            int.TryParse(objParam.Value.ToString(), out count);
            return results;
        }

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
        public List<Out_E1607_TopGoldFriend_GetData_Result> GetTopGoldFriend(int position, int pageSize, out int count)
        {
            count = 0;
            var objParam = new ObjectParameter("TotalRow", count);
            var results = Uow.Context.Out_E1607_TopGoldFriend_GetData(position, pageSize, objParam).ToList();
            int.TryParse(objParam.Value.ToString(), out count);
            return results;
        }

        public Ins_EventPromotion_GetEventById_Result GetEventById(int intEventID)
        {
            return Uow.Context.Ins_EventPromotion_GetEventById(intEventID).FirstOrDefault();
        }

        public List<Out_E1602TopUserWin_GetTopByDate_Result> E1602TopUserWin_GetTopByDate(int top)
        {
            return Uow.Context.Out_E1602TopUserWin_GetTopByDate(top).ToList();
        }

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
        public int? Out_CheckKhuyenMaiNapTien_Valid(int soluongthe, int minamount, int maxamount, int userid, int amount)
        {
            return Uow.Context.Out_CheckKhuyenMaiNapTien_Valid(soluongthe, minamount, maxamount, userid, amount).FirstOrDefault();
        }

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
        public int? Out_E1610_KhuyenMaiNapTheLanDau_Insert(int userid, int chanelid, string iprequest, int platformid, string gameversion, int amount)
        {
            return Uow.Context.Out_E1610_KhuyenMaiNapTheLanDau_Insert(userid, chanelid, iprequest, platformid, gameversion, amount).FirstOrDefault();
        }

        public List<Out_A_TPlayGame_ListHitFish_ByUser_EventBanCa_Result> Out_A_TPlayGame_ListHitFish_ByUser_EventBanCa_Result(int rowBegin, int rowEnd, int top, out int totalRow)
        {
            totalRow = 0;
            var outTotalRow = new ObjectParameter("TotalRow", totalRow);
            var value = Uow.Context.Out_A_TPlayGame_ListHitFish_ByUser_EventBanCa(rowBegin, rowEnd, top, outTotalRow).ToList();
            totalRow = outTotalRow.Value == null ? 0 : Int32.Parse(outTotalRow.Value.ToString());
            return value;
        }

        /// <summary>
        /// <para>Author:TrungLD</para>
        /// <para>DateCreated: 03/06/2016</para>
        /// <para>lấy event đang chạy theo kiểu event</para>
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public List<Out_EventPromotion_GetEventRunningByType_Result> EventPromotion_GetEventRunningByType(int typeId)
        {
            return Uow.Context.Out_EventPromotion_GetEventRunningByType(typeId).ToList();
        }

        /// <summary>
        /// Lấy danh sách user trúng thưởng trong event may mắn hằng ngày
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 6/7/2016 CreateBy MinhT 
        /// </history>
        public List<Out_LogUserRecieveLuckyEveryday_GetFullDataForAPI_Result> LogUserRecieveLuckyEveryday_GetFullDataForAPI()
        {
            return Uow.Context.Out_LogUserRecieveLuckyEveryday_GetFullDataForAPI().ToList();
        }

        /// <summary>
        /// Lấy danh sách user trúng thưởng trong event may mắn hằng ngày
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 6/7/2016 CreateBy MinhT 
        /// </history>
        public List<Out_LogUserRecieveLuckyEveryday_GetFullDataForAPI_ByDate_Result> LogUserRecieveLuckyEveryday_GetFullDataForAPI(DateTime dateReport, int periodOfTime, int rowBegin, int rowEnd)
        {
            return
                Uow.Context.Out_LogUserRecieveLuckyEveryday_GetFullDataForAPI_ByDate(dateReport, periodOfTime, rowBegin,
                    rowEnd).ToList();
        }

        /// <summary>
        /// Lấy danh sách  khung thời gian và ngày diễn ra sự kiện may mắn hằng ngày
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 6/7/2016 CreateBy MinhT 
        /// </history>
        public List<Out_E1609_LogConfigLuckyEveryday_GetDateEventEveryDay_Result> Out_E1609_LogConfigLuckyEveryday_GetDateEventEveryDay()
        {
            return Uow.Context.Out_E1609_LogConfigLuckyEveryday_GetDateEventEveryDay().ToList();
        }

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
        public int? Out_E1612_KhuyenMaiTheoMenhGia_Insert(int userid, int chanelid, int platformid, string gameversion, int amount, int cardtype, double percent, string iprequest)
        {
            return Uow.Context.Out_E1612_KhuyenMaiTheoMenhGia_Insert(userid, chanelid, platformid, gameversion, amount, cardtype, percent, iprequest).FirstOrDefault();
        }
    }
}
