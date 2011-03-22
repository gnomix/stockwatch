using System.Collections.Generic;
using System.Linq;

namespace solidware.financials.service.domain.property_bag
{
    public class PropertyBagWithoutTarget<T> : PropertyBag
    {
        IEnumerable<Property> all_properties;

        public PropertyBagWithoutTarget()
        {
            all_properties = typeof (T).GetProperties().Select(x => (Property) new PropertyReference<T>(x));
        }

        public Property property_named(string name)
        {
            if (all_properties.Any(x => x.represents(name)))
                all_properties.Single(x => x.represents(name));
            return new UnknownProperty();
        }

        public IEnumerable<Property> all()
        {
            return all_properties;
        }
    }
}