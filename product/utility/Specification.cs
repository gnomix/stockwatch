namespace utility
{
    public interface Specification<T>
    {
        bool is_satisfied_by(T item);
    }
}