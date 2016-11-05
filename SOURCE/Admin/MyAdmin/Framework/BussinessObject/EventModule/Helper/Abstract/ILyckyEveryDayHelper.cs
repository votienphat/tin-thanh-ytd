using System;
using System.Collections.Generic;
using BussinessObject.EventModule.Models;

namespace BussinessObject.EventModule.Helper.Abstract
{
    public interface ILyckyEveryDayHelper
    {
        string CreateKey(DateTime dateReport, int periodOfTime, int pageSize, int pageIndex);
        string[] GetKey(string key);
        List<LuckyEverydayMiniModel> GetValue(string key);
        bool IsSuccessSetValue(string key, List<LuckyEverydayModel> luckyEverydayModels);
        void DeleteKey(string key);
    }
}