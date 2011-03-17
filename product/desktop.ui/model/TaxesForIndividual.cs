namespace desktop.ui.model
{
    public class TaxesForIndividual : Observable<TaxesForIndividual>
    {
        public decimal TotalIncome { get; set; }
        public decimal Taxes { get; set; }
    }
}