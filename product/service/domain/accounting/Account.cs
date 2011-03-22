namespace solidware.financials.service.domain.accounting
{
    public interface Account
    {
        Quantity balance();
        Quantity balance(Date date);
        Quantity balance(Range<Date> period);
    }
}