using EntitiesObject.Entities.WebEntities;
using System.Collections.Generic;

namespace BusinessObject.WebModule.Contract
{
    public interface IWebBusiness
    {
        int SaveDataCategory(int Id, string name, string keyword, string imagepatch);

        int SaveDataSample(int Id, string content, int syntaxId);

        int SaveDataSyntax(int Id, string name, string contentsyntax, string keyword, int categoryId, string description);

        List<Out_Syntax_GetAll_Result> SyntaxGetAll(string name);

        List<Out_Category_GetListData_Result> ListDataCategory(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow);

        List<Out_Samples_GetListData_Result> ListDataSamples(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow);

        List<Out_Syntax_GetListData_Result> ListDataSyntax(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow);
    }
}