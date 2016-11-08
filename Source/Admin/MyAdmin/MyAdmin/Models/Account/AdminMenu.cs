using System.Collections.Generic;

namespace MyAdmin.Models.Account
{
    public class AdminMenu
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string PageLink { get; set; }
        public int? ParentId { get; set; }
        public int? OrderNo { get; set; }
        public int? AppId { get; set; }
        public List<AdminMenu> SubMenus { get; set; }

        public AdminMenu()
        {
            SubMenus = new List<AdminMenu>();
        }
    }
}