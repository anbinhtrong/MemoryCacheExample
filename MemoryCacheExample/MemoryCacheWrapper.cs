using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace MemoryCacheExample
{
    internal class MemoryCacheWrapper<T> where T : class
    {
        private readonly Func<string, T> _callBack;
        private readonly MemoryCache _memoryCache;

        public MemoryCacheWrapper(Func<string, T> factory, TimeSpan timeout, string name)
        {
            _memoryCache = MemoryCache.Default;
            _callBack = factory;
            TimeOut = timeout;
            Name = name;
        }

        public virtual T this[string key]
        {
            get
            {
                T value;

                if (_memoryCache.Contains(key))
                    value = (T)_memoryCache[key];
                else
                {
                    value = _callBack(key);
                    var policy = new CacheItemPolicy
                    {
                        AbsoluteExpiration = new DateTimeOffset(DateTime.Now.Add(TimeOut))
                    };
                    _memoryCache.Set(key, value, policy);
                }
                return value;
            }
        }

        public TimeSpan TimeOut { get; set; }

        public string Name { get; set; }

        public void Add(object objectToCache, string key)
        {
            _memoryCache.Add(key, objectToCache, DateTime.Now.AddMilliseconds(TimeOut.Ticks));
        }

        public T Get(string key)
        {
            return this[key];
        }

        public void Clear(string key)
        {
            _memoryCache.Remove(key);
        }

        public bool Exists(string key)
        {
            return _memoryCache.Get(key) != null;
        }

        public List<string> GetAll()
        {
            return _memoryCache.Select(keyValuePair => keyValuePair.Key).ToList();
        }
    }
}
