using System;
using System.Windows;
using gorilla.utility;
using solidware.financials.infrastructure.eventing;

namespace solidware.financials.windows.ui
{
    public class WPFApplicationController : ApplicationController
    {
        RegionManager region_manager;
        PresenterFactory factory;
        EventAggregator event_aggregator;

        public WPFApplicationController(RegionManager region_manager, PresenterFactory factory, EventAggregator event_aggregator)
        {
            this.region_manager = region_manager;
            this.event_aggregator = event_aggregator;
            this.factory = factory;
        }

        public void add_tab<Presenter, View>() where Presenter : TabPresenter where View : Tab<Presenter>, new()
        {
            var presenter = open<Presenter>();
            var view = new View();
            view.bind_to(presenter);
            region_manager.region(new TabRegionConfiguration(presenter,view.downcast_to<FrameworkElement>()));
        }

        public void load_tab<Presenter, View>(Presenter presenter) where Presenter : TabPresenter where View : Tab<Presenter>, new()
        {
            throw new NotImplementedException();
        }

        public void load_region<TPresenter, Region>() where TPresenter : Presenter where Region : FrameworkElement, View<TPresenter>, new()
        {
            configure_region<Region>(x => { x.DataContext = open<TPresenter>(); });
        }

        void configure_region<TRegion>(Action<TRegion> configure) where TRegion : UIElement
        {
            region_manager.region(configure);
        }

        TPresenter open<TPresenter>() where TPresenter : Presenter
        {
            var presenter = factory.create<TPresenter>();
            event_aggregator.subscribe(presenter);
            presenter.present();
            return presenter;
        }
    }
}