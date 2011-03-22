using System.Collections.Generic;
using gorilla.utility;
using solidware.financials.service.domain.payroll;

namespace solidware.financials.service.domain.accounting
{
    public class DetailAccount : Account, Visitable<Entry>
    {
        DetailAccount(UnitOfMeasure unit_of_measure)
        {
            this.unit_of_measure = unit_of_measure;
        }

        static public DetailAccount New(UnitOfMeasure unit_of_measure)
        {
            return new DetailAccount(unit_of_measure);
        }

        public void deposit(double amount)
        {
            deposit(amount, unit_of_measure);
        }

        public void deposit(double amount, UnitOfMeasure units)
        {
            add(Entry.New<Deposit>(amount, units));
        }

        public void withdraw(double amount, UnitOfMeasure units)
        {
            add(Entry.New<Withdrawal>(amount, units));
        }

        public void add(Entry new_entry)
        {
            entries.Add(new_entry);
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
            accept(new AnonymousVisitor<Entry>(x =>
            {
                if (x.booked_in(period)) result = x.adjust(result);
            }));
            return result;
        }

        public void accept(Visitor<Entry> visitor)
        {
            foreach (var entry in entries) visitor.visit(entry);
        }

        IList<Entry> entries = new List<Entry>();
        UnitOfMeasure unit_of_measure;
    }
}