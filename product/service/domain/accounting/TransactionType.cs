namespace solidware.financials.service.domain.accounting
{
    public interface TransactionType
    {
        Quantity adjust(Quantity balance, Quantity adjustment);
    }
}