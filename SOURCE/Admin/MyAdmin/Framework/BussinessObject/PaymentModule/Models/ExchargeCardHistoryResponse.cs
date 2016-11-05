using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.PaymentModule.Models
{
    public class ExchargeCardHistoryResponse
    {
        public int TotalRows { get; set; }
        public List<ExchargeCardHistoryModel> ListData { get; set; }
    }
}
