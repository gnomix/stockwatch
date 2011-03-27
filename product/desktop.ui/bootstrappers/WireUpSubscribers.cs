using gorilla.infrastructure.container;
using solidware.financials.infrastructure.eventing;
using solidware.financials.windows.ui.events;
using utility;

namespace solidware.financials.windows.ui.bootstrappers
{
    public class WireUpSubscribers : NeedStartup
    {
        public void run()
        {
            var eventAggregator = Resolve.the<EventAggregator>();
            eventAggregator.subscribe(new AnonymousSubscriber<SelectedFamilyMember>(x => Resolve.the<ApplicationState>().PushIn(x)));
        }
    }
}