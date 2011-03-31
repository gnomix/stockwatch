using System;
using solidware.financials.windows.ui.views.controls;
using utility;

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
        public Observable<Money> Taxes { get; set; }

        public void ApplyTaxesTo(Money totalIncome)
        {
            Taxes.Value = CalculateFederalTaxesFor(totalIncome);
        }

        public decimal CalculateFederalTaxesFor(decimal totalIncome)
        {
            var taxes = 0m;
            if (totalIncome <= 41544.00m)
            {
                taxes = totalIncome.subtract(0m).multiply_by(0.15m).add(0m);
            }
            if (totalIncome > 41544.00m && totalIncome <= 83088.00m)
            {
                taxes = ((totalIncome - 41544m)*0.22m) + 6232m;
            }
            if (totalIncome > 83088.00m && totalIncome <= 128800.00m)
            {
                taxes = ((totalIncome - 83088m)*0.26m) + 15371m;
            }
            if (totalIncome > 128800.00m)
            {
                taxes = ((totalIncome - 128800m)*0.29m) + 27256m;
            }
            return taxes;
        }
    }
}