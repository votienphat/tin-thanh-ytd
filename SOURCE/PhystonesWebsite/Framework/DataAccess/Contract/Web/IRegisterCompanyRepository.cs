using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesObject.Entities.WebEntities;
using DataAccess.Repositories.Infrastructure.Contract;

namespace DataAccess.Contract.Web
{
    public interface IRegisterCompanyRepository : IDaoRepository<RegisterCompany>
    {
        int RegisterCompany(string MST, string CompanyName, string Address, string CEO, int PackedRegister, int TypeRegister, string Email,
string ContactPreson, string ReceiveAddress);
        List<Out_RegisterCompany_GetListData_Result> GetListData(string keyWord,int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow);
    }
}
