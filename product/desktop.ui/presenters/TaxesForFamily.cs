using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            ProvincialTaxes = Money.Null;
            FederalTaxes = Money.Null;
            Chart = new ObservableCollection<KeyValuePair<string, decimal>>();
        }

        public Observable<Money> Income { get; set; }
        public Observable<Money> ProvincialTaxes { get; set; }
        public Observable<Money> FederalTaxes { get; set; }
        public ICollection<KeyValuePair<string, decimal>> Chart { get; set; }

        public TaxesForIndividual TaxesFor(Guid id)
        {
            if (!family.ContainsKey(id))
                family[id] = new TaxesForIndividual(id, new FederalTaxesViewModel(id), new ProvincialTaxesViewModel(id));
            return family[id];
        }

        public void AddIncomeFor(Guid personId, decimal amount)
        {
            TaxesFor(personId).AddIncome(amount);
            Income.Value = family.Values.Sum(x => x.Income.Value);
            ProvincialTaxes.Value = family.Values.Sum(x => x.ProvincialTaxes.Taxes.Value);
            FederalTaxes.Value = family.Values.Sum(x => x.FederalTaxes.Taxes.Value);

            RefreshChart();
        }

        void RefreshChart()
        {
            Chart.Clear();
            Chart.Add(new KeyValuePair<string, decimal>("Income", Income.Value));
            Chart.Add(new KeyValuePair<string, decimal>("Federal", FederalTaxes.Value));
            Chart.Add(new KeyValuePair<string, decimal>("Provincial", ProvincialTaxes.Value));
        }

        IDictionary<Guid, TaxesForIndividual> family = new Dictionary<Guid, TaxesForIndividual>();
    }
}