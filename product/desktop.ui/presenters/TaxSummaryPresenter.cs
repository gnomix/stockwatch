using solidware.financials.infrastructure;
using solidware.financials.infrastructure.eventing;
using solidware.financials.messages;
using solidware.financials.windows.ui.events;
using solidware.financials.windows.ui.model;

namespace solidware.financials.windows.ui.presenters
{
    public class TaxSummaryPresenter : ObservablePresenter<TaxSummaryPresenter>, TabPresenter, EventSubscriber<IncomeMessage>, EventSubscriber<SelectedFamilyMember>
    {
        UICommandBuilder builder;
        ServiceBus bus;

        public TaxSummaryPresenter(UICommandBuilder builder, ServiceBus bus)
        {
            this.builder = builder;
            this.bus = bus;
            Family = new TaxesForFamily();
        }

        public string Header
        {
            get { return "Taxes"; }
        }

        public TaxesForIndividual Individual { get; set; }
        public TaxesForFamily Family { get; set; }

        public void present()
        {
            bus.publish<FindAllIncome>();
        }

        public void notify(IncomeMessage message)
        {
            Family.AddIncomeFor(message.PersonId, message.Amount);
            update(x => x.Family);
        }

        public void notify(SelectedFamilyMember message)
        {
            Individual = Family.TaxesFor(message.id);
            update(x => x.Individual);
        }
    }
}