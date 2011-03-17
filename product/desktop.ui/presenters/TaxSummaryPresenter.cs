using System;
using System.Collections.Generic;
using desktop.ui.eventing;
using desktop.ui.events;

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
            Selected.TotalIncome += message.amount;
            if (Selected.TotalIncome <= 41544.00m)
            {
                Selected.Taxes = ((Selected.TotalIncome - 0m)*0.15m) + 0m;
            }
            if (Selected.TotalIncome > 41544.00m && Selected.TotalIncome <= 83088.00m)
            {
                Selected.Taxes = ((Selected.TotalIncome - 41544m)*0.22m) + 6232m;
            }
            if (Selected.TotalIncome > 83088.00m && Selected.TotalIncome <= 128800.00m)
            {
                Selected.Taxes = ((Selected.TotalIncome - 83088m)*0.26m) + 15371m;
            }
            if (Selected.TotalIncome > 128800.00m)
            {
                Selected.Taxes = ((Selected.TotalIncome - 128800m)*0.29m) + 27256m;
            }
            Selected.update(x => x.Taxes, x => x.TotalIncome);
        }

        public void notify(SelectedFamilyMember message)
        {
            if (!people.ContainsKey(message.id))
            {
                people[message.id] = new TaxesForIndividual();
            }
            Selected = people[message.id];
            update(x => x.Selected);
        }

        IDictionary<Guid, TaxesForIndividual> people = new Dictionary<Guid, TaxesForIndividual>();
    }

    public class TaxesForIndividual : Observable<TaxesForIndividual>
    {
        public decimal TotalIncome { get; set; }
        public decimal Taxes { get; set; }
    }
}