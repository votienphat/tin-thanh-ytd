using DataAccess.Repositories.Infrastructure;
using EntitiesObject.Entities.WebEntities;
using DataAccess.EF;
using DataAccess.Contract.Web;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace DataAccess.Repositories.Membership
{
    public class SampleRepository : DaoRepository<WebEntities, Sample>, ISampleRepository
    {
        public int SaveData(int Id,string content,int syntaxId)
        {
            return Uow.Context.Out_Sample_Save(Id, content, syntaxId);
        }
        public List<Out_Samples_GetListData_Result> GetListData(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow)
        {
            totalRow = 0;
            var outTotalRow = new ObjectParameter("TotalRow", totalRow);
            var result = Uow.Context.Out_Samples_GetListData(rowStart, rowEnd, orderBy, isDescending, outTotalRow).ToList();
            totalRow = outTotalRow.Value == null ? 0 : int.Parse(outTotalRow.Value.ToString());
            return result;
        }
    }
}