using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace desktop.ui.presenters
{
    static public class WpfBindingExtensinos
    {
        static public ObservableCollection<T> to_observable<T>(this IEnumerable<T> items)
        {
            return new ObservableCollection<T>(items);
        }
    }
}