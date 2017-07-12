using System;
using System.Collections.Generic;
using BusinessObject.WebModule.Contract;
using DataAccess.Contract.Web;
using EntitiesObject.Entities.WebEntities;
using Logger;
using MyUtility;

namespace BusinessObject.WebModule
{
    public class WebBusiness : IWebBusiness
    {
        #region Varriables

        private readonly IArticleRepository _articleRepo;
        private readonly IPortfolioRepository _portfolioRepo;
        private readonly IContactRepository _contactRepo;
        private readonly ISloganRepository _sloganRepo;
        private readonly IWorkRepository _workRepo;
        private readonly IConfigRepository _configRepo;
        private readonly IRegisterCompanyRepository _conregisterRepo;

        #endregion

        #region Constructor

        public WebBusiness(IArticleRepository articleRepo, IPortfolioRepository portfolioRepo,
            IContactRepository contactRepo, ISloganRepository sloganRepo, IWorkRepository workRepo,
            IConfigRepository configRepo)
        {
            _articleRepo = articleRepo;
            _portfolioRepo = portfolioRepo;
            _contactRepo = contactRepo;
            _sloganRepo = sloganRepo;
            _workRepo = workRepo;
            _configRepo = configRepo;
        }

        #endregion

        public int SaveDataContact(string name, string phone, string email, string messenger, string fromMail,
            string fromMail2, string fromMailName, string fromMailTitle, string toMails, string hostMail, int port)
        {
            try
            {
                // Gửi mail thông báo
                var rawPassword = Common.DecryptBase64(fromMail2);
                var mails = toMails.Split(',');

                foreach (var mail in mails)
                {
                    fromMailTitle = string.Format(fromMailTitle, name);
                    NetworkCommon.SendMail(fromMail, fromMailName, rawPassword, mail, mail, fromMailTitle,
                        string.Format("Bạn có contact mới từ {0}" +
                                      "<br/> Email: {1}" +
                                      "<br/> Số điện thoại: {2}" +
                                      "<br/> Nội dung: {3}",
                                      name, email, phone, messenger), hostMail, port, true);
                }
            }
            catch (Exception ex)
            {
                CommonLogger.DefaultLogger.Error("SaveDataContact", ex);
            }

            return _contactRepo.SaveData(name, phone, email, messenger);
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
            return _workRepo.SaveDataWork(Id, Title, Image, ContentBody, CategoryId);
        }
        public List<Out_Work_GetListData_Result> ListDataWork(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow)
        {
            return _workRepo.ListDataWork(rowStart, rowEnd, orderBy, isDescending, out totalRow);
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
        public Out_Config_GetByKey_Result ConfigGetByKey(string key)
        {
            return _configRepo.ConfigGetByKey(key);
        }
        public int SaveConfigKey(string key, string value)
        {
            return _configRepo.SaveConfigKey(key, value);
        }
        public List<Out_Article_GetArticleBlog_Result> GetArticleBlog(int categoryId, int startIndex, int pageLength,
            out int total)
        {
            return _articleRepo.GetArticleBlog(categoryId, startIndex, pageLength, out total);
        }
        public int RegisterCompany(string MST, string CompanyName, string Address, string CEO, int PackedRegister, int TypeRegister, string Email,
        string ContactPreson, string ReceiveAddress)
        {
            return _conregisterRepo.RegisterCompany(MST, CompanyName, Address, CEO, PackedRegister, TypeRegister, Email, ContactPreson, ReceiveAddress);
        }
        public List<Out_RegisterCompany_GetListData_Result> ListDataRegisterCompany(string keyWord, int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow)
        {
            return _conregisterRepo.GetListData(keyWord, rowStart, rowEnd, orderBy, isDescending, out totalRow);
        }

    }
}