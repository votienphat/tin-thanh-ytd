using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Enums
{
    public enum NotificationTypeEnum
    {
        [Description("Đổi thẻ")]
        ExchangeCard = 1,
        [Description("Hủy thẻ")]
        RefuseCard = 2,
    }
}
