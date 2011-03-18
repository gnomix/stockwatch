using System.Windows;

namespace solidware.financials.windows.ui
{
    public interface DialogLauncher
    {
        void launch<Presenter, Dialog>() where Presenter : DialogPresenter where Dialog : FrameworkElement, Dialog<Presenter>, new();
    }
}