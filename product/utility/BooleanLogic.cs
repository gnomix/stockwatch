using System;

namespace utility
{
    static public class BooleanLogic
    {
        static public bool not<T>(this T item, Func<T, bool> condition)
        {
            return !condition(item);
        }
    }
}