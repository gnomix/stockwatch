using solidware.financials.infrastructure;
using solidware.financials.infrastructure.eventing;
using solidware.financials.messages;
using solidware.financials.windows.ui.views;

namespace solidware.financials.windows.ui.handlers
{
    public class PublishEventHandler<T> : Handles<T> where T : Announcement
    {
        EventAggregator event_aggregator;
        RegionManager region_manager;

        public PublishEventHandler(EventAggregator event_aggregator, RegionManager regionManager)
        {
            this.event_aggregator = event_aggregator;
            region_manager = regionManager;
        }

        public void handle(T item)
        {
            event_aggregator.publish(item);
            region_manager.region<TrayIcon>(x =>
            {
                item.AnnounceUsing(x);
            });
        }
    }
}