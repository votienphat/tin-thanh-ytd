using BusinessObject.WebModule.Contract;
using DataAccess.Contract.Web;
using EntitiesObject.Entities.WebEntities;
using System.Collections.Generic;

namespace BusinessObject.MembershipModule
{
    public class WebBusiness : IWebBusiness
    {
        #region Varriables
        private readonly ISampleRepository _sampleRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ISyntaxRepository _syntaxRepo;
        #endregion

        #region Constructor
        public WebBusiness(ISampleRepository sampleRepo, ICategoryRepository categoryRepo, ISyntaxRepository syntaxRepo)
        {
            _sampleRepo = sampleRepo;
            _categoryRepo = categoryRepo;
            _syntaxRepo = syntaxRepo;
        }
        #endregion
        public int SaveDataCategory(int Id, string name, string keyword, string imagepatch)
        {
            return _categoryRepo.SaveData(Id, name, keyword, imagepatch);
        }
        public int SaveDataSample(int Id, string content, int syntaxId)
        {
            return _sampleRepo.SaveData(Id, content, syntaxId);
        }
        public int SaveDataSyntax(int Id, string name, string contentsyntax, string keyword, int categoryId, string description)
        {
            return _syntaxRepo.SaveData(Id, name, contentsyntax, keyword,categoryId,description);
        }
        public List<Out_Syntax_GetAll_Result> SyntaxGetAll(string name)
        {
            return _syntaxRepo.SyntaxGetAll(name);
        }
        public List<Out_Category_GetListData_Result> ListDataCategory(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow)
        {
            return _categoryRepo.GetListData(rowStart,rowEnd,orderBy,isDescending,out totalRow);
        }
        public List<Out_Samples_GetListData_Result> ListDataSamples(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow)
        {
            return _sampleRepo.GetListData(rowStart, rowEnd, orderBy, isDescending, out totalRow);
        }
        public List<Out_Syntax_GetListData_Result> ListDataSyntax(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow)
        {
            return _syntaxRepo.GetListData(rowStart, rowEnd, orderBy, isDescending, out totalRow);
        }
    }
}