using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;

namespace KaraSys.Helper.DataTables
{
    public class DataTablesParam
    {
        [DisplayName("draw")]
        [JsonProperty(PropertyName = "draw")]
        public int Draw { get; set; }

        [DisplayName("columns")]
        [JsonProperty(PropertyName = "columns")]
        public List<DataTablesColumn> Columns { get; set; }

        [DisplayName("order")]
        [JsonProperty(PropertyName = "order")]
        public List<DataTablesOrder> Orders { get; set; }

        public DataTablesOrder Order
        {
            get { return Orders.FirstOrDefault(); }
        }

        /// <summary>
        /// Get first order column in orders list
        /// </summary>
        public int OrderColumn
        {
            get
            {
                return Order != null ? Order.Column : 0;
            }
        }

        /// <summary>
        /// Get first order direction in orders list
        /// </summary>
        public string OrderDirection
        {
            get
            {
                return Order != null ? Order.Direction : "asc";
            }
        }

        /// <summary>
        /// Get true/false on first order direction in orders list
        /// </summary>
        public bool IsAscOrdering
        {
            get
            {
                return OrderDirection.ToLower().Contains("asc");
            }
        }

        [DisplayName("start")]
        [JsonProperty(PropertyName = "start")]
        public int Start { get; set; }

        [DisplayName("length")]
        [JsonProperty(PropertyName = "length")]
        public int Length { get; set; }

        /// <summary>
        /// Get first search in search list
        /// </summary>
        [DisplayName("search")]
        [JsonProperty(PropertyName = "search")]
        public DataTablesSearch Search { get; set; }

        /// <summary>
        /// Get keyword of first search in search list
        /// </summary>
        public string Keyword
        {
            get
            {
                return Search != null ? Search.Value : string.Empty;
            }
        }

        public DataTablesParam()
        {
            Columns = new List<DataTablesColumn>();
            Orders = new List<DataTablesOrder>();
        }
    }
}