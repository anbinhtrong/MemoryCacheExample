using System;
using System.Threading;

namespace MemoryCacheExample
{
    public class LongTimeRunningObject
    {
        public string Name { get; set; }

        public int Seconds { get; set; }

        public void LongTimeRunningMethod()
        {
            Seconds = new Random().Next(1, 2);
            Thread.Sleep(new TimeSpan(0, 0, Seconds));
        }
    }
}