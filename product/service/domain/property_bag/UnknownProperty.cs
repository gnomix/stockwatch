using System;

namespace solidware.financials.service.domain.property_bag
{
    public class UnknownProperty : Property
    {
        public bool represents(string name)
        {
            throw new NotImplementedException();
        }
    }
}