using System.ComponentModel.DataAnnotations;

namespace Phystones.Models.Config
{
    public class ContactConfigModel
    {
        [Required(ErrorMessage = "Please Enter Phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Skype")]
        public string Skype { get; set; }

        [Required(ErrorMessage = "Please Enter Twitter")]
        public string Twitter { get; set; }

        [Required(ErrorMessage = "Please Enter HostName")]
        public string HostName { get; set; }

        [Required(ErrorMessage = "Please Enter FullHostName")]
        public string FullHostName { get; set; }

        [Required(ErrorMessage = "Please Enter FooterContent")]
        public string FooterContent { get; set; }

        public string KeyConfig { get; set; }

        public string GoogleAnalyticID { get; set; }

        public string FacebookPixelCode { get; set; }
    }
}