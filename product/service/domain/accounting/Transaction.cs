using System.Collections.Generic;
using System.Linq;
using gorilla.utility;

namespace solidware.financials.service.domain.accounting
{
    public class Transaction
    {
        static public Transaction New(UnitOfMeasure reference_units)
        {
            return new Transaction(reference_units);
        }

        Transaction(UnitOfMeasure reference)
        {
            reference_units = reference;
        }

        public void deposit(DetailAccount destination, Quantity amount)
        {
            deposits.Add(Potential<Deposit>.New(destination, amount));
        }

        public void withdraw(DetailAccount source, Quantity amount)
        {
            withdrawals.Add(Potential<Withdrawal>.New(source, amount));
        }

        public void post()
        {
            ensure_zero_balance();
            deposits.Union(withdrawals).each(x => x.commit());
        }

        void ensure_zero_balance()
        {
            var balance = calculate_total(deposits.Union(withdrawals));
            if (balance == 0) return;

            throw new TransactionDoesNotBalance();
        }

        Quantity calculate_total(IEnumerable<PotentialEntry> potential_transactions)
        {
            var result = new Quantity(0, reference_units);
            potential_transactions.each(x => result = x.combined_with(result));
            return result;
        }

        List<PotentialEntry> deposits = new List<PotentialEntry>();
        List<PotentialEntry> withdrawals = new List<PotentialEntry>();
        UnitOfMeasure reference_units;
    }
}