using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Logger;
using StackExchange.Redis;

namespace DataAccessRedis.Infrastructure
{
    public interface IRedisRepository
    {
        /// <summary>
        /// Tao du lieu kieu string
        /// zaads
        /// </summary>
        /// <param name="key">Key khong duoc rong, khong duoc null, 2 ky tu tro len</param>
        /// <param name="value">Noi dung muon luu tru</param>
        /// <param name="expire">Thoi gian het han (khong bat buoc truyen vao)</param>
        void Set(string key = "", string value = "", DateTime? expire = null);

        /// <summary>
        /// Lay du lieu kieu string
        /// </summary>
        /// <param name="key">Key khong duoc rong, khong duoc null, 2 ky tu tro len</param>
        /// <returns></returns>
        string Get(string key);

        /// <summary>
        /// Tao du lieu kieu hash
        /// </summary>
        /// <param name="key">Key khong duoc rong, khong duoc null, 2 ky tu tro len</param>
        /// <param name="dictionary">Du lieu muon luu tru</param>
        void HashSet(string key, Dictionary<string, string> dictionary);

        /// <summary>
        /// Lay du lieu kieu hash
        /// </summary>
        /// <param name="key">Key khong duoc rong, khong duoc null, 2 ky tu tro len</param>
        /// <returns></returns>
        Dictionary<RedisValue, RedisValue> HashGetAll(string key);

        /// <summary>
        /// Xoa mot key
        /// </summary>
        /// <param name="key">Key khong duoc rong, khong duoc null, 2 ky tu tro len</param>
        /// <returns></returns>
        void Delete(string key);

        /// <summary>
        /// Xoa danh sach key
        /// </summary>
        /// <param name="listKey">Danh sach key can xoa</param>
        void Delete(List<string> listKey);

        /// <summary>
        /// Tim tat ca cac key theo pattern, VD: guo:*
        /// </summary>
        /// <param name="pattern">pattern khong duoc rong, khong duoc null, 2 ky tu tro len</param>
        /// <returns></returns>
        List<RedisKey> Scan(string pattern);

        /// <summary>
        /// Ngat ket noi redis
        /// </summary>
        void Dispose();
    }

    public class RedisRepository : IRedisRepository
    {
        #region Varriables
        /// <summary>
        /// Host server Redis
        /// </summary>
        private static string RedisHost
        {
            get
            {
                string value = ConfigurationManager.AppSettings["RedisHost"];
                if (String.IsNullOrEmpty(value))
                    return "";
                return value;
            }
        }

        /// <summary>
        /// Password server Redis
        /// </summary>
        private static string RedisPassword
        {
            get
            {
                string value = ConfigurationManager.AppSettings["RedisPassword"];
                if (String.IsNullOrEmpty(value))
                    return "";
                return value;
            }
        }

        /// <summary>
        /// Thoi gian timeout (ms) cho cac hoat dong ket noi
        /// </summary>
        private static int ConnectTimeout
        {
            get
            {
                string value = ConfigurationManager.AppSettings["ConnectTimeout"];
                if (String.IsNullOrEmpty(value))
                    return 100000;

                int i;
                if (int.TryParse(value, out i))
                    return Convert.ToInt32(value);

                return 100000;
            }
        }

        /// <summary>
        /// Time (ms) to allow for synchronous operations
        /// </summary>
        private static int SyncTimeout
        {
            get
            {
                string value = ConfigurationManager.AppSettings["SyncTimeout"];
                if (String.IsNullOrEmpty(value))
                    return 100000;

                int i;
                if (int.TryParse(value, out i))
                    return Convert.ToInt32(value);

                return 100000;
            }
        }

        /// <summary>
        /// Chi so co so du lieu mac dinh (mac dinh: 0)
        /// </summary>
        private static int DefaultDatabase
        {
            get
            {
                string value = ConfigurationManager.AppSettings["DefaultDatabase"];
                if (String.IsNullOrEmpty(value))
                    return 0;

                int i;
                if (int.TryParse(value, out i))
                    return Convert.ToInt32(value);

                return 0;
            }
        }

        private static bool AbortOnConnectFail
        {
            get
            {
                string value = ConfigurationManager.AppSettings["AbortOnConnectFail"];
                if (String.IsNullOrEmpty(value))
                    return false;

                bool i;
                if (bool.TryParse(value, out i))
                    return Convert.ToBoolean(value);

                return false;
            }
        }
        #endregion

        private readonly Lazy<ConfigurationOptions> ConfigOptions = new Lazy<ConfigurationOptions>(() =>
        {
            var configOptions = new ConfigurationOptions();
            configOptions.EndPoints.Add(RedisHost);
            configOptions.Password = RedisPassword;
            configOptions.ConnectTimeout = ConnectTimeout;
            configOptions.SyncTimeout = SyncTimeout;
            configOptions.DefaultDatabase = DefaultDatabase;
            configOptions.AbortOnConnectFail = AbortOnConnectFail;            
            return configOptions;
        });

