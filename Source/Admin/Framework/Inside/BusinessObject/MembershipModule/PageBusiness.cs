using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObject.MembershipModule.Contract;
using BusinessObject.MembershipModule.Models;
using DataAccess.Contract.Membership;
using EntitiesObject.Entities.MetroMembershipEntities;

namespace BusinessObject.MembershipModule
{
    public class PageBusiness : IPageBusiness
    {
        #region Varriables

        private readonly IMemberPermissionRepository _memberPermissionRepo;
        private readonly IMemberAdminRepository _memberAdminRepo;

        #endregion

        #region Constructor

        public PageBusiness(IMemberPermissionRepository memberPermissionRepo, IMemberAdminRepository memberAdminRepo)
        {
            _memberPermissionRepo = memberPermissionRepo;
            _memberAdminRepo = memberAdminRepo;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Lấy danh sách 
        /// </summary>
        /// <returns></returns>
        public List<PageFunctionModel> GetPages(bool isGetPage = false)
        {
            return isGetPage ? GetSubPages(_memberAdminRepo.GetPages()) : GetSubActions(_memberAdminRepo.GetPages());
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Đệ quy phân cấp menu và action
        /// </summary>
        /// <param name="pages"></param>
        /// <param name="level"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<PageFunctionModel> GetSubActions(List<PageFunction> pages, int level = 0, int? parentId = 0)
        {
            var subPages = pages
                .Where(m => m.ParentID == parentId)
                .Select(x => new PageFunctionModel
                {
                    AppID = x.AppID,
                    CreatedDate = x.CreatedDate.GetValueOrDefault(DateTime.Now),
                    FunctionPage = x.FunctionPage.GetValueOrDefault(0),
                    IsEnable = x.IsEnable.GetValueOrDefault(false),
                    IsTargetBlank = x.IsTargetBlank.GetValueOrDefault(false),
                    Link = x.Link,
                    LinkUse = x.LinkUse,
                    PageId = x.ID,
                    PageName = x.Name,
                    PageType = x.PageType.GetValueOrDefault(0),
                    ParentId = x.ParentID,
                    ReferPage = x.PageReferID,
                    SortNum = x.SortNum.GetValueOrDefault(0),
                    SubPages = new List<PageFunctionModel>(),
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate,
                    Level = level
                })
                .OrderBy(x => x.SortNum)
                .ToList();

            foreach (var page in subPages)
            {
                page.SubPages = pages.Any(m => m.ParentID == page.PageId) ? GetSubActions(pages, level + 1, page.PageId) : new List<PageFunctionModel>();
            }

            return subPages;
        }

        /// <summary>
        /// Đệ quy phân cấp menu
        /// </summary>
        /// <param name="pages"></param>
        /// <param name="level"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<PageFunctionModel> GetSubPages(List<PageFunction> pages, int level = 0, int? parentId = 0)
        {
            var subPages = pages
                .Where(m => m.ParentID == parentId && m.PageType == 1)
                .Select(x => new PageFunctionModel
                {
                    AppID = x.AppID,
                    CreatedDate = x.CreatedDate.GetValueOrDefault(DateTime.Now),
                    FunctionPage = x.FunctionPage.GetValueOrDefault(0),
                    IsEnable = x.IsEnable.GetValueOrDefault(false),
                    IsTargetBlank = x.IsTargetBlank.GetValueOrDefault(false),
                    Link = x.Link,
                    LinkUse = x.LinkUse,
                    PageId = x.ID,
                    PageName = x.Name,
                    PageType = x.PageType.GetValueOrDefault(0),
                    ParentId = x.ParentID,
                    ReferPage = x.PageReferID,
                    SortNum = x.SortNum.GetValueOrDefault(0),
                    SubPages = new List<PageFunctionModel>(),
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate,
                    Level = level
                })
                .OrderBy(x => x.SortNum)
                .ToList();

            foreach (var page in subPages)
            {
                // Lấy quyền cao nhất mà trang có thể có
                var functionPage =
                    pages.Where(m => m.ParentID == page.PageId && m.PageType == 2).Max(m => m.FunctionPage).GetValueOrDefault(0);
                page.FunctionPage = page.FunctionPage > functionPage ? page.FunctionPage : functionPage;

                page.SubPages = pages.Any(m => m.ParentID == page.PageId) ? GetSubPages(pages, level + 1, page.PageId) : new List<PageFunctionModel>();
            }

            return subPages;
        }

        #endregion
    }
}