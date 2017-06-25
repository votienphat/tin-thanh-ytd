using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VanTaiSystem.Modules.Base.Models;
using VanTaiSystem.Modules.Base.Resources;

namespace VanTaiSystem.Modules.Membership.Models
{
    public class ApiUpdatePasswordRequest : ApiBaseRequest
    {
        [DisplayName("userId")]
        [Required(ErrorMessage = BaseResource.ApiStatusCode.InvalidInput)]
        public int UserId { get; set; }

        [DisplayName("oldPassword")]
        [Required(ErrorMessage = BaseResource.ApiStatusCode.InvalidInput)]
        [MaxLength(50, ErrorMessage = BaseResource.ApiStatusCode.InvalidInputMaxMin)]
        [MinLength(5, ErrorMessage = BaseResource.ApiStatusCode.InvalidInputMaxMin)]
        public string OldPassword { get; set; }

        [DisplayName("newPassword")]
        [Required(ErrorMessage = BaseResource.ApiStatusCode.InvalidInput)]
        [MaxLength(50, ErrorMessage = BaseResource.ApiStatusCode.InvalidInputMaxMin)]
        [MinLength(5, ErrorMessage = BaseResource.ApiStatusCode.InvalidInputMaxMin)]
        public string NewPassword { get; set; }
    }

}