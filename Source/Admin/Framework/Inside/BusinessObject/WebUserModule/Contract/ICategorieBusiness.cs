using System.Collections.Generic;
using BusinessObject.MembershipModule.Enums;
using BusinessObject.MembershipModule.Models.Request;
using EntitiesObject.Entities.MetroMembershipEntities;
using EntitiesObject.Entities.MetroUserEntities;

namespace BusinessObject.WebUserModule.Contract
{
    public interface ICategorieBusiness
    {
        IEnumerable<Out_Categories_Get_Result> GetListCategorie();
    }
}
