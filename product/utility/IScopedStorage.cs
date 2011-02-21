using System.Collections;

namespace utility
{
    public interface IScopedStorage
    {
        IDictionary provide_storage();
    }
}