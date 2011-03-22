namespace solidware.financials.service.domain.payroll
{
    public class Vest
    {
        Fraction portion;
        Date vesting_date;

        public Vest(Fraction portion, Date vesting_date)
        {
            this.portion = portion;
            this.vesting_date = vesting_date;
        }

        public Units unvested_units(Units total_units, Date date)
        {
            return expires_before(date) ? Units.Empty : total_units.reduced_by(portion);
        }

        bool expires_before(Date date)
        {
            return vesting_date.is_before(date);
        }
    }
}