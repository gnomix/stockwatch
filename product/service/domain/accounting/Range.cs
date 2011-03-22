using System;

namespace solidware.financials.service.domain.accounting
{
    public interface Range<T> where T : IComparable<T>
    {
        bool includes(T item);
    }
}