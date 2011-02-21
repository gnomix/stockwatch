namespace utility
{
    public interface IContextFactory
    {
        IContext create_for(IScopedStorage storage);
    }
}