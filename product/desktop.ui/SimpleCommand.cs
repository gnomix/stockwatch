using System;

namespace solidware.financials.windows.ui
{
    public class SimpleCommand : IObservableCommand
    {
        Action action = () => {};
        Func<bool> predicate;

        public SimpleCommand(Action action) : this(action, () => true) {}

        public SimpleCommand(Action action, Func<bool> predicate)
        {
            this.action = action ?? (() => {});
            this.predicate = predicate ?? (() => true);
        }

        public event EventHandler CanExecuteChanged = (o, e) => {};

        public void Execute(object parameter)
        {
            action();
        }

        public bool CanExecute(object parameter)
        {
            return predicate();
        }

        public void notify_observers()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}