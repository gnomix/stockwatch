using System;
using System.Collections.Generic;
using gorilla.utility;

namespace utility
{
    static public class MapperRegistery
    {
        static Dictionary<MapKey, object> mappings = new Dictionary<MapKey, object>();

        static public void Register<Input, Output>(Func<Input, Output> conversion)
        {
            mappings.Add(new MapKey<Input, Output>(), conversion);
        }

        static public Output Map<Input, Output>(Input item)
        {
            var converter = mappings[new MapKey<Input, Output>()];
            return converter.downcast_to<Func<Input, Output>>()(item);
        }
    }
}