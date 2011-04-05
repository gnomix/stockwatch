using System;

namespace solidware.financials.windows.ui.presenters.validation
{
    public class AnonymousRule<Severity> : Rule where Severity : validation.Severity, new()
    {
        readonly Func<bool> failCondition;
        readonly Func<string> errorMessage;

        public AnonymousRule(Func<bool> failCondition, Func<string> errorMessage)
        {
            this.failCondition = failCondition;
            this.errorMessage = errorMessage;
        }

        public string ErrorMessage
        {
            get { return errorMessage(); }
        }

        public bool IsViolatedAndMoreSevereThan<OtherSeverity>() where OtherSeverity : validation.Severity, new()
        {
            return IsViolated() && new Severity().IsMoreSevereThan(new OtherSeverity());
        }

        public bool IsViolated()
        {
            return failCondition();
        }
    }
}