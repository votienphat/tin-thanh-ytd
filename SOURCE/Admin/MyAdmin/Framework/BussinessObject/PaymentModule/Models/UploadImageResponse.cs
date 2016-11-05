namespace BussinessObject.PaymentModule.Models
{
    public class UploadImageResponse
    {
        /// <summary>
        /// Kết quả.
        /// True: Thành công
        /// False: Thất bại
        /// </summary>
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
        public string ImagePath { get; set; }

    }
}
