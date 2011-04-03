using System.Windows;
using AvalonDock;
using gorilla.utility;
using solidware.financials.windows.ui.views;

namespace solidware.financials.windows.ui
{
    public class TabRegionConfiguration : Configuration<DocumentPane>
    {
        readonly TabPresenter presenter;
        readonly FrameworkElement view;

        public TabRegionConfiguration(TabPresenter presenter, FrameworkElement view)
        {
            this.presenter = presenter;
            this.view = view;
        }

        public void configure(DocumentPane item)
        {
            var document = new DocumentContent
                           {
                               Title = presenter.Header,
                               Content = view,
                               IsCloseable = false
                           };
            item.Items.Add(document);
            document.Show(((Shell) Application.Current.MainWindow).DockManager);
            //document.Activate();
        }
    }
}