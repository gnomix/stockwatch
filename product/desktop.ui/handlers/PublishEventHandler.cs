using System.Windows.Controls.Primitives;
using gorilla.utility;
using Hardcodet.Wpf.TaskbarNotification;
using solidware.financials.infrastructure;
using solidware.financials.infrastructure.eventing;
using solidware.financials.windows.ui.views;

namespace solidware.financials.windows.ui.handlers
{
    public class PublishEventHandler<T> : Handles<T> where T : Event
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
            region_manager.region<TaskbarIcon>(x =>
            {
                x.ShowCustomBalloon(new FancyBalloon {DataContext = new BalloonMessage {BalloonText = "Received {0}".format(typeof (T))}}, PopupAnimation.Slide, 4000);
            });
        }
    }

    public class BalloonMessage
    {
        public string BalloonText { get; set; }
    }
}