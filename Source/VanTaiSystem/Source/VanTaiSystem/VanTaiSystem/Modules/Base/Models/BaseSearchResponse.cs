using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace VanTaiSystem.Modules.Base.Models
{
    public class BaseSearchResponse
    {
        [JsonProperty(PropertyName = "total")]
        public int TotalRow { get; set; }

        [JsonProperty(PropertyName = "items")]
        public object Items { get; set; }

        public BaseSearchResponse()
        {
            TotalRow = 0;
            Items = new List<object>();
        }
    }
}