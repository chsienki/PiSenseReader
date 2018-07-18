using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiSenseReader
{
    public static class TaskExtensions
    {
        public static Task<T> SetTimeout<T>(this Task<T> task, int milliseconds)
        {
            if(Task.WaitAny(new[] { task }, milliseconds) >= 0)
            {
                return task;
            }
            else
            {
                return Task.FromResult(default(T));

            }
        }

    }
}
