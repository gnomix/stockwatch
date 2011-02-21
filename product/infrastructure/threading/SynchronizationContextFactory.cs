using System.Threading;
using infrastructure.container;
using utility;

namespace infrastructure.threading
{
    public interface ISynchronizationContextFactory : Factory<ISynchronizationContext> {}

    public class SynchronizationContextFactory : ISynchronizationContextFactory
    {
        readonly DependencyRegistry registry;

        public SynchronizationContextFactory(DependencyRegistry registry)
        {
            this.registry = registry;
        }

        public ISynchronizationContext create()
        {
            return new SynchronizedContext(registry.get_a<SynchronizationContext>());
        }
    }
}