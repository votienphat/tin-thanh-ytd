using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.PaymentModule.Models
{
    public class ChangeCardHistoryModel
    {
        public int RowNumber { get; set; }
        public string PaymentType { get; set; }

        public int PaymentTypeId { get; set; }
        public string CardName { get; set; }
        public decimal GoldValue { get; set; }
        public string DateCreated { get; set; }
        public string Serial { get; set; }
    }
}
