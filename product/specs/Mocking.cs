using System;
using Rhino.Mocks;

namespace specs
{
    public static class Mocking
    {
        public static void received<T>(this T item, Action<T> action)
        {
            item.AssertWasCalled(action);
        }

        public static void received<T>(this T item, Func<T, object> action)
        {
            item.AssertWasCalled(action);
        }
    }
}