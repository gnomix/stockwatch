using System;
using System.Collections.Generic;
using System.Linq;
using solidware.financials.infrastructure;
using solidware.financials.infrastructure.eventing;
using solidware.financials.messages;
using solidware.financials.windows.ui.events;
using solidware.financials.windows.ui.model;
using solidware.financials.windows.ui.views.controls;

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
            FamilyIncome = Money.Null;
            TotalFamilyFederalTaxes = Money.Null;
        }

        public string Header
        {
            get { return "Taxes"; }
        }

        public TaxesForIndividual Selected { get; set; }
        public Observable<Money> FamilyIncome { get; set; }
        public Observable<Money> TotalFamilyFederalTaxes { get; set; }

        public void present()
        {
            bus.publish<FindAllIncome>();
        }

        public void notify(IncomeMessage message)
        {
            FamilyIncome.Value += message.Amount;
            TaxesFor(message.PersonId).AddIncome(message.Amount);
            TotalFamilyFederalTaxes.Value = family.Values.Sum(x => x.FederalTaxes.Taxes.Value);
        }

        public void notify(SelectedFamilyMember message)
        {
            Selected = TaxesFor(message.id);
            update(x => x.Selected);
        }

        TaxesForIndividual TaxesFor(Guid id)
        {
            if (!family.ContainsKey(id))
                family[id] = new TaxesForIndividual(id, new FederalTaxesViewModel(id));
            return family[id];
        }

        IDictionary<Guid, TaxesForIndividual> family = new Dictionary<Guid, TaxesForIndividual>();
    }
}