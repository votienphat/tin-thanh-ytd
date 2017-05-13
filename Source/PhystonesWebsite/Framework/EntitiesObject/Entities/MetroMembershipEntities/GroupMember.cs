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
    
    public partial class GroupMember
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GroupMember()
        {
            this.GroupPermissions = new HashSet<GroupPermission>();
            this.MemberAdmins = new HashSet<MemberAdmin>();
        }
    
        public int ID { get; set; }
        public string GroupName { get; set; }
        public Nullable<bool> Visible { get; set; }
        public Nullable<System.DateTime> Datecreated { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupPermission> GroupPermissions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberAdmin> MemberAdmins { get; set; }
    }
}
