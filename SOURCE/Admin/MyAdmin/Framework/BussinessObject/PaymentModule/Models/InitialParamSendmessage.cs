namespace BussinessObject.PaymentModule.Models
{
    public class InitialParamSendmessage
    {
        
        public int MessageType { get; set; }

        public int Status { get; set; }

        public bool IsSysTem { get; set; }

        public string Content { get; set; }

        public InitialParamSendmessage()
        {
            //1: Tin nhắn thông thường, 2: Tin nhắn đổi thẻ, 3: Tin nhắn đổi thưởng
            MessageType = 3;
            //0: Chưa gửi, 1: Hiển thị, 2: User xóa, 3: Admin xóa
            Status = 1;
            //true: do hệ thống gởi
            IsSysTem = true;


        }
       
        //Id message mà nó comment 
    }
}