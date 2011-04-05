using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace solidware.financials.windows.ui.presenters.validation
{
    public interface INotification<T> : IDataErrorInfo
    {
        string this[Expression<Func<T, object>> property] { get; }
    }
}