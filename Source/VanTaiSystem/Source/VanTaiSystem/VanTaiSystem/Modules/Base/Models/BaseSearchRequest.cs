using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace VanTaiSystem.Modules.Base.Models
{
    public class BaseSearchRequest : ApiBaseRequest
    {
        [JsonProperty(PropertyName = "startTime")]
        public DateTime? StarTime { get; set; }

        [JsonProperty(PropertyName = "endTime")]
        public DateTime? EndTime { get; set; }

        [JsonProperty(PropertyName = "keyword")]
        public string Keyword { get; set; }

        [JsonProperty(PropertyName = "start")]
        public int StartIndex { get; set; }

        [JsonProperty(PropertyName = "end")]
        public int EndIndex { get; set; }

        public BaseSearchRequest()
        {
            StartIndex = 0;
            EndIndex = 20;

            Keyword = "";
        }
    }
}