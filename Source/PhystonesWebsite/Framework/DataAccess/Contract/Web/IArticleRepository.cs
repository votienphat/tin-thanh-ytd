using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesObject.Entities.WebEntities;
using DataAccess.Repositories.Infrastructure.Contract;

namespace DataAccess.Contract.Web
{
    public interface IArticleRepository : IDaoRepository<Article>
    {
        int SaveData(int Id, string Title, string Image, string ContentBody, int CategoryId);

        List<Out_Article_GetListData_Result> GetListData(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow);

        List<Out_CategoryArticle_GetList_Result> CategoryArticleGetList();

        Out_Article_GetById_Result ArticleGetById(int Id);

        List<Out_Article_GetByCategoryId_Result> GetByCategoryId(int CategoryId);

        Out_Article_GetByTextId_Result ArticleGetByTextId(string textId);

         List<Out_Article_GetArticleBlog_Result> GetArticleBlog(int categoryId, int startIndex, int pageLength,
            out int total);

    }
}
