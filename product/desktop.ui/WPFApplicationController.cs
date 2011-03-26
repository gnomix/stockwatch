using System;
using System.Windows;
using AvalonDock;
using solidware.financials.infrastructure.eventing;

namespace solidware.financials.windows.ui
{
    public class WPFApplicationController : ApplicationController
    {
        RegionManager region_manager;
        PresenterFactory factory;
        EventAggregator event_aggregator;

        public WPFApplicationController(RegionManager region_manager, PresenterFactory factory,
                                        EventAggregator event_aggregator)
        {
            this.region_manager = region_manager;
            this.event_aggregator = event_aggregator;
            this.factory = factory;
        }

        public void add_tab<Presenter, View>() where Presenter : TabPresenter
            where View : FrameworkElement, Tab<Presenter>, new()
        {
            var presenter = open<Presenter>();
            configure_region<DocumentPane>(x => x.Items.Add(new DocumentContent
            {
                Title = presenter.Header,
                Content = new View {DataContext = presenter}
            }));
        }

        public void load_region<TPresenter, Region>() where TPresenter : Presenter
            where Region : FrameworkElement, View<TPresenter>, new()
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