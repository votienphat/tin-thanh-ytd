using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.PaymentModule.Models
{
    public class ExchargeCardHistoryModel
    {
        public int LogId { get; set; }
        public int RowNumber { get; set; }
        public int CardType { get; set; }
        public string CardName { get; set; }
        public decimal CardAmount { get; set; }
        public string DateCreated { get; set; }
        public string UserName { get; set; }
    }
}
