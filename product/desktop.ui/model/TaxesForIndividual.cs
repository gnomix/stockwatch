using System;
using System.Collections.Generic;
using solidware.financials.windows.ui.presenters;

namespace solidware.financials.windows.ui.model
{
    public class TaxesForIndividual : Observable<TaxesForIndividual>
    {
        public TaxesForIndividual(Guid id, FederalTaxesViewModel federalTaxes)
        {
            FederalTaxes = federalTaxes;
            Id = id;
            ProvincialTaxesGrid = new List<TaxRow>
                                  {
                                      new TaxRow {Name = "john doe", Tax = 23456.09m},
                                      new TaxRow {Name = "sally doe", Tax = 9456.09m},
                                  };
        }

        public Guid Id { get; private set; }
        public decimal TotalIncome { get; private set; }
        public decimal TotalFamilyIncome { get; private set; }
        public IEnumerable<TaxRow> ProvincialTaxesGrid { get; private set; }
        public FederalTaxesViewModel FederalTaxes { get; set; }

        public void AddIncome(decimal amount)
        {
            TotalIncome += amount;
            FederalTaxes.ChangeTotalIncomeTo(TotalIncome);
            update(x => x.TotalIncome);
        }
    }
}