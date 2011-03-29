using System.Collections.Generic;

namespace solidware.financials.service.orm
{
    public interface IdentityMap<TKey, TValue>
    {
        IEnumerable<TValue> all();
        void add(TKey key, TValue value);
        void update_the_item_for(TKey key, TValue new_value);
        bool contains_an_item_for(TKey key);
        TValue item_that_belongs_to(TKey key);
        void evict(TKey key);
    }
}