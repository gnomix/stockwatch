using System;

namespace desktop.ui
{
    public interface EventAggregator
    {
        void subscribe_to<Event>(EventSubscriber<Event> subscriber) where Event : ui.Event;
        void register<Listener>(Listener listener);
        void publish<Event>(Event the_event_to_broadcast) where Event : ui.Event;
        void publish<T>(Action<T> call) where T : class;
        void publish<Event>() where Event : ui.Event, new();
    }

    public interface EventSubscriber<Event> where Event : ui.Event
    {
        void notify(Event message);
    }

    public interface Event
    {
    }
}