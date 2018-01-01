using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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