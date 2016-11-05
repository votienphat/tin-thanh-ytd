using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Enums
{
    /// <summary>
    /// Tao enum WalletType
    /// Duynd - 03/06/2016
    /// </summary>
    public enum WalletTypeEnum
    {
        [Description("Ví")]
        Vi = 1,

        [Description("Rương")]
        Ruong = 2
    }
}
