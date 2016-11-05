using System;

namespace BussinessObject.EventModule.Models
{
    public class ArticleResponseModel
    {
        public int ArticleID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string CreateDate { get; set; }
        public string Body { get; set; }
        public string PublicDate { get; set; }
        public DateTime DateSort { get; set; }

        public string DateSorts { get; set; }
        public int Status { get; set; }
        public string TextID { get; set; }
        public int CategoryID { get; set; }
    }
} ;