        private Lazy<ConnectionMultiplexer> _lazyConnection;// = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configOptions.Value));
        //private readonly IDatabase _db;

        public ConnectionMultiplexer Connection
        {
            get
            {

                if (_lazyConnection == null)
                {
                    _lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(ConfigOptions.Value));
                }
                else
                {
                    if (!_lazyConnection.Value.IsConnected)
                    {
                        Dispose();
                        _lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(ConfigOptions.Value));
                    }
                }
                return _lazyConnection.Value;
            }
        }

        /// <summary>
        /// Ngat ket noi
        /// </summary>
        public void Dispose()
        {
            if (_lazyConnection != null && _lazyConnection.Value != null)
                _lazyConnection.Value.Dispose();
        }

        //public RedisRepository()
        //{
        //    //_db = Connection.GetDatabase();
        //}

        /// <summary>
        /// Tao du lieu kieu string
        /// </summary>
        /// <param name="key">Key khong duoc rong, khong duoc null, 2 ky tu tro len</param>
        /// <param name="value">Noi dung muon luu tru</param>
        /// <param name="expire">Thoi gian het han (khong bat buoc truyen vao)</param>
        public void Set(string key, string value, DateTime? expire)
        {
            if (!string.IsNullOrEmpty(key) && key.Length > 1)
            {
                IDatabase db = Connection.GetDatabase();
                TimeSpan ts;
                if (expire != null)
                {
                    ts = expire.Value - DateTime.Now;
                    db.StringSetAsync(key, value, ts);
                }
                else
                {
                    db.StringSetAsync(key, value);

                }
                Dispose();
            }
        }

        /// <summary>
        /// Lay du lieu kieu string
        /// </summary>
        /// <param name="key">Key khong duoc rong, khong duoc null, 2 ky tu tro len</param>
        /// <returns></returns>        
        public string Get(string key)
        {

            if (!string.IsNullOrEmpty(key) && key.Length > 1)
            {
                IDatabase db = Connection.GetDatabase();
                var result = db.StringGet(key).ToString();
                Dispose();
                return result;
            }

            return string.Empty;
        }

        /// <summary>
        /// Tao du lieu kieu hash
        /// </summary>
        /// <param name="key">Key khong duoc rong, khong duoc null, 2 ky tu tro len</param>
        /// <param name="dictionary">Du lieu muon luu tru</param>
        public void HashSet(string key, Dictionary<string, string> dictionary)
        {
            if (!string.IsNullOrEmpty(key) && key.Length > 1)
            {
                IDatabase db = Connection.GetDatabase();
                var fields = dictionary.Select(pair => new HashEntry(pair.Key, pair.Value)).ToArray();
                db.HashSetAsync(key, fields);
                Dispose();
            }
        }

        /// <summary>
        /// Lay du lieu kieu hash
        /// </summary>
        /// <param name="key">Key khong duoc rong, khong duoc null, 2 ky tu tro len</param>
        /// <returns></returns>
        public Dictionary<RedisValue, RedisValue> HashGetAll(string key)
        {
            if (!string.IsNullOrEmpty(key) && key.Length > 1)
            {
                IDatabase db = Connection.GetDatabase();
                var result =  db.HashGetAll(key).ToDictionary();
                Dispose();
                return result;
            }

            return null;
        }

        /// <summary>
        /// Xoa mot key
        /// </summary>
        /// <param name="key">Key khong duoc rong, khong duoc null, 2 ky tu tro len</param>
        /// <returns></returns>
        public void Delete(string key)
        {
            if (!string.IsNullOrEmpty(key) && key.Length > 1)
                Delete(key);
        }

        /// <summary>
        /// Xoa danh sach key
        /// </summary>
        /// <param name="listKey">Danh sach key can xoa</param>
        public void Delete(List<string> listKey)
        {
            if (listKey == null || !listKey.Any()) return;
            var lst = listKey.Where(item => !string.IsNullOrEmpty(item) && item.Length > 1).Select(item => (RedisKey)item).ToList();

            if (lst.Any())
            {
                IDatabase db = Connection.GetDatabase();
                db.KeyDelete(lst.ToArray());
                Dispose();
            }
        }

        /// <summary>
        /// Tim tat ca cac key theo pattern, VD: guo:*
        /// </summary>
        /// <param name="pattern">pattern khong duoc rong, khong duoc null, 2 ky tu tro len</param>
        /// <returns></returns>
        public List<RedisKey> Scan(string pattern)
        {
            var result = new List<RedisKey>();
            if (!string.IsNullOrEmpty(pattern) && pattern.Length > 1)
            {
                var server = Connection.GetServer(Connection.GetEndPoints()[0]);
                result = server.Keys(pattern: pattern, database: DefaultDatabase).ToList();
                Dispose();
            }

            return result;
        }

        // ** Note
        // server.FlushDatabase(1); - dong nay xoa du lieu cua db 1
        // configOptions.AllowAdmin = true;
    }
}
