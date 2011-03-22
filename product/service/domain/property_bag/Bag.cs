namespace solidware.financials.service.domain.property_bag
{
    public class Bag
    {
        static public PropertyBag For<Target>()
        {
            return new PropertyBagWithoutTarget<Target>();
        }
    }
}