using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace solidware.financials.windows.ui.views.controls
{
    public class SmartCollection<T> : ObservableCollection<T>
    {
        bool suspend;

        public SmartCollection()
        {
            suspend = false;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!suspend) base.OnCollectionChanged(e);
        }

        public IDisposable BeginEdit()
        {
            return new SuspendNotifications<T>(this);
        }

        class SuspendNotifications<K> : IDisposable
        {
            readonly SmartCollection<K> items;

            public SuspendNotifications(SmartCollection<K> items)
            {
                this.items = items;
                items.suspend = true;
            }

            public void Dispose()
            {
                items.suspend = false;
                items.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }
    }
}