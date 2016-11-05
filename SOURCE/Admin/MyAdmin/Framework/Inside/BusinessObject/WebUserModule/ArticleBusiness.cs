using System;
using System.Collections.Generic;
using BusinessObject.MembershipModule.Enums;
using BusinessObject.MembershipModule.Models.Request;
using BusinessObject.WebUserModule.Contract;
using DataAccess.Contract.Membership;
using DataAccess.Contract.WebUser;
using MyUtility.Extensions;
using Newtonsoft.Json;

namespace BusinessObject.WebUserModule
{
    public class ArticleBusiness : IArticleBusiness
    {
        #region Viriables

        private readonly IArticleRepository _articleRepo;
        #endregion

        #region Constructor

        public ArticleBusiness(IArticleRepository articleRepo)
        {
            _articleRepo = articleRepo;
        }

        #endregion

        #region Method

       
        #endregion

        public int SaveArticle(string title, int categorieId, string newsContent, string imageArticle, int status, int isComment,
            string shortDescription, string seoLink, string createBy, int id)
        {
            return _articleRepo.SaveArticle(title, categorieId, newsContent, imageArticle, status, isComment,
                shortDescription, seoLink, createBy, id);
        }
    }
}
