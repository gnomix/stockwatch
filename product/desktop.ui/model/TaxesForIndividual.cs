using System;
using solidware.financials.windows.ui.presenters;
using solidware.financials.windows.ui.views.controls;

namespace solidware.financials.windows.ui.model
{
    public class TaxesForIndividual : ObservablePresenter<TaxesForIndividual>
    {
        public TaxesForIndividual(Guid id, FederalTaxesViewModel federalTaxes, ProvincialTaxesViewModel provincialTaxes)
        {
            Id = id;
            Income = Money.Null;
            FederalTaxes = federalTaxes;
            ProvincialTaxes = provincialTaxes;
        }

        public Guid Id { get; private set; }
        public Observable<Money> Income { get; private set; }
        public FederalTaxesViewModel FederalTaxes { get; set; }
        public ProvincialTaxesViewModel ProvincialTaxes { get; set; }

        public void AddIncome(decimal amount)
        {
            Income.Value += amount;
            FederalTaxes.ApplyTaxesTo(Income.Value);
            ProvincialTaxes.ApplyTaxesTo(Income.Value);
        }
    }
}