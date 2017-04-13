using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesObject.Entities.WebEntities;
using DataAccess.Repositories.Infrastructure.Contract;

namespace DataAccess.Contract.Web
{
    public interface ISyntaxRepository : IDaoRepository<Syntax>
    {
        int SaveData(int Id, string name, string contentsyntax, string keyword, int categoryId, string description);
        List<Out_Syntax_GetAll_Result> SyntaxGetAll(string name);
        List<Out_Syntax_GetListData_Result> GetListData(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow);

    }
}
