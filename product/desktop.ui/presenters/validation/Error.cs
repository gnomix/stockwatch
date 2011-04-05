namespace solidware.financials.windows.ui.presenters.validation
{
    public class Error : Severity
    {
        public bool IsMoreSevereThan<OtherSeverity>(OtherSeverity otherSeverity) where OtherSeverity : Severity
        {
            return true;
        }
    }
}