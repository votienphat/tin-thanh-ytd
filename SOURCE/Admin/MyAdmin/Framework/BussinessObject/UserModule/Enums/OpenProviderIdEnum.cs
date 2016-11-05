using System.ComponentModel;

namespace BussinessObject.UserModule.Enums
{
    public enum OpenProviderIdEnum
    {
        [Description("Login bằng username, password")]
        Normal = 0,

        Facebook = 1,
        Google = 2,
        Yahoo = 3
    }
}
