using System;
using System.ComponentModel;

namespace solidware.financials.windows.ui.views.controls
{
    public interface Observable<T> : Observable
    {
        T Value { get; set; }
        void WhenChanged(Action observer);
    }

    public interface Observable : INotifyPropertyChanged
    {
        object Value { get; }
    }
}