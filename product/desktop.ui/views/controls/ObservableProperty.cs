using System;
using System.ComponentModel;

namespace solidware.financials.windows.ui.views.controls
{
    public class ObservableProperty<T> : Observable<T>
    {
        T value;

        public ObservableProperty() : this(default(T)) {}

        public ObservableProperty(T value)
        {
            this.value = value;
        }

        public T Value
        {
            get { return value; }
            set
            {
                this.value = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Value"));
            }
        }

        public void WhenChanged(Action observer)
        {
            PropertyChanged += (o, e) => observer();
        }

        object Observable.Value
        {
            get { return value; }
        }

        static public implicit operator T(ObservableProperty<T> val)
        {
            return val.value;
        }

        public event PropertyChangedEventHandler PropertyChanged = (o, e) => {};

        public override string ToString()
        {
            return value.ToString();
        }
    }
}