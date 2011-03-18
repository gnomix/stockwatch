using System;
using System.Collections.Generic;
using solidware.financials.infrastructure.eventing;
using solidware.financials.messages;
using solidware.financials.windows.ui.events;
using solidware.financials.windows.ui.model;

namespace solidware.financials.windows.ui.presenters
{
    public class TaxSummaryPresenter : Observable<TaxSummaryPresenter>, TabPresenter, EventSubscriber<AddIncomeCommandMessage>, EventSubscriber<SelectedFamilyMember>
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

        public TaxesForIndividual Selected { get; set; }

        public void notify(AddIncomeCommandMessage message)
        {
            Selected.AddIncome(message.Amount);
        }

        public void notify(SelectedFamilyMember message)
        {
            if (!family.ContainsKey(message.id))
                family[message.id] = new TaxesForIndividual(message.id);
            Selected = family[message.id];
            update(x => x.Selected);
        }

        IDictionary<Guid, TaxesForIndividual> family = new Dictionary<Guid, TaxesForIndividual>();
    }
}