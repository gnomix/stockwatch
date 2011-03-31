using System;
using solidware.financials.windows.ui.model;

namespace solidware.financials.windows.ui.presenters
{
    public class FederalTaxesViewModel : ObservablePresenter<FederalTaxesViewModel>
    {
        public FederalTaxesViewModel(Guid id)
        {
            Id = id;
            Taxes = Money.Null;
        }

        public Guid Id { get; private set; }
        public views.controls.Observable<Money> Taxes { get; set; }

        public void ApplyTaxesTo(Money totalIncome)
        {
            Taxes.Value = new FederalTaxes().CalculateFederalTaxesFor(totalIncome);
        }
    }
}