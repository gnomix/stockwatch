using System.Windows;
using System.Windows.Controls;
using desktop.ui.eventing;

namespace desktop.ui
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

        public void launch_dialog<Presenter, Dialog>() where Presenter : DialogPresenter where Dialog : FrameworkElement, Dialog<Presenter>, new()
        {
            var presenter = factory.create<Presenter>();
            var dialog = new Dialog {DataContext = presenter};
            presenter.close = () =>
            {
                dialog.Close();
            };
            presenter.present();
            dialog.open();
        }

        public void load_region<Presenter, Region>() where Presenter : ui.Presenter where Region : FrameworkElement, View<Presenter>, new()
        {
            var presenter = factory.create<Presenter>();
            event_aggregator.subscribe(presenter);
            presenter.present();
            region_manager.region<Region>(x =>
            {
                x.DataContext = presenter;
            });
        }
    }
}