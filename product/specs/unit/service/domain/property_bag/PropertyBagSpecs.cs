using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using solidware.financials.service.domain.property_bag;

namespace specs.unit.service.domain.property_bag
{
    public class PropertyBagSpecs
    {
        public abstract class concern : runner<PropertyBag>
        {
            Establish c = () => Console.Out.WriteLine("init concern");

            protected override PropertyBag create_sut()
            {
                return Bag.For<TargetType>();
            }
        }

        [Concern(typeof (PropertyBag))]
        public class when_creating_a_property_bag_from_a_known_type : concern
        {

            It should_include_each_property_from_the_target_type = () =>
            {
                sut.property_named("name").should_not_be_null();
            };

            Establish c = () => { Console.Out.WriteLine("blah");};

            It should_not_contain_properties_that_are_not_on_the_target_type = () =>
            {
                sut.property_named("blah").should_be_an_instance_of<UnknownProperty>();
            };
        }

        [Concern(typeof (PropertyBag))]
        public class when_iterating_through_each_property_in_the_bag : concern
        {
            Establish c = () => Console.Out.WriteLine("init when_iterating_through_each_property_in_the_bag");

            It should_contain_a_reference_for_each_property_on_the_target_type = () =>
            {
                results.Count().should_be_equal_to(1);
            };

            Because b = () =>
            {
                results = sut.all();
            };

            static IEnumerable<Property> results;
        }
    }

    public class TargetType
    {
        public string name { get; set; }
    }
}