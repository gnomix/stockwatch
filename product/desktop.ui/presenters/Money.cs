using gorilla.utility;
using solidware.financials.windows.ui.views.controls;

namespace solidware.financials.windows.ui.presenters
{
    public class Money
    {
        static public readonly Money Zero = new Money(0m);
        static public Observable<Money> Null { get { return new ObservableProperty<Money>(Zero); } }

        public Money(decimal money)
        {
            this.money = money;
        }
        static public implicit operator decimal(Money money)
        {
            return money.money;
        }

        static public implicit operator Money(decimal money)
        {
            return new Money(money);
        }

        public Money Plus(Money other)
        {
            return new Money(money + other.money);
        }

        public bool Equals(Money other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.money == money;
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
            return money.GetHashCode();
        }

        public override string ToString()
        {
            return "{0:C}".format(money);
        }

        decimal money;
    }
}