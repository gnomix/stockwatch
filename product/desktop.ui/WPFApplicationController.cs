namespace desktop.ui
{
    public class WPFApplicationController : ApplicationController
    {
        readonly IStartAllModules startAllModules;
        readonly IEventAggregator eventAggregator;
        readonly IRegionManager regionManager;

        public WPFApplicationController(IStartAllModules startAllModules, IEventAggregator eventAggregator,
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