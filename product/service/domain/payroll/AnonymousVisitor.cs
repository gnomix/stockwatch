using System;
using gorilla.utility;

namespace solidware.financials.service.domain.payroll
{
    public class AnonymousVisitor<T> : Visitor<T>
    {
        Action<T> action;

        public AnonymousVisitor(Action<T> action)
        {
            this.action = action;
        }

        public void visit(T item)
        {
            action(item);
        }
    }
}