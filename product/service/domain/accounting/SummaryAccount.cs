using System.Collections.Generic;
using gorilla.utility;

namespace solidware.financials.service.domain.accounting
{
    public class SummaryAccount : Account
    {
        static public SummaryAccount New(UnitOfMeasure unit_of_measure)
        {
            return new SummaryAccount
                   {
                       unit_of_measure = unit_of_measure
                   };
        }

        public void add(Account account)
        {
            accounts.Add(account);
        }

        public Quantity balance()
        {
            return balance(Calendar.now());
        }

        public Quantity balance(Date date)
        {
            return balance(DateRange.up_to(date));
        }

        public Quantity balance(Range<Date> period)
        {
            var result = new Quantity(0, unit_of_measure);
            accounts.each(x => result = result.plus(x.balance(period)));
            return result;
        }

        ICollection<Account> accounts = new HashSet<Account>();
        UnitOfMeasure unit_of_measure;
    }
}