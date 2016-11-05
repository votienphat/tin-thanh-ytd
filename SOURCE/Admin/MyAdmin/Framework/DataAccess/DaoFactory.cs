using DataAccess.Dao.User;
using DataAccess.Interface;
using EntitiesObject.Entities.UserEntities;

namespace DataAccess
{
    public class DaoFactory
    {
        public static IMyConfigDao MyConfig { get { return new MyConfigDao(); } }
        public static IPaymentDao Payment { get { return new PaymentDao();} }
    }
}
