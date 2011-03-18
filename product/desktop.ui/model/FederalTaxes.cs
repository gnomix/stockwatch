namespace solidware.financials.windows.ui.model
{
    public class FederalTaxes
    {
        public decimal CalculateFederalTaxesFor(decimal totalIncome)
        {
            var taxes = 0m;
            if (totalIncome <= 41544.00m)
            {
                taxes = ((totalIncome - 0m)*0.15m) + 0m;
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