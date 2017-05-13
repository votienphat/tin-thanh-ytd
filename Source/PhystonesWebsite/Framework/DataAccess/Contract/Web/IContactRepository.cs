using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesObject.Entities.WebEntities;
using DataAccess.Repositories.Infrastructure.Contract;

namespace DataAccess.Contract.Web
{
    public interface IContactRepository : IDaoRepository<Contact>
    {
        int SaveData(string Name ,string  Phone ,string Email ,string Messenger);
        List<Out_Contact_GetListData_Result> GetListData(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow);

    }
}
