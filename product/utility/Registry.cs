using System.Collections.Generic;

namespace utility
{
    public interface Registry<T> : IEnumerable<T>
    {
        IEnumerable<T> all();
    }
}