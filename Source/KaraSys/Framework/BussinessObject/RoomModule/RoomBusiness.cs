using System;
using System.Collections.Generic;
using BussinessObject.RoomModule.Contract;
using DataAccess.Contract.KaraSys;
using DataAccess.Contract.Web;
using EntitiesObject.Entities.KaraSysEntities;
using EntitiesObject.Entities.WebEntities;
using EntitiesObject.Message.KaraSys;

namespace BussinessObject.RoomModule
{
    public class RoomBusiness : IRoomBusiness
    {
        #region Varriables

        private readonly IRoomLogRepository _roomLogRepository;
        private readonly IRoomRepository _roomRepository;

        #endregion

        #region Constructor

        public RoomBusiness(IRoomLogRepository roomLogRepository, IRoomRepository roomRepository)
        {
            _roomLogRepository = roomLogRepository;
            _roomRepository = roomRepository;
        }

        #endregion

        public List<Out_Room_GetAll_Result> GetAllRoom()
        {
            return _roomRepository.GetAll();
        }

        public Out_RoomLog_Get_Result GetRoomLog(int id)
        {
            return _roomLogRepository.Get(id);
        }

        public Out_RoomLog_Start_Result Start(int roomID, DateTime startTime, string customerName, string note)
        {
            return _roomLogRepository.Start(roomID, RoomLogStatusEnum.Running, startTime, customerName, note);
        }

        public bool End(int id, RoomLogStatusEnum status, DateTime startTime, DateTime endTime, decimal runningIncome,
            decimal extraIncome, decimal discount, decimal finalIncome, string customerName, string note)
        {
            // Tính số phút sử dụng
            int runningTime = (int)(endTime - startTime).TotalMinutes;

            return _roomLogRepository.End(id, status, startTime, endTime, runningTime, runningIncome,
                extraIncome, discount, finalIncome, customerName, note);
        }
    }
}