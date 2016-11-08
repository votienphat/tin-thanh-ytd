using BusinessObject.MembershipModule.Enums;

namespace BusinessObject.MembershipModule.Models.Request
{
    /// <summary>
    /// Model dung luu log thao tac cua admin
    /// </summary>
    public class LogActionAdminModel
    {
        /// <summary>
        /// Admin thuc hien thao tac
        /// </summary>
        public int AdminId { get; set; }

        /// <summary>
        /// Mo ta chi tiet (neu co)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Cac thong tin khac chiu tac dong truc tiep (neu co)
        /// </summary>
        public string ObjectId { get; set; }

        /// <summary>
        /// Gia tri config truoc khi thay doi (Co the la class)
        /// </summary>
        public object BeforeConfig { get; set; }

        /// <summary>
        /// Id thao tac cua admin
        /// </summary>
        public PageFunctionEnum ActionId { get; set; }
    }

    /// <summary>
    /// Model dung luu log thao tac cua admin (dung cho BO)
    /// </summary>
    public class LogActionAdminRequestModel
    {
        /// <summary>
        /// Ip thuc hien thao tac
        /// </summary>
        public string IpRequest { get; set; }

        /// <summary>
        /// Trinh duyet admin thuc hien thao tac
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Mo ta (neu co)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Admin thuc hien thao tac
        /// </summary>
        public int AdminId { get; set; }

        /// <summary>
        /// Cac object co lien quan, anh huong (neu co)
        /// </summary>
        public string ObjectId { get; set; }

        /// <summary>
        /// Object thong tin gia tri truoc khi thay doi
        /// </summary>
        public object Config { get; set; }

        /// <summary>
        /// Id thao tac cua admin
        /// </summary>
        public PageFunctionEnum ActionId { get; set; }
    }
}
