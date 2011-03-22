namespace solidware.financials.service.domain.accounting
{
    public class Deposit : TransactionType
    {
        public Quantity adjust(Quantity balance, Quantity adjustment)
        {
            return balance.plus(adjustment);
        }
    }
}