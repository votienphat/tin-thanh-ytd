using BussinessObject.UserModule.Contract;
using DataAccess.Contract.MainModule;
using EntitiesObject.Entities.UserEntities;

namespace BussinessObject.UserModule
{
    public class MyConfigBusiness : IMyConfigBusiness
    {
        #region Variables

        private readonly IMyConfigRepository _myConfigRepo;

        #endregion

        public MyConfigBusiness(IMyConfigRepository myConfigRepo)
        {
            _myConfigRepo = myConfigRepo;
        }

        /// <summary>
        /// Lấy chi tiết my config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <history>
        /// 2/6/2016 Create by TaiNM
        /// </history>
        public Out_MyConfig_GetCardConfig_Result Get_ByKey(string key)
        {
            return _myConfigRepo.Get(key);
        }

    }
}
