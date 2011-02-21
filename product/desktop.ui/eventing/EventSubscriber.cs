namespace desktop.ui.eventing
{
    public interface EventSubscriber<Event> where Event : eventing.Event
    {
        void notify(Event message);
    }
}