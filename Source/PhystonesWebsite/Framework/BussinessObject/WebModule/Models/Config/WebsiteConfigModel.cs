using System.ComponentModel.DataAnnotations;

namespace BusinessObject.WebModule.Models.Config
{
    public class WebsiteConfigModel
    {
        [Required(ErrorMessage = "Please Enter HostName")]
        public string HostName { get; set; }

        [Required(ErrorMessage = "Please Enter FullHostName")]
        public string FullHostName { get; set; }

        [Required(ErrorMessage = "Tên website không được để trống")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Slogan website không được để trống")]
        public string Slogan { get; set; }

        [Required(ErrorMessage = "Tiêu đề website không được để trống")]
        public string Title { get; set; }
    }
}