using System;
using DataAccess.Repositories.Infrastructure;
using DataAccess.EF;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Contract.KaraSys;
using EntitiesObject.Entities.KaraSysEntities;
using EntitiesObject.Message.KaraSys;

namespace DataAccess.Repositories.KaraSys
{
    public class RoomLogRepository : DaoRepository<KaraSysEntities, RoomLog>, IRoomLogRepository
    {

        public Out_RoomLog_Get_Result Get(int id)
        {
            return Uow.Context.Out_RoomLog_Get(id).FirstOrDefault();
        }

        public Out_RoomLog_Start_Result Start(int roomID, RoomLogStatusEnum status, DateTime startTime, string customerName,
            string note)
        {
            return Uow.Context.Out_RoomLog_Start(roomID, (int)status, startTime, customerName, note).FirstOrDefault();
        }

        public bool End(int id, RoomLogStatusEnum status, DateTime startTime, DateTime endTime, int runningTime, decimal runningIncome,
            decimal extraIncome, decimal discount, decimal finalIncome, string customerName, string note)
        {
            return
                Uow.Context.Out_RoomLog_End(id, (int) status, startTime, endTime, runningTime, runningIncome,
                    extraIncome, discount, finalIncome, customerName, note).FirstOrDefault() > 0;
        }
    }
}