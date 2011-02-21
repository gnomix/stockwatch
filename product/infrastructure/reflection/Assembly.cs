using System;
using System.Collections.Generic;
using utility;

namespace infrastructure.reflection
{
    public interface Assembly
    {
        IEnumerable<Type> all_types();
        IEnumerable<Type> all_types(Specification<Type> matching);
        IEnumerable<Type> all_classes_that_implement<T>();
    }
}