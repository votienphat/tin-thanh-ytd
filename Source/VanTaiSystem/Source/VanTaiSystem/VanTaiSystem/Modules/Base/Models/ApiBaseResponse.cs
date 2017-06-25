using System.ComponentModel;
using MyUtility.Extensions;
using Newtonsoft.Json;
using VanTaiSystem.Modules.Base.Enums;

namespace VanTaiSystem.Modules.Base.Models
{
    public class ApiBaseResponse
    {
        [JsonProperty(PropertyName = "code")]
        public ApiStatusCode Code { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Json string for data. Every method has different structure
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }

        public ApiBaseResponse()
        {
            Code = ApiStatusCode.SystemError;
            Message = ApiStatusCode.SystemError.Text();
            Data = null;
        }
    }

}