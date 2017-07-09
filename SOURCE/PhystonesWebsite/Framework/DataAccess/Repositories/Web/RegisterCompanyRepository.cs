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
    }
}