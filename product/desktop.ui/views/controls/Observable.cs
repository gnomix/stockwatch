using System;
using System.ComponentModel;

namespace solidware.financials.windows.ui.views.controls
{
    public interface Observable<T> : Observable, IDataErrorInfo
    {
        new T Value { get; set; }
        void WhenChanged(Action observer);
        void Register<Severity>(Func<T, bool> failCondition, Func<string> errorMessage) where Severity : presenters.validation.Severity, new();
    }

    public interface Observable : INotifyPropertyChanged
    {
        object Value { get; }
    }

    static public class ObservableExtensions
    {
        static public void Notify<T>(this Observable<T> observable, ObservableCommand command)
        {
            observable.WhenChanged(() => command.notify_observers());
        }
    }
}