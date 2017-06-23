using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesObject.Entities.WebEntities;
using DataAccess.Repositories.Infrastructure.Contract;

namespace DataAccess.Contract.Web
{
    public interface ISloganRepository : IDaoRepository<Slogan>
    {

        List<Out_Slogan_Get_Result> SloganGet();

    }
}
