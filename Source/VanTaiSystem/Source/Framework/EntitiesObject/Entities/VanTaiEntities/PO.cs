//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntitiesObject.Entities.VanTaiEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class PO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PO()
        {
            this.PODetails = new HashSet<PODetail>();
        }
    
        public string POId { get; set; }
        public Nullable<System.DateTime> PODate { get; set; }
        public string RequestBy { get; set; }
        public string DeliveryTo { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsComplete { get; set; }
    
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PODetail> PODetails { get; set; }
    }
}