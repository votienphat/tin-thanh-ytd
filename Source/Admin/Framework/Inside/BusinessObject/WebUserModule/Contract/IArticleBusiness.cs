using System.Collections.Generic;
using BusinessObject.MembershipModule.Enums;
using BusinessObject.MembershipModule.Models.Request;
using EntitiesObject.Entities.MetroMembershipEntities;

namespace BusinessObject.WebUserModule.Contract
{
    public interface IArticleBusiness
    {
        int SaveArticle(string title, int categorieId, string newsContent, string imageArticle, int status, int isComment,
              string shortDescription, string seoLink, string createBy, int id);
    }
}
