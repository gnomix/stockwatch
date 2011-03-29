using System.Collections.Generic;

namespace solidware.financials.service.orm
{
    public class SimpleIdentityMap<TKey, TValue> : IdentityMap<TKey, TValue>
    {
        readonly IDictionary<TKey, TValue> items_in_map;

        public SimpleIdentityMap() : this(new Dictionary<TKey, TValue>())
        {
        }

        public SimpleIdentityMap(IDictionary<TKey, TValue> items_in_map)
        {
            this.items_in_map = items_in_map;
        }

        public IEnumerable<TValue> all()
        {
            return items_in_map.Values;
        }

        public void add(TKey key, TValue value)
        {
            items_in_map.Add(key, value);
        }

        public void update_the_item_for(TKey key, TValue new_value)
        {
            if (contains_an_item_for(key)) items_in_map[key] = new_value;
            else add(key, new_value);
        }

        public bool contains_an_item_for(TKey key)
        {
            return items_in_map.ContainsKey(key);
        }

        public TValue item_that_belongs_to(TKey key)
        {
            return contains_an_item_for(key) ? items_in_map[key] : default(TValue);
        }

        public void evict(TKey key)
        {
            if (contains_an_item_for(key)) items_in_map.Remove(key);
        }
    }
}