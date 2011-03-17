using System.Windows.Input;

namespace solidware.financials.windows.ui
{
    public interface IObservableCommand : ICommand
    {
        void notify_observers();
    }
}