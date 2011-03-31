using System.Collections.Generic;
using System.Collections.ObjectModel;
using solidware.financials.windows.ui.views.controls;

namespace solidware.financials.windows.ui.presenters
{
    static public class WPFBindingExtensions
    {
        static public ObservableCollection<T> to_observable<T>(this IEnumerable<T> items)
        {
            return new ObservableCollection<T>(items);
        }

        static public Observable<T> ToObservable<T>(this T item)
        {
            return new ObservableProperty<T>(item);
        }
    }
}