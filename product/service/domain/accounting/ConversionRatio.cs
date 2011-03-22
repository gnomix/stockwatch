namespace solidware.financials.service.domain.accounting
{
    public class ConversionRatio
    {
        double rate;
        static public readonly ConversionRatio Default = new ConversionRatio(1);

        public ConversionRatio(double rate)
        {
            this.rate = rate;
        }

        public double applied_to(double amount)
        {
            return amount*rate;
        }
    }
}