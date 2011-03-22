using System;
using gorilla.utility;

namespace solidware.financials.service.domain.payroll
{
    public class Money : IEquatable<Money>
    {
        double value;
        static public Money Zero = new Money(0);

        Money(double value)
        {
            this.value = value;
        }

        static public implicit operator Money(double raw)
        {
            return new Money(raw);
        }

        public virtual Money plus(Money other)
        {
            return value + other.value;
        }

        public virtual Money minus(Money other)
        {
            return value - other.value;
        }

        public virtual bool Equals(Money other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.value.Equals(value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Money)) return false;
            return Equals((Money) obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return "{0:C}".format(value);
        }

        public Units at_price(double price)
        {
            return Units.New((int)(value / price));
        }
    }
}