namespace utility
{
    public class EmptyCallback<T> : Callback<T>, Callback
    {
        public void run_against(T item) {}

        public void run() {}
    }
}