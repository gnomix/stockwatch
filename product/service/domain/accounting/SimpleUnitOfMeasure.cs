namespace solidware.financials.service.domain.accounting
{
    public delegate ConversionRatio RateTable(UnitOfMeasure unitCurrency, UnitOfMeasure referenceCurrency);

    public abstract class SimpleUnitOfMeasure : UnitOfMeasure
    {
        public decimal convert(decimal amount, UnitOfMeasure other)
        {
            return rate_table(this, other).applied_to(amount);
        }

        public abstract string pretty_print(decimal amount);

        static RateTable rate_table = (x, y) => ConversionRatio.Default;

        static public void provide_rate(RateTable current_rates)
        {
            rate_table = current_rates;
        }
    }
}