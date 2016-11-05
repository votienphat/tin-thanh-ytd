using BussinessObject.Models;
using EntitiesObject.Entities.UserEntities;
using BussinessObject.Enums;

namespace BussinessObject.UserModule.Contract
{
    public interface IMyConfigBusiness
    {
        MyConfigModel GetMyConfigGetConfigByKey(int configId, string version, int chanelid = (int)ChanelIdEnum.IFish);

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