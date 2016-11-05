using System.Data.Entity.Core.Objects;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess.Interface
{
    public interface IMyConfigDao : IBaseFactories<MyConfig>
    {
        ObjectResult<Out_MyConfig_GetCardConfig_Result> PaymentCard(string key);
    }
}
