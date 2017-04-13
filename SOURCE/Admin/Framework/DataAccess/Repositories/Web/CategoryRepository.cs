using DataAccess.Repositories.Infrastructure;
using EntitiesObject.Entities.WebEntities;
using DataAccess.EF;
using DataAccess.Contract.Web;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace DataAccess.Repositories.Membership
{
    public class CategoryRepository : DaoRepository<WebEntities, Category>, ICategoryRepository
    {
        public int SaveData(int Id,string name, string keyword, string imagepatch)
        {
            return Uow.Context.Out_Category_Save(Id, name, keyword, imagepatch);
        }

        public List<Out_Category_GetListData_Result> GetListData(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow)
        {
            totalRow = 0;
            var outTotalRow = new ObjectParameter("TotalRow", totalRow);
            var result = Uow.Context.Out_Category_GetListData(rowStart, rowEnd, orderBy, isDescending, outTotalRow).ToList();
            totalRow = outTotalRow.Value == null ? 0 : int.Parse(outTotalRow.Value.ToString());
            return result;
        }
    }
}