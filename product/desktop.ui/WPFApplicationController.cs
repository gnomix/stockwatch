namespace desktop.ui
{
    public class WPFApplicationController : IApplicationController
    {
        readonly IStartAllModules startAllModules;
        readonly EventAggregator eventAggregator;
        readonly IRegionManager regionManager;

        public WPFApplicationController(IStartAllModules startAllModules, EventAggregator eventAggregator,
                                        IRegionManager regionManager)
        {
            this.startAllModules = startAllModules;
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
        }

        public void start()
        {
            startAllModules.run(regionManager);
            eventAggregator.register(this);
        }
    }
}