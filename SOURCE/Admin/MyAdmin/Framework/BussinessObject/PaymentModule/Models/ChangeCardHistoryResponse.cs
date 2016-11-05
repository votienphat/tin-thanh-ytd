using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesObject.Entities.LogManagementEntities;

namespace BussinessObject.PaymentModule.Models
{
    public class ChangeCardHistoryResponse
    {
        public int TotalRows { get; set; }
        public List<ChangeCardHistoryModel> ListData { get; set; }
    }
}
