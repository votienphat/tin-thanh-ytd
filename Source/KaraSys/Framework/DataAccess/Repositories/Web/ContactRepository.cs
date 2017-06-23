using DataAccess.Repositories.Infrastructure;
using EntitiesObject.Entities.WebEntities;
using DataAccess.EF;
using DataAccess.Contract.Web;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace DataAccess.Repositories.Web
{
    public class ContactRepository : DaoRepository<WebEntities, Contact>, IContactRepository
    {
        public int SaveData(string Name, string Phone, string Email, string Messenger)
        {
            return Uow.Context.Out_Contact_Save(Name, Phone, Email, Messenger);
        }
        
        public List<Out_Contact_GetListData_Result> GetListData(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow)
        {
            totalRow = 0;
            var outTotalRow = new ObjectParameter("TotalRow", totalRow);
            var result = Uow.Context.Out_Contact_GetListData(rowStart, rowEnd, orderBy, isDescending, outTotalRow).ToList();
            totalRow = outTotalRow.Value == null ? 0 : int.Parse(outTotalRow.Value.ToString());
            return result;
        }
    }
}