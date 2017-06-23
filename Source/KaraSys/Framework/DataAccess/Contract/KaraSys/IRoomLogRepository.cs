using System;
using System.Collections.Generic;
using DataAccess.Repositories.Infrastructure.Contract;
using EntitiesObject.Entities.KaraSysEntities;
using EntitiesObject.Message.KaraSys;

namespace DataAccess.Contract.KaraSys
{
    public interface IRoomLogRepository : IDaoRepository<RoomLog>
    {
        Out_RoomLog_Get_Result Get(int id);

        Out_RoomLog_Start_Result Start(int roomID, RoomLogStatusEnum status, DateTime startTime, string customerName, string note);

        bool End(int id, RoomLogStatusEnum status, DateTime startTime, DateTime endTime, int runningTime,
            decimal runningIncome, decimal extraIncome, decimal discount, decimal finalIncome, string customerName,
            string note);
    }
}
