using desktop.ui.eventing;
using desktop.ui.presenters;
using gorilla.infrastructure.container;

namespace desktop.ui.bootstrappers
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