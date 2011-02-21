using System;

namespace desktop.ui.eventing
{
    public interface EventAggregator
    {
        void subscribe_to<Event>(EventSubscriber<Event> subscriber) where Event : eventing.Event;
        void subscribe<Listener>(Listener subscriber);
        void publish<Event>(Event the_event_to_broadcast) where Event : eventing.Event;
        void publish<T>(Action<T> call) where T : class;
        void publish<Event>() where Event : eventing.Event, new();
    }
}