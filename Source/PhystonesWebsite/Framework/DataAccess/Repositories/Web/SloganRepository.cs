using DataAccess.Repositories.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Contract.Web;
using DataAccess.EF;
using EntitiesObject.Entities.WebEntities;
using System.Data.Entity.Core.Objects;

namespace DataAccess.Repositories.Web
{
    public class SloganRepository : DaoRepository<WebEntities, Slogan>, ISloganRepository
    {
        public List<Out_Slogan_Get_Result> SloganGet()
        {
            return Uow.Context.Out_Slogan_Get().ToList();
        }
        public Out_Slogan_GetById_Result Get(int id)
        {
            return Uow.Context.Out_Slogan_GetById(id).FirstOrDefault();
        }

        public int SaveDataSlogan(int Id, string Title, string Author, string ContentBody, string Language, bool IsActive)
        {
            return Uow.Context.Out_Slogan_Save(Id, Title, Author, ContentBody, Language, IsActive);
        }

        public List<Out_Slogan_GetListData_Result> ListDataSlogan(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow)
        {
            totalRow = 0;
            var outTotalRow = new ObjectParameter("TotalRow", totalRow);
            var result = Uow.Context.Out_Slogan_GetListData(rowStart, rowEnd, orderBy, isDescending, outTotalRow).ToList();
            totalRow = outTotalRow.Value == null ? 0 : int.Parse(outTotalRow.Value.ToString());
            return result;
        }
    }
}