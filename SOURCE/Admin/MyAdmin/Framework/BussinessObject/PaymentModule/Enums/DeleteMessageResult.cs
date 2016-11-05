using System.ComponentModel;

namespace BussinessObject.PaymentModule.Enums
{
    public enum DeleteMessageResult
    {
        [Description("Danh sách tin nhắn cần delete không hợp lệ")]
        Failed = 2,

        [Description("Thành công")]
        Success = 1,

        [Description("Không tìm thấy tin nhắn để xóa")]
        NotFoundMessage = 3,
    }
}