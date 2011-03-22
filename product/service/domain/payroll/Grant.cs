using System.Collections.Generic;

namespace solidware.financials.service.domain.payroll
{
    public class Grant
    {
        Date issued_on;
        History<UnitPrice> price_history = new History<UnitPrice>();
        Units units = Units.Empty;
        List<Vest> expirations = new List<Vest>();

        static public Grant New(Money purchase_amount, UnitPrice price, Fraction portion, Frequency frequency)
        {
            var grant = new Grant
                        {
                            issued_on = Calendar.now(),
                        };
            grant.change_unit_price_to(price);
            grant.purchase(purchase_amount);
            grant.apply_vesting_frequency(portion, frequency);
            return grant;
        }

        Grant() {}

        public virtual void change_unit_price_to(UnitPrice new_price)
        {
            price_history.record(new_price);
        }

        public virtual bool was_issued_on(Date date)
        {
            return issued_on.Equals(date);
        }

        public virtual Money balance()
        {
            return balance(Calendar.now());
        }

        public virtual Money balance(Date on_date)
        {
            return unit_price(on_date).total_value_of(units_remaining(on_date));
        }

        Units units_remaining(Date on_date)
        {
            var remaining = Units.Empty;
            foreach (var expiration in expirations)
            {
                remaining = remaining.combined_with(expiration.unvested_units(units, on_date));
            }
            return remaining;
        }

        void purchase(Money amount)
        {
            units = units.combined_with(current_unit_price().purchase_units(amount));
        }

        UnitPrice current_unit_price()
        {
            return unit_price(Calendar.now());
        }

        UnitPrice unit_price(Date on_date)
        {
            return price_history.recorded(on_date);
        }

        void apply_vesting_frequency(Fraction portion, Frequency frequency)
        {
            var next_date = issued_on.minus_days(1);
            portion.each(x =>
            {
                next_date = frequency.next(next_date);
                expirations.Add(new Vest(portion, next_date));
            });
        }
    }
}