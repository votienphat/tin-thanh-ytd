using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VanTaiSystem.Modules.Base.Models;
using VanTaiSystem.Modules.Base.Resources;

namespace VanTaiSystem.Modules.Membership.Models
{
    public class ApiLoginRequest : ApiBaseRequest
    {
        [DisplayName("username")]
        [Required(ErrorMessage = BaseResource.ApiStatusCode.InvalidInput)]
        [MaxLength(50, ErrorMessage = BaseResource.ApiStatusCode.InvalidInputMaxMin)]
        [MinLength(5, ErrorMessage = BaseResource.ApiStatusCode.InvalidInputMaxMin)]
        public string Username { get; set; }

        [DisplayName("password")]
        [Required(ErrorMessage = BaseResource.ApiStatusCode.InvalidInput)]
        [MaxLength(50, ErrorMessage = BaseResource.ApiStatusCode.InvalidInputMaxMin)]
        [MinLength(5, ErrorMessage = BaseResource.ApiStatusCode.InvalidInputMaxMin)]
        public string Password { get; set; }

        [DisplayName("captcha")]
        public string Captcha { get; set; }
    }

}