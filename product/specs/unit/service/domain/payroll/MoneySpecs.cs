using Machine.Specifications;
using solidware.financials.service.domain.payroll;

namespace specs.unit.service.domain.payroll
{
    public class MoneySpecs
    {
        [Concern(typeof (Money))]
        public class when_two_monies_are_the_same
        {
            It should_be_equal = () =>
            {
                Money sut = 100.00m;
                sut.Equals(100.00m);
            };
        }
    }
}