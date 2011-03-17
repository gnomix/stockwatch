using System;
using System.Collections.Generic;
using desktop.ui.eventing;
using desktop.ui.events;
using desktop.ui.model;

namespace desktop.ui.presenters
{
    public class TaxSummaryPresenter : Observable<TaxSummaryPresenter>, TabPresenter,
                                       EventSubscriber<AddIncomeCommandMessage>, EventSubscriber<SelectedFamilyMember>
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
            Selected.AddIncome(message.amount);
        }

        public void notify(SelectedFamilyMember message)
        {
            if (!family.ContainsKey(message.id))
                family[message.id] = new TaxesForIndividual();
            Selected = family[message.id];
            update(x => x.Selected);
        }

        IDictionary<Guid, TaxesForIndividual> family = new Dictionary<Guid, TaxesForIndividual>();
    }
}