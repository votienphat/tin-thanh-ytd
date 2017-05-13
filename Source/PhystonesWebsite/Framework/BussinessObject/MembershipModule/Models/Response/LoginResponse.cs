using BusinessObject.MembershipModule.Enums;
using EntitiesObject.Entities.MetroMembershipEntities;

namespace BussinessObject.MembershipModule.Models.Response
{
    public class LoginResponse
    {
        public MembershipCode Result { get; set; }

        public Ins_MemberAdmin_checkLogin_Result User { get; set; }

        public LoginResponse()
        {
            Result = MembershipCode.SystemError;
        }
    }
}