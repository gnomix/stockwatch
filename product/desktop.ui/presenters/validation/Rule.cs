namespace solidware.financials.windows.ui.presenters.validation
{
    public interface Rule
    {
        bool IsViolated();
        string ErrorMessage { get; }
        bool IsViolatedAndMoreSevereThan<Severity>() where Severity : validation.Severity, new();
    }
}