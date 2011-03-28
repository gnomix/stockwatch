using System;
using System.Collections.Generic;

namespace solidware.financials.windows.ui.model
{
    public class TaxesForIndividual : Observable<TaxesForIndividual>
    {
        public TaxesForIndividual(Guid id)
        {
            Id = id;
            ProvincialTaxesGrid = new List<TaxRow>
                                  {
                                      new TaxRow {Name = "john doe", Tax = 23456.09m},
                                      new TaxRow {Name = "sally doe", Tax = 9456.09m},
                                  };
            FederalTaxesGrid = new List<TaxRow>
                               {
                                   new TaxRow {Name = "john doe", Tax = 23456.09m},
                                   new TaxRow {Name = "sally doe", Tax = 9456.09m},
                               };
        }

        public Guid Id { get; private set; }
        public decimal TotalIncome { get; private set; }
        public decimal TotalFamilyIncome { get; private set; }
        public decimal FederalTaxes { get; private set; }
        public decimal FederalFamilyTaxes { get; private set; }
        public IEnumerable<TaxRow> ProvincialTaxesGrid { get; private set; }
        public IEnumerable<TaxRow> FederalTaxesGrid { get; private set; }

        public void AddIncome(decimal amount)
        {
            TotalIncome += amount;
            FederalTaxes = new FederalTaxes().CalculateFederalTaxesFor(TotalIncome);
            update(x => x.FederalTaxes, x => x.TotalIncome);
        }
    }

    public class TaxRow
    {
        public string Name { get; set; }
        public decimal Tax { get; set; }
    }
}