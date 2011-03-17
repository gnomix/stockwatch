using System;
using System.ComponentModel;
using System.Linq.Expressions;
using gorilla.utility;

namespace desktop.ui
{
    public abstract class Observable<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (o, e) => {};

        protected void update(params Expression<Func<T, object>>[] properties)
        {
            properties.each(x =>
            {
                PropertyChanged(this, new PropertyChangedEventArgs(x.pick_property().Name));
            });
        }
    }
}