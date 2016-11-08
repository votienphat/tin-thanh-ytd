using System;
using System.Collections.Generic;

namespace BusinessObject.MembershipModule.Models
{
    public class PageFunctionModel
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public int FunctionPage { get; set; }
        public int PageType { get; set; }
        public string Link { get; set; }
        public string LinkUse { get; set; }
        public int? ParentId { get; set; }
        public int ReferPage { get; set; }
        public bool IsEnable { get; set; }
        public bool IsTargetBlank { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int SortNum { get; set; }
        public int? AppID { get; set; }
        public int Level { get; set; }
        public List<PageFunctionModel> SubPages { get; set; }
    }
}
