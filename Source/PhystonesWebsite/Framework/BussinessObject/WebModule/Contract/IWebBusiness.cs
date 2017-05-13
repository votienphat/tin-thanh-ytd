using EntitiesObject.Entities.WebEntities;
using System.Collections.Generic;

namespace BusinessObject.WebModule.Contract
{
    public interface IWebBusiness
    {
        int SaveDataContact(string Name, string Phone, string Email, string Messenger);
        int SaveDataPortfolio(int Id, string Name, string Avatar, string About, int CategoryId, string LinkWeb, string LinkProfile);
        int SaveDataArticle(int Id, string Title, string Image, string ContentBody, int CategoryId);
        List<Out_Contact_GetListData_Result> ListDataContact(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow);
        List<Out_Portfolio_GetListData_Result> ListDataPortfolio(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow);
        List<Out_Article_GetListData_Result> ListDataArticle(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow);
        List<Out_CategoryArticle_GetList_Result> CategoryArticleGetList();
        Out_Article_GetById_Result ArticleGetById(int Id);
        List<Out_Article_GetByCategoryId_Result> GetByCategoryId(int CategoryId);
        List<Out_Slogan_Get_Result> SloganGet();
        Out_Article_GetByTextId_Result ArticleGetByTextId(string textId);
        List<Out_Portfolio_GetList_Result> GetList();
        Out_Portfolio_GetById_Result PortfolioGetById(int Id);
        List<Out_Portfolio_GetListCategoryPortfolio_Result> GetListCategoryPortfolio();
        int SaveDataWork(int Id, string Title, string Image, string ContentBody, int CategoryId);
        List<Out_Work_GetListData_Result> ListDataWork(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow);
        List<Out_CategoryWork_GetList_Result> CategoryWorkGetList();
        Out_Work_GetById_Result WorkGetById(int Id);
        List<Out_Work_GetByCategoryId_Result> GetWorkByCategoryId(int CategoryId);
        Out_Work_GetByTextId_Result WorkGetByTextId(string textId);

    }
}