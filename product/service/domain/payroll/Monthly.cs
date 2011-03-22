namespace solidware.financials.service.domain.payroll
{
    public class Monthly : Frequency
    {
        public Date next(Date from_date)
        {
            return from_date.add_months(1);
        }
    }
}