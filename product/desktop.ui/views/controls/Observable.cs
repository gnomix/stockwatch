using System.ComponentModel;

namespace solidware.financials.windows.ui.views.controls
{
    public class Observable<T> : IObservable
    {
        private T value;

        public Observable() { }

        public Observable(T value)
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

        object IObservable.Value { get { return value; } }

        public static implicit operator T(Observable<T> val)
        {
            return val.value;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public override string ToString()
        {
            return value.ToString();
        }
    }
}