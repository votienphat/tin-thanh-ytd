using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using DataAccess.Contract.ContentModule;
using DataAccess.EF;
using DataAccess.Repositories.Infrastructure;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Repositories.ContentModule
{
    public class ArticleRepository : DaoRepository<UserEntities, Article>, IArticleRepository
    {

        public List<Out_Article_SearchByCate_Result> GetArticle(int cate, int starindex, int pagelenght, out int totalRow)
        {
             totalRow = 0;
            var outTotalRow = new ObjectParameter("TotalRow", totalRow);
            var value = Uow.Context.Out_Article_SearchByCate(cate,starindex,pagelenght, outTotalRow).ToList();
            totalRow = outTotalRow.Value == null ? 0 : int.Parse(outTotalRow.Value.ToString());
            return value;
        }

        public List<Out_Article_GetDisplayOnGame_Result> GetTopArticle(int top)
        {
            return Uow.Context.Out_Article_GetDisplayOnGame(top).ToList();
        }

        public Out_Article_GetDisplayOnGameById_Result GetArticleDetail(int articleId)
        {
            return Uow.Context.Out_Article_GetDisplayOnGameById(articleId).FirstOrDefault();
        }

        public Out_Article_GetArticleDetailsForClient_Result GetArticleDetailsForClient(string textid)
        {
            return Uow.Context.Out_Article_GetArticleDetailsForClient(textid).FirstOrDefault();
        }
    }
}
