using System;
using Machine.Specifications;
using solidware.financials.service.domain;

namespace specs.unit.service.domain
{
    public class DateSpecs
    {
        public abstract class concern  {}

        [Concern(typeof (Date))]
        public class when_checking_if_a_date_is_before_another : concern
        {
            Establish c = () =>
            {
                today = DateTime.Now;
                tomorrow = today.plus_days(1);
            };

            It should_return_true_when_it_is = () =>
            {
                today.is_before(tomorrow).should_be_true();
            };

            It should_return_false_when_it_is_not = () =>
            {
                tomorrow.is_before(today).should_be_false();
            };

            static Date today;
            static Date tomorrow;
        }

        [Concern(typeof (Date))]
        public class when_checking_if_a_date_is_after_another : concern
        {
            Establish c = () =>
            {
                today = DateTime.Now;
                tomorrow = today.plus_days(1);
            };

            It should_return_true_when_it_is = () =>
            {
                tomorrow.is_after(today).should_be_true();
            };

            It should_return_false_when_it_is_not = () =>
            {
                today.is_after(tomorrow).should_be_false();
            };

            static Date today;
            static Date tomorrow;
        }
    }
}