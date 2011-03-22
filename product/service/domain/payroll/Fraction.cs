using System;

namespace solidware.financials.service.domain.payroll
{
    public interface Fraction
    {
        void each(Action<int> action);
        int of(int number);
    }
}