using System.Windows;

namespace solidware.financials.windows.ui
{
    public interface ApplicationController : DialogLauncher
    {
        void add_tab<Presenter, Tab>() where Presenter : TabPresenter
            where Tab : FrameworkElement, Tab<Presenter>, new();

        void load_region<Presenter, Region>() where Presenter : ui.Presenter
            where Region : FrameworkElement, View<Presenter>, new();
    }
}