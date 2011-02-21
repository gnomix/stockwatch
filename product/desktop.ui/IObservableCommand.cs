using System.Windows.Input;

namespace desktop.ui
{
    public interface IObservableCommand : ICommand
    {
        void notify_observers();
    }
}