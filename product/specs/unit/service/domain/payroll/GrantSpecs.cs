using System;
using Machine.Specifications;
using solidware.financials.service.domain;
using solidware.financials.service.domain.payroll;

namespace specs.unit.service.domain.payroll
{
    public class GrantSpecs
    {
        public abstract class concern : runner<Grant>
        {
            protected override Grant create_sut()
            {
                Calendar.stop(() => new DateTime(2010, 01, 01));
                return Grant.New(120, 10, new One<Twelfth>(), new Monthly());
            }
        }

        [Concern(typeof (Grant))]
        public class when_checking_what_the_outstanding_balance_of_a_grant_is : concern
        {
            It should_return_the_full_balance_before_the_first_vesting_date = () =>
            {
                Calendar.stop(() => new DateTime(2010, 01, 31));
                sut.balance().should_be_equal_to(120);
            };

            It should_return_the_unvested_portion_after_the_first_vesting_date = () =>
            {
                Calendar.stop(() => new DateTime(2010, 02, 01));
                sut.balance().should_be_equal_to(110);
            };
        }

        [Concern(typeof (Grant))]
        public class when_checking_what_the_value_of_a_grant_was_in_the_past : concern
        {
            Because b = () =>
            {
                Calendar.stop(() => january_15);
                sut.change_unit_price_to(20);

                Calendar.reset();
                sut.change_unit_price_to(40);
                result = sut.balance(january_15);
            };

            It should_return_the_correct_amount = () =>
            {
                result.should_be_equal_to(240);
            };

            static Money result;
            static Date january_15 = new DateTime(2010, 01, 15);
        }
    }
}