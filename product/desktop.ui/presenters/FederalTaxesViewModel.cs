using System;
using System.Collections.Generic;
using solidware.financials.windows.ui.model;

namespace solidware.financials.windows.ui.presenters
{
    public class FederalTaxesViewModel : Observable<FederalTaxesViewModel>
    {
        public FederalTaxesViewModel(Guid id)
        {
            Id = id;
            FederalTaxesGrid = new List<TaxRow>
                               {
                                   new TaxRow {Name = "john doe", Tax = 23456.09m},
                                   new TaxRow {Name = "sally doe", Tax = 9456.09m},
                               };
        }

        public Guid Id { get; private set; }
        public decimal FederalTaxes { get; set; }
        public decimal FederalFamilyTaxes { get; private set; }
        public IEnumerable<TaxRow> FederalTaxesGrid { get; private set; }

        public void ChangeTotalIncomeTo(decimal totalIncome)
        {
            FederalTaxes = new FederalTaxes().CalculateFederalTaxesFor(totalIncome);
            update(x => x.FederalTaxes);
        }
    }
}