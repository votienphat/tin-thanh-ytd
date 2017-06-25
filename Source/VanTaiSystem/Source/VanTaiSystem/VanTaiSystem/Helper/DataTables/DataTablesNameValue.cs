using System.ComponentModel;
using Newtonsoft.Json;

namespace VanTaiSystem.Helper.DataTables
{
    public class DataTablesNameValue
    {
        [DisplayName("name")]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [DisplayName("value")]
        [JsonProperty(PropertyName = "value")]
        public object Value { get; set; }
    }
}