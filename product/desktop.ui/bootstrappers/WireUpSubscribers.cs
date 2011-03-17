using gorilla.infrastructure.container;
using solidware.financials.infrastructure.eventing;
using solidware.financials.windows.ui.presenters;

namespace solidware.financials.windows.ui.bootstrappers
{
    public class WireUpSubscribers : NeedStartup
    {
        public void run()
        {
            var eventAggregator = Resolve.the<EventAggregator>();
            eventAggregator.subscribe(Resolve.the<IfFamilyMemberIsSelected>());
        }
    }
}