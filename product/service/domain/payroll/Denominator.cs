using System;
using System.Collections.Generic;

namespace solidware.financials.service.domain.payroll
{
    public interface Denominator
    {
        IEnumerable<int> each_possible_value();
        void each(Action<int> action);
    }
}