using System.Windows;

namespace solidware.financials.windows.ui
{
    public class WPFDialog<TPresenter> : Window, Dialog<TPresenter> where TPresenter : DialogPresenter
    {
        public WPFDialog()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ShowInTaskbar = false;
            WindowStyle = WindowStyle.SingleBorderWindow;
        }

        public void open(TPresenter presenter)
        {
            DataContext = presenter;
            Owner = Application.Current.MainWindow;
            presenter.close = () => Close();
            presenter.present();
            ShowDialog();
        }
    }
}