using System;
using System.Collections.Generic;
using EntitiesObject.Entities.KaraSysEntities;
using EntitiesObject.Entities.WebEntities;
using EntitiesObject.Message.KaraSys;

namespace BussinessObject.RoomModule.Contract
{
    public interface IRoomBusiness
    {
        /// <summary>
        /// Lấy thông tin tất cả phòng
        /// </summary>
        /// <returns></returns>
        List<Out_Room_GetAll_Result> GetAllRoom();

        /// <summary>
        /// Lấy thông tin một lượt sử dụng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Out_RoomLog_Get_Result GetRoomLog(int id);

        /// <summary>
        /// Bắt đầu một lượt sử dụng
        /// </summary>
        /// <param name="roomID"></param>
        /// <param name="startTime"></param>
        /// <param name="customerName"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        Out_RoomLog_Start_Result Start(int roomID, DateTime startTime, string customerName, string note);

        /// <summary>
        /// Kết thúc một lượt sử dụng, lưu thông tin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="runningIncome"></param>
        /// <param name="extraIncome"></param>
        /// <param name="discount">Số tiền được giảm giá</param>
        /// <param name="finalIncome">Số tiền thu thực tế</param>
        /// <param name="customerName"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        bool End(int id, RoomLogStatusEnum status, DateTime startTime, DateTime endTime,
            decimal runningIncome, decimal extraIncome, decimal discount, decimal finalIncome, string customerName,
            string note);
    }
}