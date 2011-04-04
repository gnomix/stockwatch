using System.Windows;

namespace solidware.financials.windows.ui
{
    public interface ApplicationController 
    {
        void add_tab<Presenter, Tab>() where Presenter : TabPresenter where Tab : Tab<Presenter>, new();
        void load_tab<Presenter, Tab>(Presenter presenter) where Presenter : TabPresenter where Tab : Tab<Presenter>, new();

        void load_region<Presenter, Region>() where Presenter : ui.Presenter where Region : FrameworkElement, View<Presenter>, new();
    }
}