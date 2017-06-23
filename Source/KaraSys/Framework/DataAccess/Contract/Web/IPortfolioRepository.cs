using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesObject.Entities.WebEntities;
using DataAccess.Repositories.Infrastructure.Contract;

namespace DataAccess.Contract.Web
{
    public interface IPortfolioRepository : IDaoRepository<Portfolio>
    {
        int SaveData(int Id, string Name, string Avatar, string About, int CategoryId, string LinkWeb, string LinkProfile);

        List<Out_Portfolio_GetListData_Result> GetListData(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow);

        List<Out_Portfolio_GetList_Result> GetList();

        Out_Portfolio_GetById_Result PortfolioGetById(int Id);

        List<Out_Portfolio_GetListCategoryPortfolio_Result> GetListCategoryPortfolio();
    }
}
