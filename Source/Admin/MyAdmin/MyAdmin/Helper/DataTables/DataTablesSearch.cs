using System.ComponentModel;
using Newtonsoft.Json;

namespace MyAdmin.Helper.DataTables
{
    public class DataTablesSearch
    {
        [DisplayName("value")]
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [DisplayName("regex")]
        [JsonProperty(PropertyName = "regex")]
        public bool Regex { get; set; }
    }
}