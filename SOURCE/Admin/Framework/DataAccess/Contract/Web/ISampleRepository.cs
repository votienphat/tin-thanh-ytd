using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesObject.Entities.WebEntities;
using DataAccess.Repositories.Infrastructure.Contract;

namespace DataAccess.Contract.Web
{
    public interface ISampleRepository : IDaoRepository<Sample>
    {
        int SaveData(int Id, string content, int syntaxId);

        List<Out_Samples_GetListData_Result> GetListData(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow);
    }
}
