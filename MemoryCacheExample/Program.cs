using System;
using System.Diagnostics;
using System.Runtime.Caching;
using System.Threading;

namespace MemoryCacheExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Cache simple object");
            Console.WriteLine($"First time running took {CacheSimpleObject()} miliseconds to finish ");
            Console.WriteLine($"Second time running took {CacheSimpleObject()} miliseconds to finish ");
            Thread.Sleep(new TimeSpan(0, 0, new Random().Next(2, 4)));
            Console.WriteLine($"Third time running took {CacheSimpleObject()} miliseconds to finish ");
        }

        private static long CacheSimpleObject()
        {
            string keyName = "LongTimeRunningObject";
            LongTimeRunningObject longTimeRunningObject;
            var stopWatch = Stopwatch.StartNew();

            var cache = MemoryCache.Default;

            if (cache.Contains(keyName))
            {
                longTimeRunningObject = (LongTimeRunningObject)cache.Get(keyName);
            }
            else
            {
                longTimeRunningObject = new LongTimeRunningObject();
                longTimeRunningObject.Name = "CacheSimpleObject";
                longTimeRunningObject.LongTimeRunningMethod();
                var cacheItemPolicy = new CacheItemPolicy
                {
                    AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddSeconds(2))
                };
                cache.Add(keyName, longTimeRunningObject, cacheItemPolicy);
            }

            Console.WriteLine(
                $"Example: {longTimeRunningObject.Name} - Long time running methods took {longTimeRunningObject.Seconds} to finish");
            stopWatch.Stop();
            return stopWatch.ElapsedMilliseconds;
        }
    }
}
