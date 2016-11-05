using System;
using System.Collections.Generic;
using System.Linq;
using BussinessObject.EventModule.Helper.Abstract;
using BussinessObject.EventModule.Models;
using DataAccessRedis.Constants;
using DataAccessRedis.Infrastructure;
using DataAccessRedis.Module.Contract;
using Newtonsoft.Json;

namespace BussinessObject.EventModule.Helper
{
    public class LyckyEveryDayHelper : ILyckyEveryDayHelper
    {
        private readonly IRedisRepository _redisRepository;
        private readonly IEventRedis _eventRedis;

        public LyckyEveryDayHelper(IRedisRepository redisRepository, IEventRedis eventRedis)
        {
            _redisRepository = redisRepository;
            _eventRedis = eventRedis;
        }

        public string CreateKey(DateTime dateReport, int periodOfTime, int pageSize, int pageIndex)
        {
            //Nam:Thang:Ngay:periodOfTime:PageSize:PageIndex
            return string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}", RedisKeyConstants.LuckyEveryday, dateReport.Year, dateReport.Month, dateReport.Day, periodOfTime, pageSize,
                pageIndex);
        }

        public string[] GetKey(string key)
        {
            return _eventRedis.GetStringArrayKey(key);
        }

       

        public List<LuckyEverydayMiniModel> GetValue(string key)
        {
            var value = _redisRepository.Get(key);

            var response = JsonConvert.DeserializeObject<List<LuckyEverydayMiniModel>>(value);

            return response ?? Enumerable.Empty<LuckyEverydayMiniModel>().ToList();
        }

        public bool IsSuccessSetValue(string key, List<LuckyEverydayModel> luckyEverydayModels)
        {
            try
            {
                var value = luckyEverydayModels.Select(x => new LuckyEverydayMiniModel
                {
                    RowNumber = x.RowNumber,
                    DisplayName = x.DisplayName,
                    PeriodOfTimeValue = x.PeriodOfTimeValue,
                    Present = x.Present,
                    Value = x.Value,
                    TotalRow = x.TotalRow
                }).ToList();

                var valueToRedis = JsonConvert.SerializeObject(value);
                _redisRepository.Set(key, valueToRedis);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteKey(string key)
        {
            _redisRepository.Delete(key);
        }
    }
}