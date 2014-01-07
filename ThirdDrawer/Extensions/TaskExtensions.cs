using System;
using System.Threading.Tasks;

namespace ThirdDrawer.Extensions
{
    public static class TaskExtensions
    {
        public static TResult WaitForResult<TResult>(this Task<TResult> task)
        {
            task.Wait();
            return task.Result;
        }

        public static TResult WaitForResult<TResult>(this Task<TResult> task, TimeSpan timeout)
        {
            task.Wait(timeout);
            return task.Result;
        }
    }
}