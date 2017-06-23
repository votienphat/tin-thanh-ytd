using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesObject.Entities.WebEntities;
using DataAccess.Repositories.Infrastructure.Contract;

namespace DataAccess.Contract.Web
{
    public interface IWorkRepository : IDaoRepository<Work>
    {
        int SaveDataWork(int Id, string Title, string Image, string ContentBody, int CategoryId);

        List<Out_Work_GetListData_Result> ListDataWork(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow);

        List<Out_CategoryWork_GetList_Result> CategoryWorkGetList();

        Out_Work_GetById_Result WorkGetById(int Id);

        List<Out_Work_GetByCategoryId_Result> GetWorkByCategoryId(int CategoryId);

        Out_Work_GetByTextId_Result WorkGetByTextId(string textId);

    }
}
