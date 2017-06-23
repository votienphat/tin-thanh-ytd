//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntitiesObject.Entities.MetroMembershipEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class PageFunction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PageFunction()
        {
            this.GroupPermissions = new HashSet<GroupPermission>();
            this.MemberPermissions = new HashSet<MemberPermission>();
        }
    
        public int ID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public int PageReferID { get; set; }
        public string Name { get; set; }
        public Nullable<int> Status { get; set; }
        public string Link { get; set; }
        public string LinkUse { get; set; }
        public Nullable<int> FunctionPage { get; set; }
        public Nullable<int> PageType { get; set; }
        public Nullable<bool> IsEnable { get; set; }
        public Nullable<bool> IsTargetBlank { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<int> SortNum { get; set; }
        public Nullable<int> AppID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupPermission> GroupPermissions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberPermission> MemberPermissions { get; set; }
    }
}
