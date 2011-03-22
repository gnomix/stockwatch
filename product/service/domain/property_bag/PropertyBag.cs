using System.Collections.Generic;

namespace solidware.financials.service.domain.property_bag
{
    public interface PropertyBag
    {
        Property property_named(string name);
        IEnumerable<Property> all();
    }
}