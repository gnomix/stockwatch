using System;

namespace solidware.financials.windows.ui.model
{
    public class TaxesForIndividual : Observable<TaxesForIndividual>
    {
        public TaxesForIndividual(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
        public decimal TotalIncome { get; private set; }
        public decimal FederalTaxes { get; private set; }

        public void AddIncome(decimal amount)
        {
            TotalIncome += amount;
            FederalTaxes = new FederalTaxes().CalculateFederalTaxesFor(TotalIncome);
            update(x => x.FederalTaxes, x => x.TotalIncome);
        }

    }
}