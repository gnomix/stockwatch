using System;
using System.Linq;

namespace solidware.financials.service.domain.payroll
{
    public class One<T> : Fraction where T : Denominator, new()
    {
        T denominator = new T();

        public void each(Action<int> action)
        {
            denominator.each(x => action(x));
        }

        public int of(int number)
        {
            return number / denominator.each_possible_value().Count();
        }
    }
}