namespace solidware.financials.service.domain.accounting
{
    public interface UnitOfMeasure
    {
        double convert(double amount, UnitOfMeasure other);
        string pretty_print(double amount);
    }

}