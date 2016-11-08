using BussinessObject.UserModule.Enums;
using Newtonsoft.Json;

namespace BussinessObject.UserModule.Models
{
    public class OpenUserInfoModel
    {
        [JsonProperty("id")]
        public string OpenUserId { get; set; }

        [JsonProperty("name")]
        public string FullName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        public OpenProviderIdEnum OpenProviderId { get; set; }
    }
}