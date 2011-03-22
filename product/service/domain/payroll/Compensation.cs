using System.Collections.Generic;
using System.Linq;
using gorilla.utility;

namespace solidware.financials.service.domain.payroll
{
    public class Compensation : Visitable<Grant>
    {
        IList<Grant> grants = new List<Grant>();

        public void issue_grant(Money grant_value, UnitPrice price, Fraction portion_to_issue_at_each_vest, Frequency frequency)
        {
            grants.Add(Grant.New(grant_value, price, portion_to_issue_at_each_vest, frequency));
        }

        public Grant grant_for(Date date)
        {
            return grants.Single(x => x.was_issued_on(date));
        }

        public Money unvested_balance(Date on_date)
        {
            var total = Money.Zero;
            accept(new AnonymousVisitor<Grant>(grant => total = total.plus(grant.balance(on_date))));
            return total;
        }

        public void accept(Visitor<Grant> visitor)
        {
            grants.each(x => visitor.visit(x));
        }
    }
}