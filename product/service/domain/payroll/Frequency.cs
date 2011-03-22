namespace solidware.financials.service.domain.payroll
{
    public interface Frequency
    {
        Date next(Date from_date);
    }
}