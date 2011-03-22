namespace solidware.financials.service.domain.payroll
{
    public class UnitPrice
    {
        readonly double price;

        UnitPrice(double price)
        {
            this.price = price;
        }

        static public implicit operator UnitPrice(double raw)
        {
            return new UnitPrice(raw);
        }

        public Units purchase_units(Money amount)
        {
            return amount.at_price(price);
        }

        public virtual Money total_value_of(Units units)
        {
            return units.value_at(price);
        }
    }
}