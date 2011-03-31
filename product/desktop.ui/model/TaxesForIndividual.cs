using System;
using solidware.financials.windows.ui.presenters;
using solidware.financials.windows.ui.views.controls;

namespace solidware.financials.windows.ui.model
{
    public class TaxesForIndividual : ObservablePresenter<TaxesForIndividual>
    {
        public TaxesForIndividual(Guid id, FederalTaxesViewModel federalTaxes)
        {
            Id = id;
            Income = Money.Null;
            FederalTaxes = federalTaxes;
        }

        public Guid Id { get; private set; }
        public Observable<Money> Income { get; private set; }
        public FederalTaxesViewModel FederalTaxes { get; set; }

        public void AddIncome(decimal amount)
        {
            Income.Value += amount;
            FederalTaxes.ApplyTaxesTo(Income.Value);
        }
    }
}