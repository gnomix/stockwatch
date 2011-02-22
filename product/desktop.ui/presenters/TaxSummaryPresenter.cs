namespace desktop.ui.presenters
{
    public class TaxSummaryPresenter : TabPresenter
    {
        UICommandBuilder builder;

        public TaxSummaryPresenter(UICommandBuilder builder)
        {
            this.builder = builder;
        }

        public void present()
        {
        }

        public string Header
        {
            get { return "Taxes"; }
        }
    }
}