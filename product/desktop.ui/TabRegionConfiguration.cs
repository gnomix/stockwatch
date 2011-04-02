using System.Windows;
using AvalonDock;
using gorilla.utility;

namespace solidware.financials.windows.ui
{
    public class TabRegionConfiguration :
        //Configuration<ResizingPanel>,
        Configuration<DocumentPane>
    {
        readonly TabPresenter presenter;
        readonly FrameworkElement view;

        public TabRegionConfiguration(TabPresenter presenter, FrameworkElement view)
        {
            this.presenter = presenter;
            this.view = view;
        }

        public void configure(ResizingPanel panel)
        {
            var pane = new DocumentPane();
            pane.Items.Add(new DocumentContent
                           {
                               Title = presenter.Header,
                               Content = view,
                           });
            panel.Children.Add(pane);
        }

        public void configure(DocumentPane item)
        {
            item.Items.Add(new DocumentContent
                           {
                               Title = presenter.Header,
                               Content = view,
                               IsCloseable = false
                           });
        }
    }
}