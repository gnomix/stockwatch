using desktop.ui;
using Machine.Specifications;

namespace specs
{
    public class ApplicationControllerSpecs
    {
        public class concern
        {
            Establish context = () =>
            {
                start_all_modules = The.dependency<IStartAllModules>();
                event_aggregator = The.dependency<EventAggregator>();
                region_manager = The.dependency<IRegionManager>();
                sut = new WPFApplicationController(start_all_modules, event_aggregator, region_manager);
            };

            protected static WPFApplicationController sut;
            protected static IStartAllModules start_all_modules;
            protected static EventAggregator event_aggregator;
            protected static IRegionManager region_manager;
        }

        public class when_starting_up : concern
        {
            Because of = () => { sut.start(); };

            It should_start_all_of_the_application_modules = () =>
            {
                start_all_modules.received(x => x.run(region_manager));
            };

            It should_register_itself_as_a_listener_with_the_event_aggregator = () =>
            {
                event_aggregator.received(x => x.register(sut));
            };
        }
    }
}