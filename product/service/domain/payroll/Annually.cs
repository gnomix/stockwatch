namespace solidware.financials.service.domain.payroll
{
    public class Annually : Frequency
    {
        public Date next(Date from_date)
        {
            return from_date.plus_years(1);
        }
    }
}