using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace MyAdmin.Models.Article
{
    public class ArticleModel
    {
        //public string TextId { get; set; }
        public DateTime DateForm { get; set; }
        public DateTime DateTo { get; set; }
        //public int? TotalRow { get; set; }
        public string Keyword { get; set; }
        //public string SearchFor { get; set; }
        public int  Status { get; set; }
        public bool IsMyArticle { get; set; }
        public bool IsAllArticle { get; set; }
        public bool IsNotPublic { get; set; }
    }

    public enum ArticleDisplayTarget : int
    {
        WebP111 = 1,
        WebMobileP111 = 2,
        WebDanhBaiOnline = 3,
        WebMoblieAppota = 4
    }

    public class CreateArticleModel {
        public int ArticleID { get; set; }

        [Required(ErrorMessage = "Phải chọn phân loại.")]
        public int CategoryID { get; set; }
        public bool IsCommented { get; set; }

        public string ImageLink { get; set; }

        public string ImageHomeLink { get; set; }
        //public int DomainID { get; set; }
        public int CommentCount { get; set; }
        public int HotStatus { get; set; }
        public int ViewCount { get; set; }
        
        //public int HotOrNew { get; set; }
        public int GroupID { get; set; }
        //[Required(ErrorMessage = "Nhập title")]
        public string Title { get; set; }
        public string PublicDate { get; set; }
        [Required(ErrorMessage = "Chưa chọn nơi hiển thị.")]
        public List<int> DisplayTarget { get; set; }
        public string ShortDescription { get; set; }
        public string stringImageArticle { get; set; }
        public string HotExpire { get; set; }
        public string NewExpire { get; set; }
        public DateTime CreateDate { get; set; }
        public int DisplayTargetValue { get; set; }
        
        [AllowHtml]
        [ConfigurationProperty("maxJsonLength", DefaultValue = Int32.MaxValue)]
        public string Content { get; set; }

        [AllowHtml]
        public string ContentComment { get; set; }
        
        public bool IsHot { get; set; }
        public int Status { get; set; }
        public int HotOrNew { get; set; }
        
        public int CommentFor { get; set; }
        public string TextId { get; set; }
        public string NickName { get; set; }
        public string AssName { get; set; }
        public string ProfileLink { get; set; }
        public string AssLink { get; set; }
        public int PubUserID { get; set; }

    }

    public class ArticleCategoryModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public bool IsForAdmin { get; set; }
        public int Mark { get; set; }
        public string IconPath { get; set; }
        public string TextID { get; set; }
        public bool IsUsed { get; set; }
        public int DisplayTarget { get; set; }
        public int OrderByID { get; set; }
        public string Base64Image { get; set; }
    }

    public class ArticlePastEventModel
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public bool IsActive { get; set; }
        public string DateEvent { get; set; }
        public string CreateDate { get; set; }
        public string Image { get; set; }
        public int Category { get; set; }
        public string TextID { get; set; }
    }

    public class ArticleCommentModel
    {
        public string TextId { get; set; }
        public int PageIndex { get; set; }
        public int CommentId { get; set; }
        public Boolean IsSub { get; set; }
    }

    public class SubmitCommentRequest
    {
        public string TextId { get; set; }
        //[AllowHtml]
        public string Comment { get; set; }
        public int CommentFor { get; set; }
        public bool IsSub { get; set; }
        public int? CommentId { get; set; }
    }

    public class DeleteUserCommentRequest
    {
        public bool IsSub { get; set; }
        public int? CommentId { get; set; }
    }
}