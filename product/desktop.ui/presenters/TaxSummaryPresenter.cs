using desktop.ui.eventing;

namespace desktop.ui.presenters
{
    public class TaxSummaryPresenter : Observable<TaxSummaryPresenter>, TabPresenter,
                                       EventSubscriber<AddIncomeCommandMessage>
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

        public decimal TotalIncome { get; set; }
        public decimal Taxes { get; set; }

        public void notify(AddIncomeCommandMessage message)
        {
            TotalIncome += message.amount;
            if(TotalIncome <= 41544.00m)
            {
                Taxes = ((TotalIncome - 0m)*0.15m) + 0m;
            }
            if (TotalIncome > 41544.00m && TotalIncome <= 83088.00m)
            {
                Taxes = ((TotalIncome - 41544m)*0.22m) + 6232m;
            }
            if(TotalIncome > 83088.00m && TotalIncome <= 128800.00m)
            {
                Taxes = ((TotalIncome - 83088m)*0.26m) + 15371m;
            }
            if(TotalIncome > 128800.00m)
            {
                Taxes = ((TotalIncome - 128800m)*0.29m) + 27256m;
            }
            update(x => x.Taxes, x => x.TotalIncome);
        }
    }
}