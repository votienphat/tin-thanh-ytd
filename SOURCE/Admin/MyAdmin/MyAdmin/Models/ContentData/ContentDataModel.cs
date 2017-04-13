using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;

namespace MyAdmin.Models.ContentData
{
    public class SimpleDataModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int SyntaxId { get; set; }

    }
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Keyword { get; set; }
        public string imagepatch { get; set; }
        public HttpPostedFileBase File { get; set; }

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

}