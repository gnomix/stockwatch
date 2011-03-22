namespace solidware.financials.service.domain.accounting
{
    public class Potential<Transaction> : PotentialEntry where Transaction : TransactionType, new()
    {
        static public Potential<Transaction> New(DetailAccount destination, Quantity amount)
        {
            return new Potential<Transaction>
                   {
                       account = destination,
                       amount = amount,
                   };
        }

        public Quantity combined_with(Quantity other)
        {
            return new Transaction().adjust(other, amount);
        }

        public void commit()
        {
            account.add(Entry.New<Transaction>(amount));
        }

        DetailAccount account;
        Quantity amount;
    }
}