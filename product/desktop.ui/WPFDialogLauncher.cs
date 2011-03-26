using System.Windows;

namespace solidware.financials.windows.ui
{
    public class WPFDialogLauncher : DialogLauncher
    {
        PresenterFactory factory;

        public WPFDialogLauncher(PresenterFactory factory)
        {
            this.factory = factory;
        }

        public void launch<Presenter, Dialog>() where Presenter : DialogPresenter where Dialog : FrameworkElement, Dialog<Presenter>, new()
        {
            new Dialog().open(factory.create<Presenter>());
        }
    }
}