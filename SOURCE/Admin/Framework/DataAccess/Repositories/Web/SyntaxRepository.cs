using DataAccess.Repositories.Infrastructure;
using EntitiesObject.Entities.WebEntities;
using DataAccess.EF;
using DataAccess.Contract.Web;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace DataAccess.Repositories.Membership
{
    public class SyntaxRepository : DaoRepository<WebEntities, Syntax>, ISyntaxRepository
    {
        public int SaveData(int Id,string name, string contentsyntax, string keyword, int categoryId, string description)
        {
            return Uow.Context.Out_Syntax_Save(Id, name, contentsyntax, keyword, categoryId, description);
        }
        public List<Out_Syntax_GetAll_Result> SyntaxGetAll(string name)
        {
            return Uow.Context.Out_Syntax_GetAll(name).ToList();
        }
        public List<Out_Syntax_GetListData_Result> GetListData(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow)
        {
            totalRow = 0;
            var outTotalRow = new ObjectParameter("TotalRow", totalRow);
            var result = Uow.Context.Out_Syntax_GetListData(rowStart, rowEnd, orderBy, isDescending, outTotalRow).ToList();
            totalRow = outTotalRow.Value == null ? 0 : int.Parse(outTotalRow.Value.ToString());
            return result;
        }
    }
}