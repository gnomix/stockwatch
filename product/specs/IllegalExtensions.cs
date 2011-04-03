using System;
using System.Collections.Generic;

namespace specs
{
    static public class IllegalExtensions
    {
        static public void Add<T>(this IEnumerable<T> items, T item)
        {
            var collection = items as ICollection<T>;
            if (null == collection) throw new ArgumentException("this aint a collection, buddy!");
            collection.Add(item);
        }
    }
}