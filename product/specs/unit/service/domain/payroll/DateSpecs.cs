using System;
using Machine.Specifications;
using solidware.financials.service.domain;

namespace specs.unit.service.domain.payroll
{
    public class DateSpecs
    {
        public abstract class concern : runner<Date> {}

        [Concern(typeof (Date))]
        public class when_two_dates_are_the_same : concern
        {
            It should_be_equal = () =>
            {
                sut.Equals(new DateTime(2009, 01, 01, 09, 00, 01)).should_be_true();
            };

            protected override Date create_sut()
            {
                return new DateTime(2009, 01, 01, 01, 00, 00);
            }
        }
    }
}