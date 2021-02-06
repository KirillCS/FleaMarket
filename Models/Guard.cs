using System;
using System.Diagnostics;

namespace FleaMarket
{
    public static class Guard
    {
        public static void Requires(Func<bool> predicate, Exception exception)
        {
            if (predicate())
            {
                return;
            }

            Debug.Fail(exception.Message);
            throw exception;
        }

        [Conditional("DEBUG")]
        public static void Ensures(Func<bool> predicate, string message)
        {
            Debug.Assert(predicate(), message);
        }
    }
}
