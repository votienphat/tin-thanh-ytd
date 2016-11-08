using EntitiesObject.Entities.UserEntities;

namespace BussinessObject.UserModule.Contract
{
    public interface IMyConfigBusiness
    {
        /// <summary>
        /// Lấy chi tiết my config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <history>
        /// 2/6/2016 Create by TaiNM
        /// </history>
        Out_MyConfig_GetCardConfig_Result Get_ByKey(string key);
    }
}