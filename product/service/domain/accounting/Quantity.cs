using System;

namespace solidware.financials.service.domain.accounting
{
    public class Quantity : IEquatable<Quantity>
    {
        decimal amount;
        UnitOfMeasure units;

        public Quantity(decimal amount, UnitOfMeasure units)
        {
            this.units = units;
            this.amount = amount;
        }

        public Quantity plus(Quantity other)
        {
            return new Quantity(amount + other.convert_to(units).amount, units);
        }

        public Quantity subtract(Quantity other)
        {
            return new Quantity(amount - other.convert_to(units).amount, units);
        }

        public Quantity convert_to(UnitOfMeasure unit_of_measure)
        {
            return new Quantity(unit_of_measure.convert(amount, units), unit_of_measure);
        }

        static public implicit operator decimal(Quantity quanity)
        {
            return quanity.amount;
        }

        public bool Equals(Quantity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.amount.Equals(amount) && Equals(other.units, units);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Quantity)) return false;
            return Equals((Quantity) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (amount.GetHashCode()*397) ^ (units != null ? units.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return units.pretty_print(amount);
        }
    }
}