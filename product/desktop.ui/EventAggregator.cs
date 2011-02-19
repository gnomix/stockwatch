namespace desktop.ui
{
    public interface IEventAggregator
    {
        void register<Listener>(Listener listener);
    }
}