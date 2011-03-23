using System;
using Machine.Specifications;
using solidware.financials.service.domain;

namespace specs.unit.service.domain.payroll
{
    public class DateSpecs
    {
        [Concern(typeof (Date))]
        public class when_two_dates_are_the_same
        {
            It should_be_equal = () =>
            {
                Date sut = new DateTime(2009, 01, 01, 01, 00, 00);
                sut.Equals(new DateTime(2009, 01, 01, 09, 00, 01)).should_be_true();
            };
        }
    }
}