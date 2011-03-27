using Machine.Specifications;
using solidware.financials.service.domain.accounting;

namespace specs.unit.service.domain.accounting
{
    public class SummaryAccountSpecs
    {
        public abstract class concern
        {
            Establish context = () =>
            {
                sut = SummaryAccount.New(Currency.CAD);
            };

            static protected SummaryAccount sut;
        }

        [Concern(typeof (SummaryAccount))]
        public class when_retrieving_the_balance_from_a_summary_account : concern
        {
            Establish c = () =>
            {
                cash = DetailAccount.New(Currency.CAD);
                credit = DetailAccount.New(Currency.CAD);

                cash.deposit(50, Currency.CAD);
                credit.deposit(50, Currency.CAD);
            };

            Because b = () =>
            {
                sut.add(cash);
                sut.add(credit);
                result = sut.balance();
            };

            It should_sum_the_balance_for_each_detail_account = () =>
            {
                result.should_be_equal_to(new Quantity(100.00m, Currency.CAD));
            };

            static Quantity result;
            static DetailAccount cash;
            static DetailAccount credit;
        }
    }
}