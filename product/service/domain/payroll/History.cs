using System;
using System.Collections.Generic;
using System.Linq;

namespace solidware.financials.service.domain.payroll
{
    public class History<T>
    {
        Stack<Event<T>> events = new Stack<Event<T>>();

        public void record(T change)
        {
            events.Push(new Event<T>(change));
        }

        public T recorded(Date date)
        {
            return events.Where(x => x.occurred_on_or_before(date)).Max();
        }

        class Event<K> : IComparable<Event<K>>
        {
            public Event(K adjustment)
            {
                date_of_change = Calendar.today();
                this.adjustment = adjustment;
            }

            K adjustment;
            Date date_of_change;

            public int CompareTo(Event<K> other)
            {
                return date_of_change.CompareTo(other.date_of_change);
            }

            public bool occurred_on_or_before(Date date)
            {
                return date_of_change.Equals(date) || date_of_change.is_before(date);
            }

            static public implicit operator K(Event<K> eventA)
            {
                return eventA.adjustment;
            }
        }
    }
}