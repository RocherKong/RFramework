using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFramework.Provider;

namespace RFramework.Cache.Interface
{
    public abstract class CacheProvider : BaseProvider
    {
        public abstract bool Add<T>(string key, T item);
        public abstract bool Add<T>(string key, T item, TimeSpan? expiry);

        public abstract bool Remove(string key);
        public abstract bool Update<T>(string key, T item);

        public abstract T Get<T>(string key);
        public abstract object Get(string key, Type type);
        public abstract bool IsExist(string key);
    }
}
