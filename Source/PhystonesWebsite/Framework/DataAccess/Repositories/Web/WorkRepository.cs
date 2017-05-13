using DataAccess.Repositories.Infrastructure;
using DataAccess.EF;
using DataAccess.Contract.Web;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using EntitiesObject.Entities.WebEntities;

namespace DataAccess.Repositories.Membership
{
    public class WorkRepository : DaoRepository<WebEntities, Work>, IWorkRepository
    {
        public int SaveDataWork(int Id,string Title,string Image,string ContentBody,int CategoryId)
        {
            return Uow.Context.Out_Work_Save(Id, Title, Image, ContentBody, CategoryId);
        }
        public List<Out_Work_GetListData_Result> ListDataWork(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow)
        {
            totalRow = 0;
            var outTotalRow = new ObjectParameter("TotalRow", totalRow);
            var result = Uow.Context.Out_Work_GetListData(rowStart, rowEnd, orderBy, isDescending, outTotalRow).ToList();
            totalRow = outTotalRow.Value == null ? 0 : int.Parse(outTotalRow.Value.ToString());
            return result;
        }
        public List<Out_CategoryWork_GetList_Result> CategoryWorkGetList()
        {
            var result = Uow.Context.Out_CategoryWork_GetList().ToList();
            return result;
        }
        public Out_Work_GetById_Result WorkGetById(int Id)
        {
            var result = Uow.Context.Out_Work_GetById(Id).FirstOrDefault();
            return result;
        }
        public List<Out_Work_GetByCategoryId_Result> GetWorkByCategoryId(int CategoryId) {
            var result = Uow.Context.Out_Work_GetByCategoryId(CategoryId).ToList();
            return result;
        }
        public Out_Work_GetByTextId_Result WorkGetByTextId(string textId)
        {
            var result = Uow.Context.Out_Work_GetByTextId(textId).FirstOrDefault();
            return result;
        }
    }
}