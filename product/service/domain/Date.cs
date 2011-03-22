using System;
using gorilla.utility;

namespace solidware.financials.service.domain
{
    public class Date : IComparable<Date>
    {
        long ticks;
        static public readonly Date First = new Date(DateTime.MinValue);
        static public readonly Date Last = new Date(DateTime.MaxValue);

        Date(DateTime date)
        {
            ticks = date.Date.Ticks;
        }

        static public implicit operator Date(DateTime raw)
        {
            return new Date(raw);
        }

        public bool Equals(Date other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.ticks == ticks;
        }

        public int CompareTo(Date other)
        {
            return to_date_time().CompareTo(new DateTime(other.ticks));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Date)) return false;
            return Equals((Date) obj);
        }

        public override int GetHashCode()
        {
            return ticks.GetHashCode();
        }

        public override string ToString()
        {
            return "{0}".format(to_date_time());
        }

        public DateTime to_date_time()
        {
            return new DateTime(ticks);
        }
    }
}