namespace solidware.financials.service.domain.accounting
{
    public class Entry
    {
        static public Entry New<Transaction>(double amount, UnitOfMeasure units) where Transaction : TransactionType, new()
        {
            return New<Transaction>(new Quantity(amount, units));
        }

        static public Entry New<Transaction>(Quantity amount) where Transaction : TransactionType, new()
        {
            return new Entry
                   {
                       when_booked = Calendar.now(),
                       transaction_type = new Transaction(),
                       amount = amount,
                   };
        }

        public virtual Quantity adjust(Quantity balance)
        {
            return transaction_type.adjust(balance, amount);
        }

        public virtual bool booked_in(Range<Date> period)
        {
            return period.includes(when_booked);
        }

        Date when_booked;
        TransactionType transaction_type;
        Quantity amount;
    }
}