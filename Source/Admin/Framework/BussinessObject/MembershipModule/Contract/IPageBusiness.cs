using System.Collections.Generic;
using BusinessObject.MembershipModule.Models;

namespace BusinessObject.MembershipModule.Contract
{
    public interface IPageBusiness
    {
        /// <summary>
        /// Lấy danh sách page
        /// </summary>
        /// <returns></returns>
        List<PageFunctionModel> GetPages(bool isGetPage = false);
    }
}