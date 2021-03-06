﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using EntitiesObject.Entities.KaraSysEntities;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class KaraSysEntities : DbContext
    {
        public KaraSysEntities()
            : base("name=KaraSysEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PriceUnit> PriceUnits { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomLog> RoomLogs { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
    
        public virtual ObjectResult<Out_Room_GetAll_Result> Out_Room_GetAll()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Out_Room_GetAll_Result>("Out_Room_GetAll");
        }
    
        public virtual ObjectResult<Out_RoomLog_Get_Result> Out_RoomLog_Get(Nullable<int> roomLogID)
        {
            var roomLogIDParameter = roomLogID.HasValue ?
                new ObjectParameter("RoomLogID", roomLogID) :
                new ObjectParameter("RoomLogID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Out_RoomLog_Get_Result>("Out_RoomLog_Get", roomLogIDParameter);
        }
    
        public virtual ObjectResult<Out_RoomLog_Start_Result> Out_RoomLog_Start(Nullable<int> roomID, Nullable<int> status, Nullable<System.DateTime> startTime, string customerName, string note)
        {
            var roomIDParameter = roomID.HasValue ?
                new ObjectParameter("RoomID", roomID) :
                new ObjectParameter("RoomID", typeof(int));
    
            var statusParameter = status.HasValue ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(int));
    
            var startTimeParameter = startTime.HasValue ?
                new ObjectParameter("StartTime", startTime) :
                new ObjectParameter("StartTime", typeof(System.DateTime));
    
            var customerNameParameter = customerName != null ?
                new ObjectParameter("CustomerName", customerName) :
                new ObjectParameter("CustomerName", typeof(string));
    
            var noteParameter = note != null ?
                new ObjectParameter("Note", note) :
                new ObjectParameter("Note", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Out_RoomLog_Start_Result>("Out_RoomLog_Start", roomIDParameter, statusParameter, startTimeParameter, customerNameParameter, noteParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> Out_RoomLog_End(Nullable<int> roomLogID, Nullable<int> status, Nullable<System.DateTime> startTime, Nullable<System.DateTime> endTime, Nullable<int> runningTime, Nullable<decimal> runningIncome, Nullable<decimal> extraIncome, Nullable<decimal> discount, Nullable<decimal> finalIncome, string customerName, string note)
        {
            var roomLogIDParameter = roomLogID.HasValue ?
                new ObjectParameter("RoomLogID", roomLogID) :
                new ObjectParameter("RoomLogID", typeof(int));
    
            var statusParameter = status.HasValue ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(int));
    
            var startTimeParameter = startTime.HasValue ?
                new ObjectParameter("StartTime", startTime) :
                new ObjectParameter("StartTime", typeof(System.DateTime));
    
            var endTimeParameter = endTime.HasValue ?
                new ObjectParameter("EndTime", endTime) :
                new ObjectParameter("EndTime", typeof(System.DateTime));
    
            var runningTimeParameter = runningTime.HasValue ?
                new ObjectParameter("RunningTime", runningTime) :
                new ObjectParameter("RunningTime", typeof(int));
    
            var runningIncomeParameter = runningIncome.HasValue ?
                new ObjectParameter("RunningIncome", runningIncome) :
                new ObjectParameter("RunningIncome", typeof(decimal));
    
            var extraIncomeParameter = extraIncome.HasValue ?
                new ObjectParameter("ExtraIncome", extraIncome) :
                new ObjectParameter("ExtraIncome", typeof(decimal));
    
            var discountParameter = discount.HasValue ?
                new ObjectParameter("Discount", discount) :
                new ObjectParameter("Discount", typeof(decimal));
    
            var finalIncomeParameter = finalIncome.HasValue ?
                new ObjectParameter("FinalIncome", finalIncome) :
                new ObjectParameter("FinalIncome", typeof(decimal));
    
            var customerNameParameter = customerName != null ?
                new ObjectParameter("CustomerName", customerName) :
                new ObjectParameter("CustomerName", typeof(string));
    
            var noteParameter = note != null ?
                new ObjectParameter("Note", note) :
                new ObjectParameter("Note", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("Out_RoomLog_End", roomLogIDParameter, statusParameter, startTimeParameter, endTimeParameter, runningTimeParameter, runningIncomeParameter, extraIncomeParameter, discountParameter, finalIncomeParameter, customerNameParameter, noteParameter);
        }
    }
}
