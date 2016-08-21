using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFramework.Cache.Interface;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace RFramework.Cache.Impl
{
    public class RedisCacheProvider : CacheProvider
    {
        private ConnectionMultiplexer connection;

        private IDatabase cacheDB { get { return connection.GetDatabase(); } }

        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);
            String ConStr = config["ConnectionString"];
            if (String.IsNullOrEmpty(ConStr))
            {
                throw new Exception("the node of [ConnectionString] Can't be Empty.");
            }
            connection = ConnectionMultiplexer.Connect(ConStr);
        }
        public override bool Add<T>(string key, T item)
        {
            return Add<T>(key, item, null);
        }

        public override bool Add<T>(string key, T item, TimeSpan? expiry)
        {
            String data = JsonConvert.SerializeObject(item);
            return cacheDB.StringSet(key, data, expiry);
        }

        public override object Get(string key, Type type)
        {
            String result = cacheDB.StringGet(key);
            return JsonConvert.DeserializeObject(result, type);
        }

        public override T Get<T>(string key)
        {
            String result = cacheDB.StringGet(key);
            if (String.IsNullOrEmpty(result))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(result);
        }

        public override bool IsExist(string key)
        {
            return cacheDB.KeyExists(key);
        }

        public override bool Remove(string key)
        {
            return cacheDB.KeyDelete(key);
        }

        public override bool Update<T>(string key, T item)
        {
            String data = JsonConvert.SerializeObject(item);
            return cacheDB.StringSet(key, data);
        }
    }
}
