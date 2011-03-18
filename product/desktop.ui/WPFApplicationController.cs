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

        public WpfApplicationController(RegionManager region_manager, PresenterFactory factory, EventAggregator event_aggregator)
        {
            this.region_manager = region_manager;
            this.event_aggregator = event_aggregator;
            this.factory = factory;
        }

        public void add_tab<Presenter, View>() where Presenter : TabPresenter where View : FrameworkElement, Tab<Presenter>, new()
        {
            var presenter = factory.create<Presenter>();
            event_aggregator.subscribe(presenter);
            presenter.present();
            region_manager.region<TabControl>(x => x.Items.Add(new TabItem
                                                               {
                                                                   Header = presenter.Header,
                                                                   Content = new View {DataContext = presenter}
                                                               }));
        }

        public void launch<Presenter, Dialog>() where Presenter : DialogPresenter where Dialog : FrameworkElement, Dialog<Presenter>, new()
        {
            var presenter = factory.create<Presenter>();
            var dialog = new Dialog {DataContext = presenter};
            presenter.close = () => dialog.Close();
            presenter.present();
            dialog.open();
        }

        public void load_region<TPresenter, Region>() where TPresenter : Presenter where Region : FrameworkElement, View<TPresenter>, new()
        {
            var presenter = factory.create<TPresenter>();
            event_aggregator.subscribe(presenter);
            presenter.present();
            region_manager.region<Region>(x =>
            {
                x.DataContext = presenter;
            });
        }
    }
}