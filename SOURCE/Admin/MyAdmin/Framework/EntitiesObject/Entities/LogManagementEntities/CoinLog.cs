//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntitiesObject.Entities.LogManagementEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class CoinLog
    {
        public long Id { get; set; }
        public int UserID { get; set; }
        public int ReasonId { get; set; }
        public Nullable<decimal> CurrenCoin { get; set; }
        public Nullable<decimal> ValueCoin { get; set; }
        public string Description { get; set; }
        public Nullable<int> MatchID { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}