using System.Reflection;

namespace solidware.financials.service.domain.property_bag
{
    public class PropertyReference<Target> : Property
    {
        readonly PropertyInfo property;

        public PropertyReference(PropertyInfo property)
        {
            this.property = property;
        }

        public bool represents(string name)
        {
            return string.Compare(property.Name, name, true) == 0;
        }
    }
}