using solidware.financials.infrastructure;
using solidware.financials.infrastructure.eventing;

namespace solidware.financials.windows.ui.handlers
{
    public class PublishEventHandler<T> : Handles<T> where T : Event
    {
        EventAggregator event_aggregator;

        public PublishEventHandler(EventAggregator event_aggregator)
        {
            this.event_aggregator = event_aggregator;
        }

        public void handle(T item)
        {
            event_aggregator.publish(item);
        }
    }
}