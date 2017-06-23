using System.ComponentModel;
using Newtonsoft.Json;

namespace KaraSys.Helper.DataTables
{
    public class DataTablesColumn
    {
        [DisplayName("data")]
        [JsonProperty(PropertyName = "data")]
        public string Data { get; set; }

        [DisplayName("name")]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [DisplayName("searchable")]
        [JsonProperty(PropertyName = "searchable")]
        public bool Searchable { get; set; }

        [DisplayName("orderable")]
        [JsonProperty(PropertyName = "orderable")]
        public bool Orderable { get; set; }

        [DisplayName("search")]
        [JsonProperty(PropertyName = "search")]
        public DataTablesSearch Search { get; set; }
    }
}