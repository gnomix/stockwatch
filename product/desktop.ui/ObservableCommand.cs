using System.Windows.Input;

namespace solidware.financials.windows.ui
{
    public interface ObservableCommand : ICommand
    {
        void notify_observers();
    }
}