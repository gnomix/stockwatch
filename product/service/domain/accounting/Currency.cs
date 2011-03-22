using gorilla.utility;

namespace solidware.financials.service.domain.accounting
{
    public class Currency : SimpleUnitOfMeasure
    {
        static public readonly Currency USD = new Currency("USD");
        static public readonly Currency CAD = new Currency("CAD");

        Currency(string pneumonic)
        {
            this.pneumonic = pneumonic;
        }

        public override string pretty_print(double amount)
        {
            return "{0:C} {1}".format(amount, this);
        }

        public bool Equals(Currency other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.pneumonic, pneumonic);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Currency)) return false;
            return Equals((Currency) obj);
        }

        public override int GetHashCode()
        {
            return (pneumonic != null ? pneumonic.GetHashCode() : 0);
        }

        static public bool operator ==(Currency left, Currency right)
        {
            return Equals(left, right);
        }

        static public bool operator !=(Currency left, Currency right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return pneumonic;
        }

        string pneumonic;
    }
}