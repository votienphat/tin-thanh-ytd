using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using VanTaiSystem.Modules.Base.Resources;

namespace VanTaiSystem.Modules.Base.Models
{
    public class ApiBaseRequest
    {
        [JsonProperty(PropertyName = "sign")]
        public string Sign { get; set; }

        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }

        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "ip")]
        public string Ip { get; set; }

        [JsonProperty(PropertyName = "userAgent")]
        public string UserAgent { get; set; }

        [JsonProperty(PropertyName = "name")]
        [Required(ErrorMessage = BaseResource.ApiStatusCode.InvalidInput)]
        [MaxLength(50, ErrorMessage = BaseResource.ApiStatusCode.InvalidInput)]
        public string ApiName { get; set; }
    }

}