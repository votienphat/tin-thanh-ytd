using System.Collections.Generic;
using DataAccess.Infrastructure.Contract;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Contract.ContentModule
{
    public interface IArticleRepository : IDaoRepository<Article>
    {
        List<Out_Article_SearchByCate_Result> GetArticle(int cate,int starindex,int pagelenght,out int totalrow);

        Out_Article_GetDisplayOnGameById_Result GetArticleDetail(int articleId);
        Out_Article_GetArticleDetailsForClient_Result GetArticleDetailsForClient(string textid);

        List<Out_Article_GetDisplayOnGame_Result> GetTopArticle(int top);

    }
}
