using System.Collections.Generic;
using DataAccess.Contract.Membership;
using DataAccess.Contract.WebUser;
using DataAccess.Entity;
using DataAccess.Repositories.Infrastructure;
using EntitiesObject.Entities.MetroUserEntities;

namespace DataAccess.Repositories.WebUser
{
    public class ArticleRepository : DaoRepository<MetroUserEntities, Article>, IArticleRepository
    {
        public int SaveArticle(string title, int categorieId, string newsContent, string imageArticle, int status, int isComment,
            string shortDescription, string seoLink, string createBy, int id)
        {
            var rs = Uow.Context.Ins_Article_Save(title,categorieId,newsContent,imageArticle,status,isComment,
                shortDescription,seoLink,createBy,id);
            return rs;
        }
    }
}
