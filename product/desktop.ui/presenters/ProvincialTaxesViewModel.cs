using System;
using solidware.financials.windows.ui.views.controls;
using utility;

namespace solidware.financials.windows.ui.presenters
{
    public class ProvincialTaxesViewModel : ObservablePresenter<FederalTaxesViewModel>
    {
        public ProvincialTaxesViewModel(Guid id)
        {
            Id = id;
            Taxes = Money.Null;
        }

        public Guid Id { get; private set; }
        public Observable<Money> Taxes { get; set; }

        public void ApplyTaxesTo(Money totalIncome)
        {
            Taxes.Value = CalculateFederalTaxesFor(totalIncome);
        }

        public decimal CalculateFederalTaxesFor(decimal totalIncome)
        {
            return totalIncome.multiply_by(0.10m);
        }
    }
}