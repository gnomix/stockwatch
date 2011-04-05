namespace solidware.financials.windows.ui.presenters.validation
{
    public class Warning : Severity
    {
        public bool IsMoreSevereThan<OtherSeverity>(OtherSeverity otherSeverity) where OtherSeverity : Severity
        {
            return !(otherSeverity is Error);
        }
    }
}