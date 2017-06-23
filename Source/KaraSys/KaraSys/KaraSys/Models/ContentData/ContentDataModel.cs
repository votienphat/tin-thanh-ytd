using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;

namespace KaraSys.Models.ContentData
{
    public class SimpleDataModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int SyntaxId { get; set; }

    }
    public class ArticleModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public HttpPostedFileBase Image { get; set; }
        [AllowHtml]
        public string ContentBody { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImgeString { get; set; }

    }
    public class SyntaxModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int SyntaxId { get; set; }

    }
    public class SyntaxList
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class SendContact
    {
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please Enter Correct Phone.")]
        [Required(ErrorMessage = "Please Enter Phone")]
        [StringLength(11, ErrorMessage = "The Mobile must contains 10 - 11 characters", MinimumLength = 10)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please Enter Correct Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Messenger")]
        public string Messenger { get; set; }
    }
    public class PortfolioView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        [AllowHtml]
        public string About { get; set; }
        public string Specialize { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string LinkWeb { get; set; }
        public string LinkProfile { get; set; }
        public int NextPor { get; set; }
        public int PrePor { get; set; }
    }
    public class PortfolioModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        [AllowHtml]
        public string About { get; set; }
        public string Specialize { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string LinkWeb { get; set; }
        public string LinkProfile { get; set; }
        public string ImgeString { get; set; }
    }
}