namespace utility
{
    public interface Mapper<Input, Output>
    {
        Output map_from(Input item);
    }

    public interface Mapper
    {
        Output map_from<Input, Output>(Input item);
    }
}