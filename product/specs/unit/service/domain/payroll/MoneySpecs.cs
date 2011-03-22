using Machine.Specifications;
using solidware.financials.service.domain.payroll;

namespace specs.unit.service.domain.payroll
{
    public class MoneySpecs
    {
        public abstract class concern : runner<Money> {}

        [Concern(typeof (Money))]
        public class when_two_monies_are_the_same : concern
        {
            It should_be_equal = () =>
            {
                sut.Equals(100.00);
            };

            protected override Money create_sut()
            {
                return 100.00;
            }
        }
    }
}