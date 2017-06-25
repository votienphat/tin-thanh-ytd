using System.ComponentModel;
using Newtonsoft.Json;

namespace VanTaiSystem.Helper.DataTables
{
    public class DataTablesOrder
    {
        [DisplayName("column")]
        [JsonProperty(PropertyName = "column")]
        public int Column { get; set; }

        [DisplayName("dir")]
        [JsonProperty(PropertyName = "dir")]
        public string Direction { get; set; }

        public DataTablesOrder()
        {
            Direction = "asc";
        }
    }
}