using System.ComponentModel;
namespace BussinessObject.PaymentModule.Enums
{
    public enum UpdateGoldServerGameEnum
    {
        [Description("Thay the gold hien tai")]
        Replace = 1,

        [Description("Cap nhat cong hoac tru gold")]
        Update = 2
    }
}