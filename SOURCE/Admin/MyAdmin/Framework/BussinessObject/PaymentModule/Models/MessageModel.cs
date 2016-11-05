namespace BussinessObject.PaymentModule.Models
{
    public class MessageModel 
    {
        public long? RowNumber { get; set; }
        public int MessageId { get; set; }
        public int? SenderId { get; set; }
        public string SenderName { get; set; }
        public int? ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public int? MessageType { get; set; }
        public string MessageContent { get; set; }
        public string SendDate { get; set; }
        public string ReadDate { get; set; }
        public bool? IsRead { get; set; }
        public int? Status { get; set; }
        public int? ParentId { get; set; }
    }
}