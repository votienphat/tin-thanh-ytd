using DataAccess.Repositories.Infrastructure;
using EntitiesObject.Entities.WebEntities;
using DataAccess.EF;
using DataAccess.Contract.Web;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace DataAccess.Repositories.Membership
{
    public class PortfolioRepository : DaoRepository<WebEntities, Portfolio>, IPortfolioRepository
    {
        public int SaveData(int Id,string Name,string Avatar,string About,int CategoryId,string LinkWeb,string LinkProfile)
        {
            return Uow.Context.Out_Portfolio_Save(Id, Name, Avatar, About, CategoryId, LinkWeb, LinkProfile);
        }

        public List<Out_Portfolio_GetListData_Result> GetListData(int rowStart, int rowEnd, int orderBy, bool isDescending, out int totalRow)
        {
            totalRow = 0;
            var outTotalRow = new ObjectParameter("TotalRow", totalRow);
            var result = Uow.Context.Out_Portfolio_GetListData(rowStart, rowEnd, orderBy, isDescending, outTotalRow).ToList();
            totalRow = outTotalRow.Value == null ? 0 : int.Parse(outTotalRow.Value.ToString());
            return result;
        }
        public List<Out_Portfolio_GetList_Result> GetList()
        {
            var result = Uow.Context.Out_Portfolio_GetList().ToList();
            return result;
        }

        public Out_Portfolio_GetById_Result PortfolioGetById(int Id)
        {
            var result = Uow.Context.Out_Portfolio_GetById(Id).FirstOrDefault();
            return result;
        }

        public List<Out_Portfolio_GetListCategoryPortfolio_Result> GetListCategoryPortfolio()
        {
            var result = Uow.Context.Out_Portfolio_GetListCategoryPortfolio().ToList();
            return result;
        }
    }
}