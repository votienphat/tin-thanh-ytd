using System.Data.Entity.Core.Objects;
using DataAccess.EF;
using DataAccess.Interface;
using EntitiesObject.Entities.UserEntities;
namespace DataAccess.Dao.User
{
    public class MyConfigDao : DaoFactories<UserEntities, MyConfig>, IMyConfigDao
    {

        public ObjectResult<Out_MyConfig_GetCardConfig_Result> PaymentCard(string key)
        {
            using (Uow)
            {
                return Uow.Context.Out_MyConfig_GetCardConfig(key);
            }
        }
    }
}
