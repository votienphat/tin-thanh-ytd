//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntitiesObject.Entities.UserEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ItemGame
    {
        public int ID { get; set; }
        public string ItemName { get; set; }
        public int TypeItem { get; set; }
        public int StatusItem { get; set; }
        public int ParentItemID { get; set; }
        public int QuantitySplit { get; set; }
        public int Quantity { get; set; }
        public string ImagePath { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public decimal MoneyItems { get; set; }
        public int TimeEffect { get; set; }
        public Nullable<int> SubNumber { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsUseBoxGift { get; set; }
        public Nullable<decimal> MaxPriceMarket { get; set; }
        public Nullable<decimal> MinPriceMarket { get; set; }
        public Nullable<decimal> PricePromotion { get; set; }
        public Nullable<decimal> PriceRemove { get; set; }
        public Nullable<int> TimePromotion { get; set; }
        public Nullable<int> CardType { get; set; }
    }
}