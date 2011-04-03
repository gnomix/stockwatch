using gorilla.infrastructure.container;
using gorilla.infrastructure.threading;
using utility;

namespace solidware.financials.windows.ui.bootstrappers
{
    public class StopEssentialServices : NeedsShutdown
    {
        DependencyRegistry registry;

        public StopEssentialServices(DependencyRegistry registry)
        {
            this.registry = registry;
        }

        public void run()
        {
            registry.get_a<CommandProcessor>().stop();
            registry.get_a<Timer>().Dispose();
        }
    }
}