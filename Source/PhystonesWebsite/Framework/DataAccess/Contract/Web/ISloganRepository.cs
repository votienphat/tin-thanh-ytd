using System.Collections.Generic;
using EntitiesObject.Entities.WebEntities;
using DataAccess.Repositories.Infrastructure.Contract;

namespace DataAccess.Contract.Web
{
    public interface ISloganRepository : IDaoRepository<Slogan>
    {
        List<Out_Slogan_Get_Result> SloganGet();

        Out_Slogan_GetById_Result Get(int id);
        int SaveDataSlogan(int Id, string Title, string Author, string ContentBody, string Language,bool IsActive);

        List<Out_Slogan_GetListData_Result> ListDataSlogan(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow);

    }
}
