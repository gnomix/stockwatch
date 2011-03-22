namespace solidware.financials.service.domain.payroll
{
    public class Units
    {
        static public readonly Units Empty = new Units();
        Units() {}

        static public Units New(int units)
        {
            return new Units {units = units};
        }

        public Money value_at(double price)
        {
            return price*units;
        }

        public Units combined_with(Units other)
        {
            return New(units + other.units);
        }

        public Units reduced_by(Fraction portion)
        {
            return New(portion.of(units));
        }

        int units;
    }
}