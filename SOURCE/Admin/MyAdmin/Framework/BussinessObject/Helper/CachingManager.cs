using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using log4net.Util;
using Logger;

namespace BussinessObject.Helper
{
    /// <summary>
    /// Quản lý cache
    /// Author: PhatVT
    /// Date Created: 12/16/2014
    /// </summary>
    public class CachingManager
    {
        public delegate object CallBack<T>(T obj);

        /// <summary>
        /// Lấy nội dung cache
        /// Author: PhatVT
        /// Date Created: 12/16/2014
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cachingName"></param>
        /// <param name="cachingType">Loại cache. Mặc định là lấy cache trên RAM</param>
        /// <returns></returns>
        public static T Get<T>(string cachingName, CachingType cachingType = CachingType.Default)
        {
            switch (cachingType)
            {
                default:
                    return GetDefaultCache<T>(cachingName);
            }
        }

        /// <summary>
        /// Lưu cache
        /// </summary>
        /// <param name="cachingName"></param>
        /// <param name="obj"></param>
        /// <param name="expireTime">Thời gian lưu cache, tính bằng giây. Nếu không muốn set expire thì giá trị là -1</param>
        /// <param name="cachingType">Loại cache. Mặc định là lấy cache trên RAM</param>
        public static bool Set(string cachingName, object obj, int expireTime = -1,
            CachingType cachingType = CachingType.Default)
        {
            switch (cachingType)
            {
                default:
                    return SetDefaultCache(cachingName, obj, expireTime);
            }
        }

        /// <summary>
        /// Cập nhật cache
        /// Author: PhatVT
        /// Date Created: 12/16/2014
        /// </summary>
        /// <typeparam name="T">Loại đối tượng cần xử lý</typeparam>
        /// <param name="cachingName">Tên cache</param>
        /// <param name="expireTime">Thời gian lưu cache, tính bằng giây. Mặc định là 30 phút</param>
        /// <param name="callBack">Hàm sẽ callback. Hàm này phải truyền đối tượng vào và trả về object</param>
        /// <param name="cachingType"></param>
        public static void Refresh<T>(string cachingName, CallBack<T> callBack, int expireTime = 1800,
            CachingType cachingType = CachingType.Default)
        {
            Set(cachingName, callBack, expireTime, cachingType);
        }

        #region Cache on memory

        /// <summary>
        /// Cache dùng RAM memory
        /// Author: PhatVT
        /// Date Created: 12/16/2014
        /// </summary>
        /// <param name="cachingName"></param>
        /// <param name="obj"></param>
        /// <param name="expireTime"></param>
        private static bool SetDefaultCache(string cachingName, object obj, int expireTime)
        {
            ObjectCache cache = MemoryCache.Default;

            if (cache == null)
            {
                return false;
            }

            // Nếu chưa có session thì tự động tạo mới
            var policy = new CacheItemPolicy();
            if (expireTime > 0)
            {
                policy.AbsoluteExpiration = DateTime.Now.AddSeconds(expireTime);
            }
            if (cache.Contains(cachingName))
            {
                cache.Remove(cachingName);
            }
            cache.Add(cachingName, obj, policy);
            return true;
        }

        private static T GetDefaultCache<T>(string cachingName)
        {
            ObjectCache cache = MemoryCache.Default;

            if (cache == null || !cache.Contains(cachingName))
            {
                return default(T);
            }

            var obj = cache[cachingName];
            return (T)obj;
        }

        #endregion
    }

    /// <summary>
    /// Định nghĩa loại cache
    /// Author: PhatVT
    /// Date Created: 12/16/2014
    /// </summary>
    public enum CachingType
    {
        /// <summary>
        /// Cache dùng memory của RAM
        /// </summary>
        Default = 0
    }
}
