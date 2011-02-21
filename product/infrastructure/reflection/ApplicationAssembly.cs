using System;
using System.Collections.Generic;
using utility;

namespace infrastructure.reflection
{
    public class ApplicationAssembly : Assembly
    {
        IList<Type> types = new List<Type>();

        public ApplicationAssembly(params System.Reflection.Assembly[] assemblies)
        {
            assemblies.each(x => types.add_range(x.GetTypes()));
        }

        public IEnumerable<Type> all_types()
        {
            return types;
        }

        public IEnumerable<Type> all_types(Specification<Type> matching)
        {
            return all_types().where(x => matching.is_satisfied_by(x));
        }

        public IEnumerable<Type> all_classes_that_implement<Contract>()
        {
            return all_types()
                .where(x => typeof (Contract).IsAssignableFrom(x))
                .where(x => !x.IsInterface)
                .where(x => !x.IsAbstract)
                .where(x => !x.IsGenericType);
        }
    }
}