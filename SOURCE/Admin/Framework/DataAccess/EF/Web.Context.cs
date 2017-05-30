﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    using EntitiesObject.Entities.WebEntities;

    public partial class WebEntities : DbContext
    {
        public WebEntities()
            : base("name=WebEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Sample> Samples { get; set; }
        public virtual DbSet<Syntax> Syntaxes { get; set; }
    
        public virtual int Out_Category_Save(Nullable<int> id, string name, string keyword, string imagePatch)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var keywordParameter = keyword != null ?
                new ObjectParameter("Keyword", keyword) :
                new ObjectParameter("Keyword", typeof(string));
    
            var imagePatchParameter = imagePatch != null ?
                new ObjectParameter("ImagePatch", imagePatch) :
                new ObjectParameter("ImagePatch", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Out_Category_Save", idParameter, nameParameter, keywordParameter, imagePatchParameter);
        }
    
        public virtual int Out_Sample_Save(Nullable<int> id, string contentSample, Nullable<int> syntaxId)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            var contentSampleParameter = contentSample != null ?
                new ObjectParameter("ContentSample", contentSample) :
                new ObjectParameter("ContentSample", typeof(string));
    
            var syntaxIdParameter = syntaxId.HasValue ?
                new ObjectParameter("SyntaxId", syntaxId) :
                new ObjectParameter("SyntaxId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Out_Sample_Save", idParameter, contentSampleParameter, syntaxIdParameter);
        }
    
        public virtual int Out_Syntax_Save(Nullable<int> id, string name, string contentSyntax, string keyWord, Nullable<int> categoryId, string description)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var contentSyntaxParameter = contentSyntax != null ?
                new ObjectParameter("ContentSyntax", contentSyntax) :
                new ObjectParameter("ContentSyntax", typeof(string));
    
            var keyWordParameter = keyWord != null ?
                new ObjectParameter("KeyWord", keyWord) :
                new ObjectParameter("KeyWord", typeof(string));
    
            var categoryIdParameter = categoryId.HasValue ?
                new ObjectParameter("CategoryId", categoryId) :
                new ObjectParameter("CategoryId", typeof(int));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Out_Syntax_Save", idParameter, nameParameter, contentSyntaxParameter, keyWordParameter, categoryIdParameter, descriptionParameter);
        }
    
        public virtual ObjectResult<Out_Syntax_GetAll_Result> Out_Syntax_GetAll(string keyword)
        {
            var keywordParameter = keyword != null ?
                new ObjectParameter("Keyword", keyword) :
                new ObjectParameter("Keyword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Out_Syntax_GetAll_Result>("Out_Syntax_GetAll", keywordParameter);
        }
    
        public virtual ObjectResult<Out_Category_GetListData_Result> Out_Category_GetListData(Nullable<int> rowStart, Nullable<int> rowEnd, Nullable<int> orderBy, Nullable<bool> isDescending, ObjectParameter totalRow)
        {
            var rowStartParameter = rowStart.HasValue ?
                new ObjectParameter("RowStart", rowStart) :
                new ObjectParameter("RowStart", typeof(int));
    
            var rowEndParameter = rowEnd.HasValue ?
                new ObjectParameter("RowEnd", rowEnd) :
                new ObjectParameter("RowEnd", typeof(int));
    
            var orderByParameter = orderBy.HasValue ?
                new ObjectParameter("OrderBy", orderBy) :
                new ObjectParameter("OrderBy", typeof(int));
    
            var isDescendingParameter = isDescending.HasValue ?
                new ObjectParameter("IsDescending", isDescending) :
                new ObjectParameter("IsDescending", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Out_Category_GetListData_Result>("Out_Category_GetListData", rowStartParameter, rowEndParameter, orderByParameter, isDescendingParameter, totalRow);
        }
    
        public virtual ObjectResult<Out_Samples_GetListData_Result> Out_Samples_GetListData(Nullable<int> rowStart, Nullable<int> rowEnd, Nullable<int> orderBy, Nullable<bool> isDescending, ObjectParameter totalRow)
        {
            var rowStartParameter = rowStart.HasValue ?
                new ObjectParameter("RowStart", rowStart) :
                new ObjectParameter("RowStart", typeof(int));
    
            var rowEndParameter = rowEnd.HasValue ?
                new ObjectParameter("RowEnd", rowEnd) :
                new ObjectParameter("RowEnd", typeof(int));
    
            var orderByParameter = orderBy.HasValue ?
                new ObjectParameter("OrderBy", orderBy) :
                new ObjectParameter("OrderBy", typeof(int));
    
            var isDescendingParameter = isDescending.HasValue ?
                new ObjectParameter("IsDescending", isDescending) :
                new ObjectParameter("IsDescending", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Out_Samples_GetListData_Result>("Out_Samples_GetListData", rowStartParameter, rowEndParameter, orderByParameter, isDescendingParameter, totalRow);
        }
    
        public virtual ObjectResult<Out_Syntax_GetListData_Result> Out_Syntax_GetListData(Nullable<int> rowStart, Nullable<int> rowEnd, Nullable<int> orderBy, Nullable<bool> isDescending, ObjectParameter totalRow)
        {
            var rowStartParameter = rowStart.HasValue ?
                new ObjectParameter("RowStart", rowStart) :
                new ObjectParameter("RowStart", typeof(int));
    
            var rowEndParameter = rowEnd.HasValue ?
                new ObjectParameter("RowEnd", rowEnd) :
                new ObjectParameter("RowEnd", typeof(int));
    
            var orderByParameter = orderBy.HasValue ?
                new ObjectParameter("OrderBy", orderBy) :
                new ObjectParameter("OrderBy", typeof(int));
    
            var isDescendingParameter = isDescending.HasValue ?
                new ObjectParameter("IsDescending", isDescending) :
                new ObjectParameter("IsDescending", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Out_Syntax_GetListData_Result>("Out_Syntax_GetListData", rowStartParameter, rowEndParameter, orderByParameter, isDescendingParameter, totalRow);
        }
    }
}