<<<<<<< HEAD
﻿using EntitiesObject.Entities.WebEntities;
using System.Collections.Generic;

namespace BusinessObject.WebModule.Contract
{
    public interface IWebBusiness
    {
        int SaveDataContact(string name, string phone, string email, string messenger, string fromMail,
            string fromMail2, string fromMailName, string fromMailTitle, string toMails, string hostMail, int port);

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
        Out_Config_GetByKey_Result ConfigGetByKey(string key);
        int SaveConfigKey(string key, string value);
        List<Out_Article_GetArticleBlog_Result> GetArticleBlog(int categoryId, int startIndex, int pageLength,
          out int total);
        int RegisterCompany(string MST, string CompanyName, string Address, string CEO, int PackedRegister, int TypeRegister, string Email, string ContactPreson, string ReceiveAddress);
        List<Out_RegisterCompany_GetListData_Result> ListDataRegisterCompany(string keyWord, int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow);
    }
=======
﻿using EntitiesObject.Entities.WebEntities;
using System.Collections.Generic;
using EntitiesObject.Message.Content;
using EntitiesObject.Message.Enum;

namespace BusinessObject.WebModule.Contract
{
    public interface IWebBusiness
    {
        int SaveDataContact(string name, string phone, string email, string messenger, string fromMail,
            string fromMail2, string fromMailName, string fromMailTitle, string toMails, string hostMail, int port);

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
        Out_Config_GetByKey_Result ConfigGetByKey(string key);
        int SaveConfigKey(string key, string value);
        List<Out_Article_GetArticleBlog_Result> GetArticleBlog(int categoryId, int startIndex, int pageLength,
          out int total);
        int RegisterCompany(string MST, string CompanyName, string Address, string CEO, int PackedRegister, int TypeRegister, string Email,string ContactPreson, string ReceiveAddress);

        Out_Slogan_GetById_Result GetSlogan(SloganEnum id);
        List<Out_Plain_GetByType_Result> GetPlainByType(PlainEnum type);
        List<Out_Slogan_GetListData_Result>ListDataSlogan(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow);
        int SaveDataSlogan(int Id, string Title, string Author, string ContentBody, string Language, bool IsActive);
    }
>>>>>>> 7c95c30dc45d72cd825a1900048f07bb52b4624c
}