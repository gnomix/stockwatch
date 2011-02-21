namespace utility
{
    public interface Callback : Command
    {
    }

    public interface Callback<T> : Command<T>
    {
    }
}