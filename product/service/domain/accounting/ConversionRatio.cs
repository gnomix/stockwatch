namespace solidware.financials.service.domain.accounting
{
    public class ConversionRatio
    {
        decimal rate;
        static public readonly ConversionRatio Default = new ConversionRatio(1);

        public ConversionRatio(decimal rate)
        {
            this.rate = rate;
        }

        public decimal applied_to(decimal amount)
        {
            return amount*rate;
        }
    }
}