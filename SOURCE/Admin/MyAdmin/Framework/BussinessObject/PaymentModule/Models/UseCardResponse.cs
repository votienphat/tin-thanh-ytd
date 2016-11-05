namespace EntitiesObject.Entities.UserEntities
{
    public class UseCardResponse
    {
        /// <summary>
        /// Kết quả.
        /// True: Thành công
        /// False: Thất bại
        /// </summary>
        public bool Result { get; set; }

        public int PartnerId { get; set; }
        public string Message { get; set; }
        public string PartnerTransId { get; set; }
        public int Amount { get; set; }
    }
}
