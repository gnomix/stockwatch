using Machine.Specifications;
using solidware.financials.service.orm;

namespace specs.unit.service.orm
{
    public class SimpleIdentityMapSpecs
    {
        [Concern(typeof (SimpleIdentityMap<,>))]
        public abstract class behaves_like_identity_map
        {
            Establish context = () =>
            {
                sut = new SimpleIdentityMap<int, string>();
            };

            static protected IdentityMap<int, string> sut;
        }

        [Concern(typeof (SimpleIdentityMap<,>))]
        public class when_getting_an_item_from_the_identity_map_for_an_item_that_has_been_added : behaves_like_identity_map
        {
            It should_return_the_item_that_was_added_for_the_given_key = () =>
            {
                result.should_be_equal_to("1");
            };

            Because b = () =>
            {
                sut.add(1, "1");
                result = sut.item_that_belongs_to(1);
            };

            static string result;
        }

        [Concern(typeof (SimpleIdentityMap<,>))]
        public class when_getting_an_item_from_the_identity_map_that_has_not_been_added : behaves_like_identity_map
        {
            It should_return_the_default_value_for_that_type = () =>
            {
                result.should_be_equal_to(null);
            };

            Because b = () =>
            {
                result = sut.item_that_belongs_to(2);
            };

            static string result;
        }

        [Concern(typeof (SimpleIdentityMap<,>))]
        public class when_checking_if_an_item_has_been_added_to_the_identity_map_that_has_been_added :
            behaves_like_identity_map
        {
            It should_return_true = () =>
            {
                result.should_be_true();
            };

            Because b = () =>
            {
                sut.add(10, "10");
                result = sut.contains_an_item_for(10);
            };

            static bool result;
        }

        [Concern(typeof (SimpleIdentityMap<,>))]
        public class when_checking_if_an_item_has_been_added_to_the_identity_map_that_has_not_been_added :
            behaves_like_identity_map
        {
            It should_return_false = () =>
            {
                result.should_be_false();
            };

            Because b = () =>
            {
                result = sut.contains_an_item_for(9);
            };

            static bool result;
        }

        [Concern(typeof (SimpleIdentityMap<,>))]
        public class when_updating_the_value_for_a_key_that_has_already_been_added_to_the_identity_map :
            behaves_like_identity_map
        {
            It should_replace_the_old_item_with_the_new_one = () =>
            {
                result.should_be_equal_to("7");
            };

            Because b = () =>
            {
                sut.add(6, "6");
                sut.update_the_item_for(6, "7");
                result = sut.item_that_belongs_to(6);
            };

            static string result;
        }

        [Concern(typeof (SimpleIdentityMap<,>))]
        public class when_updating_the_value_for_a_key_that_has_not_been_added_to_the_identity_map : behaves_like_identity_map
        {
            It should_add_the_new_item = () =>
            {
                result.should_be_equal_to("3");
            };

            Because b = () =>
            {
                sut.update_the_item_for(3, "3");
                result = sut.item_that_belongs_to(3);
            };

            static string result;
        }
    }
}