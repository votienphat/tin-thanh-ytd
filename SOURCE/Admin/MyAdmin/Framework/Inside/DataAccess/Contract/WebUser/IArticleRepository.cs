using System.Collections.Generic;
using DataAccess.Repositories.Infrastructure.Contract;
using EntitiesObject.Entities.MetroUserEntities;

namespace DataAccess.Contract.WebUser
{

    public interface IArticleRepository : IDaoRepository<Article>
    {
        int SaveArticle(string title,int categorieId,string newsContent,string imageArticle,int status,int isComment,
            string shortDescription,string seoLink,string createBy,int id);
    }
}