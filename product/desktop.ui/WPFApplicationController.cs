using System;
using System.Windows;
using System.Windows.Controls;
using solidware.financials.infrastructure.eventing;

namespace solidware.financials.windows.ui
{
    public class WpfApplicationController : ApplicationController
    {
        RegionManager region_manager;
        PresenterFactory factory;
        EventAggregator event_aggregator;

        public WpfApplicationController(RegionManager region_manager, PresenterFactory factory,
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
            configure_region<TabControl>(x => x.Items.Add(new TabItem
            {
                Header = presenter.Header,
                Content = new View {DataContext = presenter}
            }));
        }

        public void launch<Presenter, Dialog>() where Presenter : DialogPresenter
            where Dialog : FrameworkElement, Dialog<Presenter>, new()
        {
            new Dialog().open(factory.create<Presenter>());
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