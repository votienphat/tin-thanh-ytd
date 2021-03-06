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
    using EntitiesObject.Entities.VanTaiEntities;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class VanTaiEntities : DbContext
    {
        public VanTaiEntities()
            : base("name=VanTaiEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PO> POes { get; set; }
        public virtual DbSet<PODetail> PODetails { get; set; }
        public virtual DbSet<User> Users { get; set; }
    
        public virtual ObjectResult<PO_Search_Result> PO_Search(string keyword, Nullable<System.DateTime> startTime, Nullable<System.DateTime> endTime, Nullable<int> startIndex, Nullable<int> endIndex, ObjectParameter totalRow)
        {
            var keywordParameter = keyword != null ?
                new ObjectParameter("Keyword", keyword) :
                new ObjectParameter("Keyword", typeof(string));
    
            var startTimeParameter = startTime.HasValue ?
                new ObjectParameter("StartTime", startTime) :
                new ObjectParameter("StartTime", typeof(System.DateTime));
    
            var endTimeParameter = endTime.HasValue ?
                new ObjectParameter("EndTime", endTime) :
                new ObjectParameter("EndTime", typeof(System.DateTime));
    
            var startIndexParameter = startIndex.HasValue ?
                new ObjectParameter("StartIndex", startIndex) :
                new ObjectParameter("StartIndex", typeof(int));
    
            var endIndexParameter = endIndex.HasValue ?
                new ObjectParameter("EndIndex", endIndex) :
                new ObjectParameter("EndIndex", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PO_Search_Result>("PO_Search", keywordParameter, startTimeParameter, endTimeParameter, startIndexParameter, endIndexParameter, totalRow);
        }
    }
}
