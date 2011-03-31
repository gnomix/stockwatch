using System;
using solidware.financials.windows.ui.presenters;
using solidware.financials.windows.ui.views.controls;

namespace solidware.financials.windows.ui.model
{
    public class TaxesForIndividual : ObservablePresenter<TaxesForIndividual>
    {
        public TaxesForIndividual(Guid id, FederalTaxesViewModel federalTaxes)
        {
            FederalTaxes = federalTaxes;
            Id = id;
            TotalIncome = Money.Null;
        }

        public Guid Id { get; private set; }
        public Observable<Money> TotalIncome { get; private set; }
        public FederalTaxesViewModel FederalTaxes { get; set; }

        public void AddIncome(decimal amount)
        {
            TotalIncome.Value += amount;
            FederalTaxes.ApplyTaxesTo(TotalIncome.Value);
        }
    }
}