using System;
using System.Collections.Generic;
using System.Linq;
using solidware.financials.windows.ui.model;
using solidware.financials.windows.ui.views.controls;

namespace solidware.financials.windows.ui.presenters
{
    public class TaxesForFamily
    {
        public TaxesForFamily()
        {
            Income = Money.Null;
            FederalTaxes = Money.Null;
        }

        public Observable<Money> Income { get; set; }
        public Observable<Money> FederalTaxes { get; set; }

        public TaxesForIndividual TaxesFor(Guid id)
        {
            if (!family.ContainsKey(id))
                family[id] = new TaxesForIndividual(id, new FederalTaxesViewModel(id));
            return family[id];
        }

        public void AddIncomeFor(Guid personId, decimal amount)
        {
            TaxesFor(personId).AddIncome(amount);
            Income.Value = family.Values.Sum(x => x.Income.Value);
            FederalTaxes.Value = family.Values.Sum(x => x.FederalTaxes.Taxes.Value);
        }

        IDictionary<Guid, TaxesForIndividual> family = new Dictionary<Guid, TaxesForIndividual>();
    }
}