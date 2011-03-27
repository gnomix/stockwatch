using System;
using System.Collections.Generic;
using solidware.financials.infrastructure;
using solidware.financials.infrastructure.eventing;
using solidware.financials.messages;
using solidware.financials.windows.ui.events;
using solidware.financials.windows.ui.model;

namespace solidware.financials.windows.ui.presenters
{
    public class TaxSummaryPresenter : Observable<TaxSummaryPresenter>, TabPresenter, EventSubscriber<IncomeMessage>, EventSubscriber<SelectedFamilyMember>
    {
        UICommandBuilder builder;
        ServiceBus bus;

        public TaxSummaryPresenter(UICommandBuilder builder, ServiceBus bus)
        {
            this.builder = builder;
            this.bus = bus;
        }

        public void present()
        {
            bus.publish<FindAllIncome>();
        }

        public string Header
        {
            get { return "Taxes"; }
        }

        public TaxesForIndividual Selected { get; set; }

        public void notify(IncomeMessage message)
        {
            TaxesFor(message.PersonId).AddIncome(message.Amount);
        }

        public void notify(SelectedFamilyMember message)
        {
            Selected = TaxesFor(message.id);
            update(x => x.Selected);
        }

        TaxesForIndividual TaxesFor(Guid id)
        {
            if (!family.ContainsKey(id))
                family[id] = new TaxesForIndividual(id);
            return family[id];
        }

        IDictionary<Guid, TaxesForIndividual> family = new Dictionary<Guid, TaxesForIndividual>();
    }
}