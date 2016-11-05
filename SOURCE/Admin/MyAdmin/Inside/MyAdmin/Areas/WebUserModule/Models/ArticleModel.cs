using System;

namespace MyAdmin.Areas.WebUserModule.Models
{
    public class ArticleModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategorieId { get; set; }
        public string NewsContent { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string ImageArticle { get; set; }
        public string stringImageArticle { get; set; }
        public int Status { get; set; }
        public bool IsComment { get; set; }
        public int CountView { get; set; }
        public string ShortDescription { get; set; }
        public string SeoLink { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }
}
