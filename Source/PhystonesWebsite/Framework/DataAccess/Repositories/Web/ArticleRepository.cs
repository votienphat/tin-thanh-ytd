using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using DataAccess.Contract.Web;
using DataAccess.EF;
using DataAccess.Repositories.Infrastructure;
using EntitiesObject.Entities.WebEntities;

namespace DataAccess.Repositories.Web
{
    public class ArticleRepository : DaoRepository<WebEntities, Article>, IArticleRepository
    {
        public int SaveData(int Id, string Title, string Image, string ContentBody, int CategoryId)
        {
            return Uow.Context.Out_Article_Save(Id, Title, Image, ContentBody, CategoryId);
        }
        public List<Out_Article_GetListData_Result> GetListData(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow)
        {
            totalRow = 0;
            var outTotalRow = new ObjectParameter("TotalRow", totalRow);
            var result = Uow.Context.Out_Article_GetListData(rowStart, rowEnd, orderBy, isDescending, outTotalRow).ToList();
            totalRow = outTotalRow.Value == null ? 0 : int.Parse(outTotalRow.Value.ToString());
            return result;
        }
        public List<Out_CategoryArticle_GetList_Result> CategoryArticleGetList()
        {
            var result = Uow.Context.Out_CategoryArticle_GetList().ToList();
            return result;
        }
        public Out_Article_GetById_Result ArticleGetById(int Id)
        {
            var result = Uow.Context.Out_Article_GetById(Id).FirstOrDefault();
            return result;
        }
        public List<Out_Article_GetByCategoryId_Result> GetByCategoryId(int CategoryId)
        {
            var result = Uow.Context.Out_Article_GetByCategoryId(CategoryId).ToList();
            return result;
        }
        public Out_Article_GetByTextId_Result ArticleGetByTextId(string textId)
        {
            var result = Uow.Context.Out_Article_GetByTextId(textId).FirstOrDefault();
            return result;
        }

        public List<Out_Article_GetArticleBlog_Result> GetArticleBlog(int categoryId, int startIndex, int pageLength, out int total)
        {

            var outTotal = new ObjectParameter("totalRow", typeof(int));
            var result =
                Uow.Context.Out_Article_GetArticleBlog(categoryId, startIndex, pageLength, outTotal)
                    .ToList();
            int.TryParse(outTotal.Value.ToString(), out total);
            return result;
        }
    }
}