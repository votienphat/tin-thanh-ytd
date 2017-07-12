using DataAccess.Repositories.Infrastructure;
using DataAccess.EF;
using DataAccess.Contract.Web;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using EntitiesObject.Entities.WebEntities;

namespace DataAccess.Repositories.Membership
{
    public class RegisterCompanyRepository : DaoRepository<WebEntities, RegisterCompany>, IRegisterCompanyRepository
    {
        public int RegisterCompany(string MST,string CompanyName,string Address,string CEO,int PackedRegister,int TypeRegister,string Email,
string ContactPreson,string ReceiveAddress)
        {
            return Uow.Context.Out_RegisterCompany_Save(MST,CompanyName,Address,CEO,PackedRegister,TypeRegister,Email,ContactPreson,ReceiveAddress).FirstOrDefault().GetValueOrDefault();
        }
         public List<Out_RegisterCompany_GetListData_Result> GetListData(string keyWord,int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow)
        {
            totalRow = 0;
            var outTotalRow = new ObjectParameter("TotalRow", totalRow);
            var result = Uow.Context.Out_RegisterCompany_GetListData(keyWord,rowStart, rowEnd, orderBy, isDescending, outTotalRow).ToList();
            totalRow = outTotalRow.Value == null ? 0 : int.Parse(outTotalRow.Value.ToString());
            return result;
        }
    }
}