namespace utility
{
    public interface Command
    {
        void run();
    }

    public interface Command<T>
    {
        void run_against(T item);
    }
}