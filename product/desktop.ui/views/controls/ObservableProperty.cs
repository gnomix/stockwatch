using System;
using System.ComponentModel;
using solidware.financials.windows.ui.presenters.validation;

namespace solidware.financials.windows.ui.views.controls
{
    public class ObservableProperty<T> : Observable<T>, IDataErrorInfo
    {
        T value;

        public ObservableProperty() : this(default(T)) {}

        public ObservableProperty(T value)
        {
            this.value = value;
            Notification = new Notification<Observable<T>>();
        }

        public Notification<Observable<T>> Notification { get; private set; }

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

        public void Register<TSeverity>(Func<T, bool> failCondition, Func<string> errorMessage) where TSeverity : Severity, new()
        {
            Notification.Register<TSeverity>(x => x.Value, () => failCondition(Value), errorMessage);
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

        public string this[string property]
        {
            get { return Notification[property]; }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}