using System.ComponentModel;

namespace BussinessObject.Enums
{
    public enum ResponseCode
    {
        [Description("thất bại")]
        Failed = 0,

        [Description("thành công")]
        Success = 1,

        [Description("Lỗi hệ thống")]
        SystemError = 9999
    }
}