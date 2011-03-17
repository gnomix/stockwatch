namespace solidware.financials.infrastructure
{
    public interface Handles<T>
    {
        void handle(T item);
    }
}