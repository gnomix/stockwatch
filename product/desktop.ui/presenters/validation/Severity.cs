namespace solidware.financials.windows.ui.presenters.validation
{
    public interface Severity
    {
        bool IsMoreSevereThan<OtherSeverity>(OtherSeverity otherSeverity) where OtherSeverity : Severity;
    }
}