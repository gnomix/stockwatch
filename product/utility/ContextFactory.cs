namespace utility
{
    public class ContextFactory : IContextFactory
    {
        public IContext create_for(IScopedStorage storage)
        {
            return new Context(storage.provide_storage());
        }
    }
}