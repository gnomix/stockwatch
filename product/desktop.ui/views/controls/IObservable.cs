using System.ComponentModel;

namespace solidware.financials.windows.ui.views.controls
{
    public interface IObservable : INotifyPropertyChanged
    {
        object Value { get; }
    }
}