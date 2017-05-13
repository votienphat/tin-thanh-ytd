using BusinessObject.WebModule.Contract;
using DataAccess.Contract.Web;
using EntitiesObject.Entities.WebEntities;
using System.Collections.Generic;

namespace BusinessObject.MembershipModule
{
    public class WebBusiness : IWebBusiness
    {
        #region Varriables
        private readonly IArticleRepository _articleRepo;
        private readonly IPortfolioRepository _portfolioRepo;
        private readonly IContactRepository _contactRepo;
        private readonly ISloganRepository _sloganRepo;
        private readonly IWorkRepository _workRepo;
        #endregion

        #region Constructor
        public WebBusiness(IArticleRepository articleRepo, IPortfolioRepository portfolioRepo, IContactRepository contactRepo, ISloganRepository sloganRepo, IWorkRepository workRepo)
        {
            _articleRepo = articleRepo;
            _portfolioRepo = portfolioRepo;
            _contactRepo = contactRepo;
            _sloganRepo = sloganRepo;
            _workRepo = workRepo;
        }
        #endregion
        public int SaveDataContact(string Name, string Phone, string Email, string Messenger)
        {
            return _contactRepo.SaveData(Name, Phone, Email, Messenger);
        }
        public int SaveDataPortfolio(int Id, string Name, string Avatar, string About, int CategoryId, string LinkWeb, string LinkProfile)
        {
            return _portfolioRepo.SaveData(Id, Name, Avatar, About, CategoryId, LinkWeb, LinkProfile);
        }
        public int SaveDataArticle(int Id, string Title, string Image, string ContentBody, int CategoryId)
        {
            return _articleRepo.SaveData(Id, Title, Image, ContentBody, CategoryId);
        }
        public List<Out_Contact_GetListData_Result> ListDataContact(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow)
        {
            return _contactRepo.GetListData(rowStart, rowEnd, orderBy, isDescending, out totalRow);
        }
        public List<Out_Portfolio_GetListData_Result> ListDataPortfolio(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow)
        {
            return _portfolioRepo.GetListData(rowStart, rowEnd, orderBy, isDescending, out totalRow);
        }
        public List<Out_Article_GetListData_Result> ListDataArticle(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow)
        {
            return _articleRepo.GetListData(rowStart, rowEnd, orderBy, isDescending, out totalRow);
        }
        public List<Out_CategoryArticle_GetList_Result> CategoryArticleGetList()
        {
            return _articleRepo.CategoryArticleGetList();
        }
        public Out_Article_GetById_Result ArticleGetById(int Id)
        {
            return _articleRepo.ArticleGetById(Id);
        }
        public List<Out_Article_GetByCategoryId_Result> GetByCategoryId(int CategoryId)
        {
            return _articleRepo.GetByCategoryId(CategoryId);
        }
        public List<Out_Slogan_Get_Result> SloganGet()
        {
            return _sloganRepo.SloganGet();
        }
        public Out_Article_GetByTextId_Result ArticleGetByTextId(string textId)
        {
            return _articleRepo.ArticleGetByTextId(textId);
        }
        public List<Out_Portfolio_GetList_Result> GetList()
        {
            return _portfolioRepo.GetList();
        }
        public Out_Portfolio_GetById_Result PortfolioGetById(int Id)
        {
            return _portfolioRepo.PortfolioGetById(Id);
        }
        public List<Out_Portfolio_GetListCategoryPortfolio_Result> GetListCategoryPortfolio()
        {
            return _portfolioRepo.GetListCategoryPortfolio();
        }
        public int SaveDataWork(int Id, string Title, string Image, string ContentBody, int CategoryId)
        {
            return _workRepo.SaveDataWork(Id, Title,Image,ContentBody,CategoryId);
        }
        public List<Out_Work_GetListData_Result> ListDataWork(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow)
        {
            return _workRepo.ListDataWork(rowStart,rowEnd,orderBy,isDescending,out totalRow);
        }
        public List<Out_CategoryWork_GetList_Result> CategoryWorkGetList()
        {
            return _workRepo.CategoryWorkGetList();
        }
        public Out_Work_GetById_Result WorkGetById(int Id)
        {
            return _workRepo.WorkGetById(Id);
        }
        public List<Out_Work_GetByCategoryId_Result> GetWorkByCategoryId(int CategoryId)
        {
            return _workRepo.GetWorkByCategoryId(CategoryId);
        }
        public Out_Work_GetByTextId_Result WorkGetByTextId(string textId)
        {
            return _workRepo.WorkGetByTextId(textId);
        }
    }
}